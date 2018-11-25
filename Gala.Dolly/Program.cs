using System;
using System.Windows.Forms;

namespace Gala.Dolly
{
    using Galatea.AI.Imaging;

    internal static class Program
    {
        internal static SmartEngine Engine { get { return _engine; } }
        internal static UI.BaseForm BaseForm { get { return _baseForm; } set { _baseForm = value; } }
        internal static UI.IConsole Console { get { return _console; } }
        internal static bool Started { get { return _started; } }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += Application_ThreadException;

            /*
            // Load Local Settings
            Gala.Dolly.Properties.LocalSettingsHelper.Load(localSettingsFile);
             */

            // Start UI
            TemplateRecognitionForm form = new TemplateRecognitionForm();
            _baseForm = form;
            _baseForm.UIDebugger.FileLogger = new Gala.Dolly.UI.Diagnostics.FileLogger();
            _baseForm.UIDebugger.FileLogger.StartLogging(Properties.Settings.Default.LogFileName, System.IO.FileMode.Append);

            _console = form.Console;

            // Start Galatea Robotics Engine
            Program.Startup();

            Application.Run(_baseForm);

            // Shutdown Galatea Robotics Engine
            Program.Shutdown();
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            if (e.Exception is Galatea.TeaException)
            {
                _engine.Debugger.HandleTeaException(e.Exception as Galatea.TeaException, null);
            }
            else
            {
                _engine.Debugger.ThrowSystemException(e.Exception, null);
            }

            // Notify UI 
            _console.SendResponse(Engine.Debugger.ErrorMessage);
            _engine.Debugger.ClearError();
        }

        internal static void Startup()
        {
            /*
            // Load Settings
            Gala.Dolly.Properties.Settings.Default.ImagingSettings = Galatea.AI.Imaging.ImagingSettings.Create();

            // TODO: Use settings and get rid of "Create()" function
            Galatea.AI.Imaging.Properties.Settings.Default.ImagingSettings = Properties.Settings.Default.ImagingSettings;
             */

            // Initialize Robotics Engine
            _engine = new SmartEngine(_baseForm.UIDebugger);
            _engine.Startup();

            Gala.Dolly.UI.Runtime.Program.Startup(_engine);

            // Load UI Settings
            _engine.Machine.SerialPortController.WaitInterval = Properties.Settings.Default.SerialPortDefaultInterval;
            _engine.Machine.SerialPortController.DisableWarning = Properties.Settings.Default.SerialPortDisableWarning;

            // Enable Context and Response Logging
            _engine.ExecutiveFunctions.ContextLogging = true;
            _engine.ExecutiveFunctions.ResponseLogging = true;


            // Finalize
            bool started = false;

            if (_engine.DataAccessManager.IsInitialized)
            {
                started = true;
            }

            // Finalize
            _started = started;
        }

        internal static void ShutdownUI()
        {
            Properties.Settings.Default.ImagingSettings = VisualProcessor.ImagingSettings;
            Properties.Settings.Default.Save();

            /*
            Properties.LocalSettingsHelper.Save(localSettingsFile);
             */

            _baseForm.UIDebugger.LogLevel = Galatea.Diagnostics.DebuggerLogLevel.Log;
        }

        internal static void Shutdown()
        {
            Gala.Dolly.UI.Runtime.Program.Shutdown();

            // Save Settings
            Properties.Settings.Default.SpeechIsSilent = Engine.AI.LanguageModel.SpeechModule.StaySilent;

            // Shutdown AI Engine
            _engine.Shutdown();

            // Save Settings and shit
            ShutdownUI();

            _started = false;
        }

        //private const string localSettingsFile = @"..\..\..\Local Settings\LocalSettings.config";
        private static UI.BaseForm _baseForm;
        private static UI.IConsole _console;
        private static SmartEngine _engine;
        private static bool _started;
    }
}