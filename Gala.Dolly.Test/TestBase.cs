using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;

namespace Gala.Dolly.Test
{
    using Galatea;
    using Galatea.Diagnostics;
    using Gala.Data;
    using Gala.Data.Databases;
    using Gala.Dolly.Test.Properties;

    [TestClass]
    public class TestBase : IProvider
    {
        private static TestEngine _engine;
        private static Galatea.AI.Abstract.IUser _user; 
        private static Galatea.AI.Abstract.BaseTemplate _namedTemplate;
        private static Galatea.AI.Abstract.NamedEntity _namedEntity;

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
                //// Load Local Settings
                //if (System.IO.File.Exists("Gala.Dolly.Command.config"))
                //    AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", "Gala.Dolly.Command.exe.config");

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

                SerializedDataAccessManager dataAccessManager = new SerializedDataAccessManager(Settings.Default.DataAccessManagerConnectionString);
                dataAccessManager.RestoreBackup(@"..\..\..\Data\SerializedData.V1.dat");

                // Start Test Engine
                _engine = new TestEngine(debugger, dataAccessManager);
                _engine.User = _user;
                _engine.Startup();

                _engine.ExecutiveFunctions.ContextRecognition += ExecutiveFunctions_ContextRecognition;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
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
