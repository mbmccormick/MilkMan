﻿using Milkman.Background.Common;
using Microsoft.Phone.Scheduler;
using Milkman.Common;
using System;
using System.Device.Location;
using System.Net;
using System.Windows;
using System.Reflection;

namespace Milkman.Background
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        private static volatile bool _classInitialized;

        public ScheduledAgent()
        {
            if (!_classInitialized)
            {
                _classInitialized = true;
                Deployment.Current.Dispatcher.BeginInvoke(delegate
                {
                    Application.Current.UnhandledException += ScheduledAgent_UnhandledException;
                });
            }
        }

        private void ScheduledAgent_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is WebException)
            {
                // ignore these exceptions
            }
            else if (e.ExceptionObject is OutOfMemoryException)
            {
                // ignore these exceptions
            }
            else if (e.ExceptionObject is TargetInvocationException)
            {
                // ignore these exceptions
            }
            else
            {
                LittleWatson.ReportException(e.ExceptionObject, null);
            }

            e.Handled = true;

            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debugger.Break();
            }

            NotifyComplete();
        }

        GeoCoordinateWatcher _watcher = null;

        protected override void OnInvoke(ScheduledTask task)
        {
            App.LoadData();

            AppSettings settings = new AppSettings();

            if (settings.LocationRemindersEnabled == true)
            {
                _watcher = new GeoCoordinateWatcher();
                _watcher.Start();
            }

            SyncData();
        }

        private void SyncData()
        {
            if (!string.IsNullOrEmpty(App.RtmClient.AuthToken))
            {
                App.RtmClient.SyncEverything(() =>
                {
                    LoadData();

                    if (System.Diagnostics.Debugger.IsAttached)
                        ScheduledActionService.LaunchForTest("BackgroundWorker", new TimeSpan(0, 0, 1, 0)); // every minute

                    NotifyComplete();
                });
            }
            else
            {
                NotificationsManager.ResetLiveTiles();

                if (System.Diagnostics.Debugger.IsAttached)
                    ScheduledActionService.LaunchForTest("BackgroundWorker", new TimeSpan(0, 0, 1, 0)); // every minute

                NotifyComplete();
            }
        }

        private void LoadData()
        {
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                App.SaveData();

                if (_watcher != null)
                    NotificationsManager.SetupNotifications(_watcher.Position.Location);
                else
                    NotificationsManager.SetupNotifications(null);
            });
        }
    }
}
