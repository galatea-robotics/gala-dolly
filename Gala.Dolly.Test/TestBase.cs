using System;
using System.ComponentModel;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gala.Dolly.Test
{
    using Galatea;
    using Galatea.AI.Imaging;
    using Galatea.AI.Imaging.Configuration;
    using Galatea.Diagnostics;
    using Galatea.Imaging.IO;
    using Gala.Data;
    using Gala.Data.Databases;
    using Properties;

    [TestClass]
    public class TestBase : IProvider
    {
        private static TestEngine _engine;
        private static Galatea.AI.Abstract.IUser _user;
        private static Galatea.AI.Abstract.BaseTemplate _namedTemplate;
        private static Galatea.AI.Abstract.NamedEntity _namedEntity;
        private static string connectionString;

        internal static TestEngine TestEngine { get { return _engine; } }

        public TestBase()
        {
            var t = this.GetType();
            _providerId = t.FullName;
            _providerName = t.Name;
        }

        public Galatea.AI.Abstract.IUser User { get { return _user; } }
        public Galatea.AI.Abstract.BaseTemplate NamedTemplate { get { return _namedTemplate; } }
        public Galatea.AI.Abstract.NamedEntity NamedEntity { get { return _namedEntity; } }

        protected ImagingContextStream GetImagingContextStream(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException("File not Found!", new FileInfo(filename).FullName);

            return ImagingContextStream.FromBitmap(new System.Drawing.Bitmap(filename));
        }

        protected static string ConnectionString { get { return connectionString; } }

        protected static string resourcesFolderName;

        #region IProvider
        string IProvider.ProviderID { get { return _providerId; } }
        string IProvider.ProviderName { get { return _providerName; } }

        ISite IComponent.Site
        {
            get { return _site; }
            set { _site = value; }
        }
        void IDisposable.Dispose()
        {
            _engine.Dispose();
            if (Disposed != null) Disposed(this, EventArgs.Empty);
        }
        public event EventHandler Disposed;

        private readonly string _providerId, _providerName;
        private ISite _site;
        #endregion

        [AssemblyInitialize]
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

                Properties.Settings.Default.DebuggerLogLevel = DebuggerLogLevel.Diagnostic;
                Properties.Settings.Default.DebuggerAlertLevel = DebuggerLogLevel.Message;

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

                Properties.Settings.Default.Save();

                // Suppress Timeout
                Settings.Default.ImagingSettings.Timeout = 2000;
                Settings.Default.ImagingSettings.DebugRecognitionSaveImages = true;
                Settings.Default.Save();

                Settings.Default.ImagingSettings.SuppressTimeout = true;

                // Initialize Runtime
                _user = new Galatea.Runtime.Services.User("Test");

                Properties.Settings.Default.DebuggerLogLevel = Galatea.Diagnostics.DebuggerLogLevel.Diagnostic;
                DebuggerLogLevelSettings.Initialize(Properties.Settings.Default.DebuggerLogLevel, Properties.Settings.Default.DebuggerAlertLevel);
                Galatea.Diagnostics.IDebugger debugger = new Gala.Dolly.Test.TestDebugger();

                SerializedDataAccessManager dataAccessManager;
                //TestEngine.LoadConfig();

                connectionString = Properties.Settings.Default.DataAccessManagerConnectionString;
                dataAccessManager = new SerializedDataAccessManager(connectionString);
                resourcesFolderName = @"..\..\..\..\Resources\";

                // Restore Data from backup file
                FileInfo fi = new FileInfo(connectionString);
                string backupFilename = Path.Combine(fi.DirectoryName, "SerializedData.V1.dat");
                dataAccessManager.RestoreBackup(backupFilename);

                // Suppress Timeout
                Properties.Settings.Default.ImagingSettings.SuppressTimeout = true;

                _engine = new TestEngine(debugger, dataAccessManager);
                _engine.User = _user;
                _engine.AI.LanguageModel.IsSpeechModuleInactive = true;
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

        private static void _engine_StartupComplete(object sender, EventArgs e)
        {
#if PORTABLE
            Galatea.AI.Imaging.ImageManager.BitmapConverter = new Gala.Dolly.Test.BitmapConverter();
#endif
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
