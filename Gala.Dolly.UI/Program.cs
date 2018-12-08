using Galatea.Runtime;

namespace Gala.Dolly.UI.Runtime
{
    internal static class Program
    {
        /// <summary>
        /// Sets Runtime settings from the <see cref="Gala.Dolly.UI.Properties.Settings"/> instance.
        /// </summary>
        /// <param name="runtimeEngine">
        /// The running of the <see cref="Galatea.Runtime.IRuntimeEngine"/> instance.
        /// </param>
        public static void Startup(IRuntimeEngine runtimeEngine)
        {
            _runtimeEngine = runtimeEngine;

            // Set Runtime Properties
            BaseForm.Current.UIDebugger.LogLevel = Gala.Dolly.UI.Properties.Settings.Default.DebuggerLogLevel;
            BaseForm.Current.UIDebugger.AlertLevel = Gala.Dolly.UI.Properties.Settings.Default.DebuggerAlertLevel;
            BaseForm.Current.UIDebugger.ShowAlerts = Gala.Dolly.UI.Properties.Settings.Default.DebuggerShowAlerts;

            if (Program.RuntimeEngine == null ||
                    Program.RuntimeEngine.AI == null ||
                    Program.RuntimeEngine.AI.LanguageModel == null ||
                    Program.RuntimeEngine.AI.LanguageModel.SpeechModule == null)
            {
                BaseForm.Current.UIDebugger.SpeechIsSilent = true;
                BaseForm.Current.UIDebugger.SpeechMenuEnabled = false;
                return;
            }

            BaseForm.Current.UIDebugger.SpeechIsSilent = Program.RuntimeEngine.AI.LanguageModel.SpeechModule.StaySilent;
            _started = true;
        }
        /// <summary>
        /// Saves the runtime settings to the <see cref="Gala.Dolly.UI.Properties.Settings"/> instance.
        /// </summary>
        public static void Shutdown()
        {
            Gala.Dolly.UI.Properties.Settings.Default.DebuggerLogLevel = BaseForm.Current.UIDebugger.LogLevel;
            Gala.Dolly.UI.Properties.Settings.Default.DebuggerAlertLevel = BaseForm.Current.UIDebugger.AlertLevel;
            Gala.Dolly.UI.Properties.Settings.Default.DebuggerShowAlerts = BaseForm.Current.UIDebugger.ShowAlerts;
            Gala.Dolly.UI.Properties.Settings.Default.Save();
        }

        internal static IRuntimeEngine RuntimeEngine { get { return _runtimeEngine; } }

        internal static StartupStatus StartupStatus { get; set; }
        //internal static TeaInitializationException InitializationException { get; set; }

        internal static bool Started { get { return _started; } }

        static IRuntimeEngine _runtimeEngine;
        static bool _started;
    }
}
