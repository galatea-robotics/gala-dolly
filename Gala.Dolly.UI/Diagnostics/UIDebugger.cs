﻿using System;
using System.Globalization;
using System.Windows.Forms;
using Galatea.Runtime.Services;
using Galatea.Diagnostics;

namespace Gala.Dolly.UI.Diagnostics
{
    using Gala.Dolly.UI.Runtime;

    /// <summary>
    /// Includes a <see cref="ToolStripMenuItem"/> User Interface for setting diagnostic application 
    /// properties, as well as File Logging and methods for Handling Errors instead of simply outputting 
    /// the stack trace and then re-throwing.
    /// </summary>
    public partial class UIDebugger : Gala.Dolly.UI.Diagnostics.Debugger, IDebugger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Gala.Dolly.UI.Diagnostics.UIDebugger"/> component.
        /// </summary>
        public UIDebugger() : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the threshold to determine how messages are logged.
        /// </summary>
        public override DebuggerLogLevel LogLevel
        {
            get
            {
                return base.LogLevel;
            }
            set
            {
                foreach (ToolStripMenuItem item in new[]{
                    debugDiagnosticMenuItem, debugLogMenuItem, debugEventMenuItem, debugMsgMenuItem})
                {
                    item.Checked = false;
                }

                switch (value)
                {
                    case DebuggerLogLevel.Diagnostic:
                        this.debugDiagnosticMenuItem.Checked = true;
                        break;
                    case DebuggerLogLevel.Log:
                        this.debugLogMenuItem.Checked = true;
                        break;
                    case DebuggerLogLevel.Event:
                        this.debugEventMenuItem.Checked = true;
                        break;
                    default:
                        this.debugMsgMenuItem.Checked = true;
                        break;
                }

                base.LogLevel = value;
            }
        }
        /// <summary>
        /// Gets or sets the threshold to determine when the user receives a <see cref="MessageBox"/> alert.
        /// </summary>
        public DebuggerLogLevel AlertLevel
        {
            get { return _alertLevel; }
            set { _alertLevel = value; }
        }
        /// <summary>
        /// Gets or sets a boolean that determines if the <see cref="MessageBox"/> should alert the user.
        /// </summary>
        public bool ShowAlerts
        {
            get { return debugShowAlertsMenuItem.Checked; }
            set { debugShowAlertsMenuItem.Checked = value; }
        }
        /// <summary>
        /// Logs messages and errors to a log file using a <see cref="IFileLogger"/> instance.
        /// </summary>
        /// <param name="level">
        /// The <see cref="DebuggerLogLevel"/> of the message to be logged.
        /// </param>
        /// <param name="message"> The message to be logged. </param>
        /// <param name="overrideLevel"> 
        /// A boolean value indicating that the UIDebugger should log the 
        /// message, regardless of <see cref="DebuggerLogLevel"/>.
        /// </param>
        /// <param name="suppressPopup">
        /// A boolean value indicating that the UIDebugger should ignore the 
        /// <see cref="MessageBox"/> popup.
        /// </param>
        public void Log(DebuggerLogLevel level, string message, bool overrideLevel, bool suppressPopup)
        {
            this.Log(level, message, overrideLevel);

            // Show Alerts
            if (debugShowAlertsMenuItem.Checked && level >= _alertLevel)
            {
                if (!suppressPopup)
                    ShowAlert(level, message);
            }
        }

        #region UI

        internal void InitializeToolStrip(ToolStripMenuItem container)
        {
            // Create the Menu Items
            this.debugDiagnosticMenuItem.Checked = LogLevel == DebuggerLogLevel.Diagnostic;
            this.debugLogMenuItem.Checked = (LogLevel == DebuggerLogLevel.Log);
            this.debugEventMenuItem.Checked = (LogLevel == DebuggerLogLevel.Event);
            this.debugMsgMenuItem.Checked = (LogLevel >= DebuggerLogLevel.Message);
            this.debugShowAlertsMenuItem.CheckOnClick = true;

            // Initialize Events
            debugDiagnosticMenuItem.Click += DebugStripMenuItem_Click;
            debugLogMenuItem.Click += DebugStripMenuItem_Click;
            debugEventMenuItem.Click += DebugStripMenuItem_Click;
            debugMsgMenuItem.Click += DebugStripMenuItem_Click;

            speechSilentMenuItem.CheckOnClick = true;
            speechSilentMenuItem.Click += SpeechSilentMenuItem_Click;

            // Add the Menu to the Application
            container.DropDownItems.Add(this.debugMenu);
            container.DropDownItems.Add(this.speechMenu);
        }

        private void SpeechSilentMenuItem_Click(object sender, EventArgs e)
        {
            Program.RuntimeEngine.AI.LanguageModel.SpeechModule.StaySilent = speechSilentMenuItem.Checked;
        }

        private void DebugStripMenuItem_Click(object sender, System.EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            DebuggerLogLevel level = DebuggerLogLevel.Log;

            // Only one Toolstrip item checked at a time
            this.debugDiagnosticMenuItem.Checked = false;
            this.debugLogMenuItem.Checked = false;
            this.debugEventMenuItem.Checked = false;
            this.debugMsgMenuItem.Checked = false;
            item.Checked = true;

            // Update the debugger
            if (item.Equals(debugDiagnosticMenuItem)) level = DebuggerLogLevel.Diagnostic;
            if (item.Equals(debugEventMenuItem)) level = DebuggerLogLevel.Event;
            if (item.Equals(debugMsgMenuItem)) level = DebuggerLogLevel.Message;

            base.LogLevel = level;
        }

        #endregion

        internal bool SpeechIsSilent
        {
            get { return speechSilentMenuItem.Checked; }
            set { speechSilentMenuItem.Checked = value; }
        }

        internal bool SpeechMenuEnabled
        {
            get { return speechMenu.Enabled; }
            set
            {
                speechMenu.Enabled = value;
                speechSilentMenuItem.Enabled = value;
            }
        }

        private void ShowAlert(DebuggerLogLevel level, string message)
        {
            MessageBoxIcon mbIcon;

            switch (level)
            {
                case DebuggerLogLevel.Critical:
                    mbIcon = MessageBoxIcon.Stop;
                    break;
                case DebuggerLogLevel.Error:
                    mbIcon = MessageBoxIcon.Error;
                    break;
                case DebuggerLogLevel.Warning:
                    mbIcon = MessageBoxIcon.Warning;
                    break;
                case DebuggerLogLevel.Message:
                    mbIcon = MessageBoxIcon.Exclamation;
                    break;
                default:
                    mbIcon = MessageBoxIcon.Information;
                    break;
            }

            #region Code Analysis

            MessageBoxOptions mbOptions;

            if (Container is ContainerControl && (Container as ContainerControl).RightToLeft == RightToLeft.Yes)
            {
                mbOptions = MessageBoxOptions.RightAlign & MessageBoxOptions.RtlReading;
            }
            else
            {
                mbOptions = MessageBoxOptions.DefaultDesktopOnly;
            }
            #endregion

            MessageBox.Show(message, Properties.Settings.Default.ApplicationTitle,
                MessageBoxButtons.OK, mbIcon, MessageBoxDefaultButton.Button1, mbOptions);
        }

        private DebuggerLogLevel _alertLevel;
    }
}
