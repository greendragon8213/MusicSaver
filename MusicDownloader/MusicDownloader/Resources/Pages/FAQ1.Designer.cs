﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MusicDownloader.Resources.Pages {
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
    public class FAQ {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal FAQ() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MusicDownloader.Resources.Pages.FAQ", typeof(FAQ).Assembly);
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
        ///   Looks up a localized string similar to 1). If you need to download all music list scroll down the vk music page and select all songs; &lt;br&gt;
        /// 2). If there are lots of songs it would be better if you download them by groups of 100 songs; &lt;br&gt;
        /// 3). To download not by group of 100 songs just use the {0}. &lt;br&gt;.
        /// </summary>
        public static string Answer_HowToDownloadAllFromVk {
            get {
                return ResourceManager.GetString("Answer_HowToDownloadAllFromVk", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to  1). Go to your (or somebody else) music page; &lt;br&gt;
        /// 2). Select all songs you need (don&apos;t bother about duration, our service is able to recognize and ignore it if it has been copied); &lt;br&gt;
        /// 3). Just copy-past selected songs to our service and click to download. &lt;br&gt;.
        /// </summary>
        public static string Answer_HowToDownloadFromVk {
            get {
                return ResourceManager.GetString("Answer_HowToDownloadFromVk", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to It happens when you are trying to download too many songs. Please download them by groups of 100 songs. &lt;br&gt;
        /// If you would like to download all use the {0} &lt;br&gt;.
        /// </summary>
        public static string Answer_WhyIsBeingDownloadingLong {
            get {
                return ResourceManager.GetString("Answer_WhyIsBeingDownloadingLong", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to FAQ.
        /// </summary>
        public static string FaqTitle {
            get {
                return ResourceManager.GetString("FaqTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to How to download all my (or somebody else) music from vk.com?.
        /// </summary>
        public static string Question_HowToDownloadAllFromVk {
            get {
                return ResourceManager.GetString("Question_HowToDownloadAllFromVk", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to How to download music from vk.com?.
        /// </summary>
        public static string Question_HowToDownloadFromVk {
            get {
                return ResourceManager.GetString("Question_HowToDownloadFromVk", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Why my music is being downloaded too long?.
        /// </summary>
        public static string Question_WhyIsBeingDownloadingLong {
            get {
                return ResourceManager.GetString("Question_WhyIsBeingDownloadingLong", resourceCulture);
            }
        }
    }
}
