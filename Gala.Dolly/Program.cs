using System;
using System.Windows.Forms;

namespace Gala.Dolly
{
    using Galatea.AI.Imaging;
    using Galatea.Diagnostics;
    using Galatea.Runtime;
    using Gala.Dolly.UI.Properties;

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
            
            Gala.Dolly.UI.Properties.Settings uiSettings = Gala.Dolly.UI.Properties.Settings.Default;

            #region // This is only temporary
            // TODO:
            /*
            uiSettings.DebuggerLogLevel = DebuggerLogLevel.Diagnostic;
            uiSettings.DebuggerAlertLevel = DebuggerLogLevel.Message;
            uiSettings.Save();

            ImagingSettings imagingSettings = new ImagingSettings
            {
                Timeout = 2000,
                SuppressTimeout = true,
            };

            imagingSettings.ColorStatsSettings.StatisticalAnalysisTypes = Galatea.AI.Math.StatsTypes.Mean;
            imagingSettings.MonochromeBlobFilterSettings.ContrastCorrectionFactor = 0;
            imagingSettings.MonochromeBlobFilterSettings.AdaptiveSmoothingFactor = 0.25;
            imagingSettings.MonochromeBlobFilterSettings.FrameWidth = 10;
            imagingSettings.BlobPointSettings.LineSegmentThreshold = 20;
            imagingSettings.BlobPointSettings.LineAngleThreshold = 10;
            imagingSettings.BlobPointSettings.CurveAngleThreshold = 27;

            imagingSettings.TemplateRecognitionSettings.ColorBrightnessThreshold = 5;
            imagingSettings.TemplateRecognitionSettings.ColorSaturationThreshold = 5;
            imagingSettings.TemplateRecognitionSettings.ShapeOblongThreshold = 1.75M;
            imagingSettings.TemplateRecognitionSettings.ShapeOblongRecognitionNormalization = true;
            imagingSettings.TemplateRecognitionSettings.IdentifyShapeCertaintyMinimum = 65;
            Properties.Settings.Default.ImagingSettings = imagingSettings;
            Properties.Settings.Default.Save();
             */
            #endregion

            Galatea.Diagnostics.DebuggerLogLevelSettings.Initialize(uiSettings.DebuggerLogLevel, uiSettings.DebuggerAlertLevel);

            // Start UI
            TemplateRecognitionForm form = new TemplateRecognitionForm();
            _baseForm = form;
            _baseForm.UIDebugger.FileLogger = new Gala.Dolly.UI.Diagnostics.FileLogger();
            _baseForm.UIDebugger.FileLogger.StartLogging(Properties.Settings.Default.LogFileName, System.IO.FileMode.Append);

            _console = form.Console;

            // Start Galatea Robotics Engine
            Program.Startup();

            if (Program.Started)
            {
                Application.Run(_baseForm);
            }

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
            // Suppress alerts during startup
            bool showAlertsConfig = _baseForm.UIDebugger.ShowAlerts;
            _baseForm.UIDebugger.ShowAlerts = false;

            // Initialize Robotics Engine
            try
            {
                _engine = new SmartEngine(_baseForm.UIDebugger);
                _engine.Startup();

                Gala.Dolly.UI.Runtime.Program.Startup(_engine);
            }
            catch (Exception ex)
            {
                _started = false;
                _baseForm.UIDebugger.ThrowSystemException(ex, _engine);

                return;
            }

            // Load UI Settings
            _engine.Machine.SerialPortController.WaitInterval = Properties.Settings.Default.SerialPortDefaultInterval;
            _engine.Machine.SerialPortController.DisableWarning = Properties.Settings.Default.SerialPortDisableWarning;

            // Enable Context and Response Logging
            _engine.ExecutiveFunctions.ContextLogging = true;
            _engine.ExecutiveFunctions.ResponseLogging = true;

            // Reset alerts
            _baseForm.UIDebugger.ShowAlerts = showAlertsConfig;

            // Finalize
            bool started = false;

            if (SmartEngine.DataAccessManager.IsInitialized)
            {
                started = true;
            }

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

            if (_engine != null)
            {
                // Save Settings
                if (SmartEngine.AI?.LanguageModel?.SpeechModule != null)
                {
                    Properties.Settings.Default.SpeechIsSilent = SmartEngine.AI.LanguageModel.SpeechModule.StaySilent;
                }
                Properties.Settings.Default.Save();

                // Shutdown AI Engine
                _engine.Shutdown();
            }

            // Save Settings and shit
            ShutdownUI();

            _started = false;
        }

        private static UI.BaseForm _baseForm;
        private static UI.IConsole _console;
        private static SmartEngine _engine;
        private static bool _started;
    }
}