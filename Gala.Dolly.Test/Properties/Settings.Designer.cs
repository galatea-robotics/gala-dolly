﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gala.Dolly.Test.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.7.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
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
        [global::System.Configuration.DefaultSettingValueAttribute("MARIA")]
        public string DefaultChatbotName {
            get {
                return ((string)(this["DefaultChatbotName"]));
            }
            set {
                this["DefaultChatbotName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Because I\'m awesome!")]
        public string DefaultChatbotResponse {
            get {
                return ((string)(this["DefaultChatbotResponse"]));
            }
            set {
                this["DefaultChatbotResponse"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Diagnostic")]
        public global::Galatea.Diagnostics.DebuggerLogLevel DebuggerLogLevel {
            get {
                return ((global::Galatea.Diagnostics.DebuggerLogLevel)(this["DebuggerLogLevel"]));
            }
            set {
                this["DebuggerLogLevel"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Message")]
        public global::Galatea.Diagnostics.DebuggerLogLevel DebuggerAlertLevel {
            get {
                return ((global::Galatea.Diagnostics.DebuggerLogLevel)(this["DebuggerAlertLevel"]));
            }
            set {
                this["DebuggerAlertLevel"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("..\\..\\..\\..\\Data\\SerializedData.dat")]
        public string DataAccessManagerConnectionString {
            get {
                return ((string)(this["DataAccessManagerConnectionString"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("25")]
        public short ColorTemplateHybridResultThreshold {
            get {
                return ((short)(this["ColorTemplateHybridResultThreshold"]));
            }
            set {
                this["ColorTemplateHybridResultThreshold"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ImagingSettings xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Timeout>2000</Timeout>
  <SuppressTimeout>true</SuppressTimeout>
  <DebugRecognitionSaveImages>true</DebugRecognitionSaveImages>
  <ColorStatsSettings>
    <StatisticalAnalysisTypes>Mean</StatisticalAnalysisTypes>
  </ColorStatsSettings>
  <MonochromeBlobFilterSettings>
    <ContrastCorrectionFactor>-64</ContrastCorrectionFactor>
    <AdaptiveSmoothingFactor>0.25</AdaptiveSmoothingFactor>
    <FrameWidth>10</FrameWidth>
  </MonochromeBlobFilterSettings>
  <BlobPointSettings>
    <LineSegmentThreshold>18</LineSegmentThreshold>
    <LineAngleThreshold>9</LineAngleThreshold>
    <CurveAngleThreshold>27</CurveAngleThreshold>
  </BlobPointSettings>
  <TemplateRecognitionSettings>
    <ColorBrightnessThreshold>6</ColorBrightnessThreshold>
    <ColorSaturationThreshold>5</ColorSaturationThreshold>
    <ShapeOblongThreshold>1.75</ShapeOblongThreshold>
    <ShapeOblongRecognitionLevel>2</ShapeOblongRecognitionLevel>
    <ShapeOblongRecognitionNormalization>true</ShapeOblongRecognitionNormalization>
    <IdentifyShapeCertaintyMinimum>65</IdentifyShapeCertaintyMinimum>
  </TemplateRecognitionSettings>
</ImagingSettings>")]
        public global::Galatea.AI.Imaging.ImagingSettings ImagingSettings {
            get {
                return ((global::Galatea.AI.Imaging.ImagingSettings)(this["ImagingSettings"]));
            }
            set {
                this["ImagingSettings"] = value;
            }
        }
    }
}
