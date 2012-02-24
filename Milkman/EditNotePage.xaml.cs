﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Collections.ObjectModel;
using Milkman.Common;
using IronCow;
using System.ComponentModel;
using Microsoft.Phone.Shell;

namespace Milkman
{
    public partial class EditNotePage : PhoneApplicationPage
    {
        #region IsLoading Property

        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register("IsLoading", typeof(bool), typeof(EditNotePage),
                new PropertyMetadata((bool)false));

        private bool loadedDetails = false;

        public bool IsLoading
        {
            get
            {
                return (bool)GetValue(IsLoadingProperty);
            }

            set
            {
                try
                {
                    SetValue(IsLoadingProperty, value);
                    if (progressIndicator != null)
                        progressIndicator.IsIndeterminate = value;
                }
                catch (Exception ex)
                {
                }
            }
        }

        #endregion

        #region Task Property

        private Task CurrentTask = null;
        private TaskNote CurrentNote = null;

        #endregion

        #region Construction and Navigation

        ProgressIndicator progressIndicator;

        public EditNotePage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            progressIndicator = new ProgressIndicator();
            progressIndicator.IsVisible = true;
            SystemTray.ProgressIndicator = progressIndicator;

            if (!loadedDetails)
            {
                IsLoading = true;

                ReloadNote();
                loadedDetails = true;
            }

            base.OnNavigatedTo(e);
        }

        #endregion

        #region Loading Data

        private void ReloadNote()
        {
            // load task
            string taskId;
            if (NavigationContext.QueryString.TryGetValue("task", out taskId))
            {
                CurrentTask = App.RtmClient.GetTask(taskId);

                // load note
                string id;
                if (NavigationContext.QueryString.TryGetValue("id", out id))
                {
                    CurrentNote = CurrentTask.GetNote(id);

                    // bind title
                    this.txtTitle.Text = CurrentNote.Title;

                    // bind body
                    this.txtBody.Text = CurrentNote.Body;
                }
            }

            IsLoading = false;
        }

        #endregion

        #region Event Handlers

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!IsLoading)
            {
                IsLoading = true;

                // edit note
                SmartDispatcher.BeginInvoke(() =>
                {
                    this.txtBody.Text = this.txtBody.Text.Replace("\r", "\n");

                    CurrentTask.EditNote(CurrentNote, this.txtTitle.Text, this.txtBody.Text, () =>
                    {
                        Dispatcher.BeginInvoke(() =>
                        {
                            IsLoading = false;
                            NavigationService.GoBack();
                        });
                    });
                });
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.NavigationService.CanGoBack)
                this.NavigationService.GoBack();
            else
                NavigationService.Navigate(new Uri("/TaskDetailsPage.xaml?id=" + CurrentTask.Id, UriKind.Relative));
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!IsLoading)
            {
                if (MessageBox.Show("Are you sure you want to delete this note?", "Delete", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    IsLoading = true;

                    // delete note
                    SmartDispatcher.BeginInvoke(() =>
                    {
                        CurrentTask.DeleteNote(CurrentNote, () =>
                        {
                            App.RtmClient.CacheTasks(() =>
                            {
                                Dispatcher.BeginInvoke(() =>
                                {
                                    IsLoading = false;
                                    NavigationService.GoBack();
                                });
                            });
                        });
                    });
                }
            }
        }

        #endregion
    }
}