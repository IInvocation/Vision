﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FluiTec.Vision.Server.Host.AspCoreHost.Resources.MailModels {
    using System;
    using System.Reflection;
    
    
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
    public class ConfirmMailModel {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ConfirmMailModel() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("FluiTec.Vision.Server.Host.AspCoreHost.Resources.MailModels.ConfirmMailModel", typeof(ConfirmMailModel).GetTypeInfo().Assembly);
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
        ///   Looks up a localized string similar to Sie erhalten diese Nachricht, da Ihre E-Mail-Adresse dazu verwendet wurde einen Account auf &lt;a href=&quot;http://vision.fluitech.de&quot;&gt;www.vision.fluitech.de&lt;/a&gt; zu erstellen.
        ///				(Für den Fall, dass Sie dies nicht getan haben - möchten wir Sie bitten diese Nachricht zu ignorieren).
        /// </summary>
        public static string ConfirmEmailReasonText {
            get {
                return ResourceManager.GetString("ConfirmEmailReasonText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to E-Mail-Adresse bestätigen.
        /// </summary>
        public static string ConfirmLinkText {
            get {
                return ResourceManager.GetString("ConfirmLinkText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Account-Management.
        /// </summary>
        public static string Header {
            get {
                return ResourceManager.GetString("Header", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hallo, &lt;br/&gt;bitte bestätigen Sie Ihre E-Mail-Adresse durch das Öffnen des folgenden Links:.
        /// </summary>
        public static string PleaseConfirmText {
            get {
                return ResourceManager.GetString("PleaseConfirmText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to E-Mail-Adresse bestätigen.
        /// </summary>
        public static string Subject {
            get {
                return ResourceManager.GetString("Subject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Vielen Dank.&lt;br /&gt;
        ///				Ihr Team von &lt;a href=&quot;http://vision.fluitech.de&quot;&gt;www.vision.fluitech.de&lt;/a&gt;..
        /// </summary>
        public static string ThankYouText {
            get {
                return ResourceManager.GetString("ThankYouText", resourceCulture);
            }
        }
    }
}
