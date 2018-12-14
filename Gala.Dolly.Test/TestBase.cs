using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gala.Dolly.Test
{
    using Galatea;
    using Galatea.AI.Abstract;
    using Galatea.AI.Imaging;
    using Galatea.AI.Imaging.Configuration;
    using Galatea.Diagnostics;
    using Galatea.Imaging.IO;
    using Gala.Data;
    using Gala.Data.Databases;
    using Properties;

    [TestClass]
    [CLSCompliant(false)]
    public class TestBase : IProvider
    {
        private static TestEngine _engine;
        private static IUser _user;
        private static BaseTemplate _namedTemplate;
        private static NamedEntity _namedEntity;
        private static string connectionString;

        protected const string ResourcesFolderName = @"..\..\..\..\Resources\";

        internal static TestEngine TestEngine { get { return _engine; } }

        public TestBase()
        {
            var t = this.GetType();
            _providerId = t.FullName;
            _providerName = t.Name;
        }

        //public Galatea.AI.Abstract.IUser User { get { return _user; } }

        public static BaseTemplate NamedTemplate { get { return _namedTemplate; } }
        public static NamedEntity NamedEntity { get { return _namedEntity; } }

        protected static ImagingContextStream GetImagingContextStream(string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException("File not Found!", new FileInfo(fileName).FullName);

            using (Bitmap bitmap = new Bitmap(fileName))
            {
                return ImagingContextStream.FromImage(bitmap);
            }
        }

        protected static string ConnectionString { get { return connectionString; } }

        #region IProvider
        public string ProviderId { get { return _providerId; } }
        public string ProviderName { get { return _providerName; } }
        public ISite Site
        {
            get { return _site; }
            set { _site = value; }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                _engine.Dispose();
            }

            Disposed?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Disposed;

        private readonly string _providerId, _providerName;
        private ISite _site;
        #endregion

        [AssemblyInitialize]
#pragma warning disable CA1801 // Review unused parameters
        public static void Initialize(TestContext testContext)
        {
            try
            {
#if NETFX_CORE
                // Load Local Settings
                Properties.Settings.Load(@"Properties\TestSettings.json");
#endif
                //if (System.IO.File.Exists("Gala.Dolly.Command.config"))
                //    AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", "Gala.Dolly.Command.exe.config");

                Settings.Default.DebuggerLogLevel = DebuggerLogLevel.Diagnostic;
                Settings.Default.DebuggerAlertLevel = DebuggerLogLevel.Message;
                #region Default Settings
                /*
                Properties.Settings.Default.ImagingSettings = new ImagingSettings
                {
                    Timeout = 2000,
                    SuppressTimeout = true,
                    ColorStatsSettings = new Galatea.AI.Imaging.Configuration.ColorStatsSettings
                    {
                        StatisticalAnalysisTypes = Galatea.AI.Math.StatsTypes.Mean
                    },
                    MonochromeBlobFilterSettings = new MonochromeBlobFilterSettings
                    {
                        ContrastCorrectionFactor = 0,
                        AdaptiveSmoothingFactor = 0.25,
                        FrameWidth = 10
                    },
                    BlobPointSettings = new BlobPointSettings
                    {
                        LineSegmentThreshold = 20,
                        LineAngleThreshold = 10,
                        CurveAngleThreshold = 27
                    },
                    TemplateRecognitionSettings = new TemplateRecognitionSettings
                    {
                        ColorBrightnessThreshold = 5,
                        ColorSaturationThreshold = 5,
                        ShapeOblongThreshold = 1.75M,
                        ShapeOblongRecognitionNormalization = true,
                        IdentifyShapeCertaintyMinimum = 65
                    }
                };
                 */
                #endregion
                Settings.Default.Save();

                // Suppress Timeout
                Settings.Default.ImagingSettings.Timeout = 2000;
                Settings.Default.ImagingSettings.DebugRecognitionSaveImages = true;
                Settings.Default.Save();

                Settings.Default.ImagingSettings.SuppressTimeout = true;

                // Initialize Runtime
                _user = new Galatea.Runtime.Services.User("Test");

                Settings.Default.DebuggerLogLevel = DebuggerLogLevel.Diagnostic;
                DebuggerLogLevelSettings.Initialize(Settings.Default.DebuggerLogLevel, Settings.Default.DebuggerAlertLevel);
                connectionString = Settings.Default.DataAccessManagerConnectionString;

                TestEngine engine = null;
                IDebugger debugger = null;
                SerializedDataAccessManager dataAccessManager = null;

                try
                {
                    debugger = new TestDebugger();
                    dataAccessManager = new SerializedDataAccessManager(connectionString);

                    // Restore Data from backup file
                    FileInfo fi = new FileInfo(connectionString);
                    string backupFilename = Path.Combine(fi.DirectoryName, "SerializedData.V1.dat");
                    dataAccessManager.RestoreBackup(backupFilename);

                    // Initialize Engine
                    engine = new TestEngine(debugger, dataAccessManager);
                    _engine = engine;

                    engine = null;
                    debugger = null;
                    dataAccessManager = null;
                }
                finally
                {
                    engine?.Dispose();
                    debugger?.Dispose();
                    dataAccessManager?.Dispose();
                }

                // Suppress Timeout
                Settings.Default.ImagingSettings.SuppressTimeout = true;
                TestEngine.AI.LanguageModel.IsSpeechModuleInactive = true;

                _engine.User = _user;
                _engine.StartupComplete += _engine_StartupComplete;
                _engine.Startup();

                _engine.ExecutiveFunctions.ContextRecognition += ExecutiveFunctions_ContextRecognition;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.Write(ex.StackTrace);
                throw;
            }
        }
#pragma warning restore CA1801 // Review unused parameters

        private static void _engine_StartupComplete(object sender, EventArgs e)
        {
        }

        private static void ExecutiveFunctions_ContextRecognition(object sender, Galatea.AI.Abstract.ContextRecognitionEventArgs e)
        {
            _namedTemplate = e.NamedTemplate;
            _namedEntity = e.NamedEntity;
        }

        [AssemblyCleanup]
        public static void End()
        {
            _engine.Shutdown();

            // Save Runtime Settings
            Properties.Settings.Default.DebuggerLogLevel = DebuggerLogLevelSettings.DebuggerLogLevel;
            Properties.Settings.Default.DebuggerAlertLevel = DebuggerLogLevelSettings.DebuggerAlertLevel;
            Properties.Settings.Default.Save();
        }
    }
}
