using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Gala.Data.Properties
{
    internal class Settings
    {
        protected Settings()
        {
        }

        public static void Load(string path = @"Properties\Settings.json")
        {
            /*
            //https://docs.microsoft.com/en-us/windows/uwp/files/file-access-permissions

            Assembly configAsm = typeof(Settings).GetTypeInfo().Assembly;
            StorageFile file = await configAsm.GetStorageFile(ApplicationData.Current.LocalFolder, path);
             */

            string data = null;
            Settings result = null;

            try
            {
                data = File.ReadAllText(path);
                result = JsonConvert.DeserializeObject<Settings>(data);
                if (result == null) throw new NullReferenceException();
            }
            catch
            {
                /*
                // Copy Local Settings
                string appPath = Path.Combine("Gala.Data.Config", new FileInfo(path).Name);
                if (File.Exists(path)) File.Delete(path);
                File.Copy(appPath, path);
                data = File.ReadAllText(path);
                result = JsonConvert.DeserializeObject<Settings>(data);
                if (result == null) throw;
                 */

                throw;
            }

            // TODO:  Auto-Update Storage File if settings schema changes
            _default = result;
        }

        public void Save(string filename = @"Properties\Settings.json")
        {
            string data = JsonConvert.SerializeObject(this);
            System.IO.File.WriteAllText(filename, data);
        }

        public string DataAccessManagerConnectionString { get; set; }
        public string DefaultUserName { get; set; }
        public string DefaultChatbotName { get; set; }
        public string DefaultChatbotResponse { get; set; }

        /*
        public string ChatbotAliceConfigFolder { get; set; }
        public string ChatbotResourcesFolder { get; set; }
        public short ChatbotDisplayResponseWaitTime { get; set; }
         */

        public Galatea.Diagnostics.DebuggerLogLevel DebuggerLogLevel { get; set; }
        public Galatea.Diagnostics.DebuggerLogLevel DebuggerAlertLevel { get; set; }

        public short ColorTemplateHybridResultThreshold { get; set; }

        /*
        public short ColorBrightnessThreshold { get; set; }

        public int GpioLeftForwardPin { get; set; }
        public int GpioLeftReversePin { get; set; }
        public int GpioRightForwardPin { get; set; }
        public int GpioRightReversePin { get; set; }

        public int HttpRequestTimeout { get; set; }
        public string HttpServiceHostIpAddress { get; set; }
        public int HttpServiceHostPort { get; set; }

        public short ShapeOblongRecognitionLevel { get; set; }
        public decimal ShapeOblongThreshold { get; set; }
        public bool ShapeOblongRecognitionNormalization { get; set; }
        public bool SpeechIsSilent { get; set; }
         */

        public Galatea.AI.Imaging.ImagingSettings ImagingSettings { get; set; }

        public static Settings Default { get { return _default; } }

        private static Settings _default;
    }
}
