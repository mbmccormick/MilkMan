﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BackgroundWorker {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BackgroundWorker.Strings", typeof(Strings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to All Tasks.
        /// </summary>
        public static string AllTasks {
            get {
                return ResourceManager.GetString("AllTasks", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No tasks due today.
        /// </summary>
        public static string LiveTileEmpty {
            get {
                return ResourceManager.GetString("LiveTileEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to overdue.
        /// </summary>
        public static string LiveTileOverdue {
            get {
                return ResourceManager.GetString("LiveTileOverdue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to tasks due today.
        /// </summary>
        public static string LiveTilePlural {
            get {
                return ResourceManager.GetString("LiveTilePlural", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to task due today.
        /// </summary>
        public static string LiveTileSingle {
            get {
                return ResourceManager.GetString("LiveTileSingle", resourceCulture);
            }
        }
    }
}
