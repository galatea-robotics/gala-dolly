﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gala.Dolly.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.7.0.0")]
    internal sealed partial class LocalSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static LocalSettings defaultInstance = ((LocalSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new LocalSettings())));
        
        public static LocalSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("MARIA")]
        public string ChatbotName {
            get {
                return ((string)(this["ChatbotName"]));
            }
            set {
                this["ChatbotName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool SpeechIsSilent {
            get {
                return ((bool)(this["SpeechIsSilent"]));
            }
            set {
                this["SpeechIsSilent"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\GALA\\Log.txt")]
        public string LogFileName {
            get {
                return ((string)(this["LogFileName"]));
            }
            set {
                this["LogFileName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Peanut")]
        public string DefaultUserName {
            get {
                return ((string)(this["DefaultUserName"]));
            }
            set {
                this["DefaultUserName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("4")]
        public int TextToSpeechDefaultVoiceIndex {
            get {
                return ((int)(this["TextToSpeechDefaultVoiceIndex"]));
            }
            set {
                this["TextToSpeechDefaultVoiceIndex"] = value;
            }
        }
    }
}
