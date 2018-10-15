using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;

namespace Gala.Dolly.Test
{
    using Galatea;
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
        public Galatea.AI.Abstract.IUser User { get { return _user; } }
        public Galatea.AI.Abstract.BaseTemplate NamedTemplate { get { return _namedTemplate; } }
        public Galatea.AI.Abstract.NamedEntity NamedEntity { get { return _namedEntity; } }

        #region IProvider
        string IProvider.ProviderID
        {
            get
            {
                return this.GetType().FullName;
            }
        }
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

        private ISite _site;
        #endregion

        [AssemblyInitialize]
        public static void Initialize(TestContext testContext)
        {
            try
            {
                // Initialize Runtime
                _user = new Galatea.Runtime.Services.User("Test");
                Galatea.Diagnostics.IDebugger debugger = new Gala.Dolly.Test.TestDebugger();
                debugger.LogLevel = Galatea.Diagnostics.DebuggerLogLevel.Diagnostic;

                SerializedDataAccessManager dataAccessManager = new SerializedDataAccessManager(Settings.Default.DataAccessManagerConnectionString);
                dataAccessManager.RestoreBackup(@"..\..\..\Data\SerializedData.V1.dat");

                _engine = new TestEngine(debugger, dataAccessManager);
                _engine.User = _user;
                _engine.Startup();

                _engine.ExecutiveFunctions.ContextRecognition += ExecutiveFunctions_ContextRecognition;

                // Suppress Timeout
                Galatea.AI.Imaging.Properties.Settings.Default.SuppressTimeout = true;
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
        }
    }
}
