using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;

namespace Gala.Dolly.Properties
{
    internal static class LocalSettingsHelper
    {
        public static void Load(string path)
        {
            // Initialize Dictionary
            Dictionary<string, string> settingsList = new Dictionary<string, string>();

            // Read LocalSettings.config
            ConfigXmlDocument doc = new ConfigXmlDocument();
            doc.LoadXml(System.IO.File.ReadAllText(path));
            
            foreach(XmlNode node in doc.GetElementsByTagName("setting"))
            {
                string settingsName = node.Attributes["name"].Value;
                string settingsValue = node.InnerText;
                settingsList.Add(settingsName, settingsValue);
            }

            // Load Settings into LocalSettings

            /*
             *
            foreach (string key in settingsList.Keys)
                LocalSettings.Default[key] = settingsList[key];
             *
             *
             ***** unfortunately it looks like we have to unbox by type *****
             */

            Settings.Default.ChatbotName = settingsList["ChatbotName"];
            Settings.Default.DefaultUserName = settingsList["DefaultUserName"];
            Settings.Default.LogFileName = settingsList["LogFileName"];
            Settings.Default.SpeechIsSilent = bool.Parse(settingsList["SpeechIsSilent"]);
            Settings.Default.TextToSpeechDefaultVoiceIndex = int.Parse(settingsList["TextToSpeechDefaultVoiceIndex"]);
        }

        public static void Save(string path)
        {
            foreach(SettingsProperty property in LocalSettings.Default.Properties)
            {
                LocalSettings.Default[property.Name] = Settings.Default[property.Name];
            }
            LocalSettings.Default.Save();

            ConfigXmlDocument doc = new ConfigXmlDocument();
            doc.LoadXml(System.IO.File.ReadAllText(path));

            foreach (XmlNode node in doc.GetElementsByTagName("setting"))
            {
                string settingsName = node.Attributes["name"].Value;
                node.InnerText = LocalSettings.Default[settingsName].ToString();
            }

            doc.Save(path);
        }
    }
}