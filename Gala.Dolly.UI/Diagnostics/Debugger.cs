using System;
using System.Windows.Forms;

namespace Gala.Dolly.UI.Diagnostics
{ 
    using Galatea;
    using Galatea.Diagnostics;
    using Galatea.Globalization;
    using Galatea.Runtime.Services;

    /// <summary>
    /// Includes File Logging and methods for Handling Errors instead of simply outputting
    /// the stack trace and then re-throwing.
    /// </summary>
    [CLSCompliant(false)]
    public partial class Debugger : Galatea.Runtime.Services.Debugger, IDebugger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Gala.Dolly.UI.Diagnostics.Debugger"/> class.
        /// </summary>
        public Debugger()
        {
            InitializeComponent();
        }

        /*
        public Debugger() : base(Properties.Settings.Default.DebuggerLogLevel, Properties.Settings.Default.DebuggerAlertLevel)
        {
            InitializeComponent();
        }
         */


        /// <summary>
        /// Gets or sets a <see cref="FileLogger"/> component reference.
        /// </summary>
        public virtual IFileLogger FileLogger
        {
            get { return _fileLogger; }
            set { _fileLogger = value; }
        }
        /// <summary>
        /// Handles expected Galatea Core Exceptions, typically by logging them.
        /// </summary>
        /// <param name="ex">
        /// A run-time <see cref="TeaException"/>.
        /// </param>
        /// <param name="provider">
        /// The runtime component where the exception occurred.
        /// </param>
        public override void HandleTeaException(TeaException ex, IProvider provider)
        {
            HandleTeaException(ex, provider, false);
        }

        /// <param name="ex">
        /// A run-time <see cref="TeaException"/>.
        /// </param>
        /// <param name="provider">
        /// The runtime component where the exception occurred.
        /// </param>
        /// <param name="throwException">
        /// A value indicating if the Exception should remain unhandled after
        /// processing by the <see cref="IDebugger"/> instance.
        /// </param>
        public override void HandleTeaException(TeaException ex, IProvider provider, bool throwException)
        {
            if (ex == null) return;

            string msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            Log(DebuggerLogLevel.Error, msg);
            Log(DebuggerLogLevel.StackTrace, ex.StackTrace, true);

            string errorMessage = provider != null 
                ? string.Format(System.Globalization.CultureInfo.CurrentCulture,
                    DiagnosticResources.Debugger_Error_Speech_Message_Format,
                    provider.ProviderName) 
                : DiagnosticResources.Debugger_Error_Speech_Message;

            if (msg.Substring(0, 17) != "Exception of type") errorMessage += "  " + msg;

            this.Exception = ex;
            this.ErrorMessage = errorMessage;
        }
        /// <summary>
        /// Handles unexpected System Errors, typically by logging them, and then
        /// re-throwing them.
        /// </summary>
        /// <param name="ex">
        /// A run-time <see cref="System.Exception"/>.
        /// </param>
        /// <param name="provider">
        /// The runtime component where the exception occurred.
        /// </param>
        public override void ThrowSystemException(Exception ex, IProvider provider)
        {
            if (ex == null) return;

            Log(DebuggerLogLevel.Critical, ex.Message);
            Log(DebuggerLogLevel.StackTrace, ex.StackTrace, true);

            this.Exception = ex;
            this.ErrorMessage = provider != null
                ? string.Format(System.Globalization.CultureInfo.CurrentCulture,
                    DiagnosticResources.Debugger_Error_Unexpected_Speech_Message_Format,
                    provider.ProviderName)
                : DiagnosticResources.Debugger_Error_Unexpected_Speech_Message;
        }

        /// <summary>
        /// Logs messages and errors to a log file using a <see cref="IFileLogger"/> instance.
        /// </summary>
        /// <param name="level">
        /// The <see cref="DebuggerLogLevel"/> of the message to be logged.
        /// </param>
        /// <param name="message"> The message to be logged. </param>
        /// <param name="overrideLevel"> 
        /// A boolean value indicating that the Debugger should log the 
        /// message, regardless of <see cref="DebuggerLogLevel"/>.
        /// </param>
        public override void Log(DebuggerLogLevel level, string message, bool overrideLevel)
        {
            if (level >= this.LogLevel || overrideLevel)
            {
                string sOutput = string.Format(
                    System.Globalization.CultureInfo.InvariantCulture,
                    DiagnosticResources.Debugger_Log_Message_Format,
                    GetLogLevelToken(level), DateTime.Now, message);

                // Output to log file
                System.Diagnostics.Debug.WriteLine(sOutput);

                if (_fileLogger == null) return;

                if (IsInitialized && _fileLogger.IsLogging)
                {
                    _fileLogger.Log(sOutput);
                }
            }
        }
        /// <summary>
        /// Logs messages and errors.
        /// </summary>
        /// <param name="level">
        /// The <see cref="DebuggerLogLevel"/> of the message to be logged.
        /// </param>
        /// <param name="message"> The message to be logged. </param>
        public override void Log(DebuggerLogLevel level, string message)
        {
            this.Log(level, message, false);
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            _fileLogger?.Dispose();
            _fileLogger = null;

            base.Dispose(disposing);
        }

        /// <summary>
        /// Gets a string representing a <see cref="DebuggerLogLevel"/> value that
        /// is visible to the user.
        /// </summary>
        /// <param name="level">
        /// The <see cref="DebuggerLogLevel"/> to tokenize.
        /// </param>
        /// <returns>
        /// A string representing a <see cref="DebuggerLogLevel"/> value that is visible 
        /// to the user.
        /// </returns>
        protected override string GetLogLevelToken(DebuggerLogLevel level)
        {
            switch (level)
            {
                case DebuggerLogLevel.Diagnostic: return " ^^^ ";
                case DebuggerLogLevel.Log: return " Log ";
                case DebuggerLogLevel.Event: return "Event";
                case DebuggerLogLevel.Message: return " Msg ";
                case DebuggerLogLevel.Warning: return "Warn ";
                case DebuggerLogLevel.Error: return "Error";
                case DebuggerLogLevel.Critical: return "*ERR*";
                case DebuggerLogLevel.StackTrace: return "TRACE";
                default: throw new TeaArgumentException();
            }
        }

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		private void InitializeComponent()
        {
        }

        #endregion

        private IFileLogger _fileLogger;
    }
}
