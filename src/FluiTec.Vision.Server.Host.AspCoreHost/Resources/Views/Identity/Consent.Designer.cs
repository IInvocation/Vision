﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FluiTec.Vision.Server.Host.AspCoreHost.Resources.Views.Identity {
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
    public class Consent {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Consent() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("FluiTec.Vision.Server.Host.AspCoreHost.Resources.Views.Identity.Consent", typeof(Consent).GetTypeInfo().Assembly);
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
        ///   Looks up a localized string similar to Ja, erlauben.
        /// </summary>
        public static string AllowAccessText {
            get {
                return ResourceManager.GetString("AllowAccessText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Anwendungssteuerung.
        /// </summary>
        public static string ApplicationAccessHeader {
            get {
                return ResourceManager.GetString("ApplicationAccessHeader", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Zustimmung.
        /// </summary>
        public static string ConsentHeader {
            get {
                return ResourceManager.GetString("ConsentHeader", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nein, nicht erlauben.
        /// </summary>
        public static string DenyAccessText {
            get {
                return ResourceManager.GetString("DenyAccessText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to FluiTech-Authentifizierung.
        /// </summary>
        public static string IdentityHeader {
            get {
                return ResourceManager.GetString("IdentityHeader", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Persönliche Informationen.
        /// </summary>
        public static string PersonalInformationHeader {
            get {
                return ResourceManager.GetString("PersonalInformationHeader", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An die Erlaubnis erinnern?.
        /// </summary>
        public static string RememberConsentText {
            get {
                return ResourceManager.GetString("RememberConsentText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to benötigt deine Erlaubnis.
        /// </summary>
        public static string RequestText {
            get {
                return ResourceManager.GetString("RequestText", resourceCulture);
            }
        }
    }
}
