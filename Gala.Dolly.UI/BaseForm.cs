using System.Windows.Forms;

namespace Gala.Dolly.UI
{
    using Gala.Dolly.UI.Runtime;

    /// <summary>
    /// Represents a window or dialog box containing controls pre-configured to run the 
    /// Galatea.NET framework.
    /// </summary>
    public partial class BaseForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Gala.Dolly.UI.BaseForm"/> class.
        /// </summary>
        public BaseForm() : base()
        {
            InitializeComponent();
            this.Icon = Galatea.IconResource.Galatea;

            this._debugger = new Diagnostics.UIDebugger();
            _debugger.LogLevel = Properties.Settings.Default.DebuggerLogLevel;
            _debugger.AlertLevel = Properties.Settings.Default.DebuggerAlertLevel;
            _debugger.ShowAlerts = Properties.Settings.Default.DebuggerShowAlerts;
            this._debugger.InitializeToolStrip(_toolsMenu);

            _current = this;
        }
        
        /// <summary>
        /// Gets a reference to the <see cref="Galatea.Runtime.IEngine.Debugger"/> instance, 
        /// with a UI tools menu to control settings.
        /// </summary>
        public Gala.Dolly.UI.Diagnostics.UIDebugger UIDebugger { get { return _debugger; } }

        internal ToolStripMenuItem FileMenu { get { return _fileMenu; } }
        internal ToolStripMenuItem ViewMenu { get { return _viewMenu; } }
        internal ToolStripMenuItem ToolsMenu { get { return _toolsMenu; } }
        internal static BaseForm Current { get { return _current; } }

        private void BaseForm_Load(object sender, System.EventArgs e)
        {
            bool startupHasErrors = false;

            // Validate Startup
            if (!Program.RuntimeEngine.DataAccessManager.IsInitialized)
            {
                _debugger.Log(Galatea.Diagnostics.DebuggerLogLevel.Error,
                    $"{Program.RuntimeEngine.DataAccessManager.ProviderName} " +
                        "did not initialize properly.", true);

                startupHasErrors = true;
            }

            if(startupHasErrors)
            {
                this.Close();
            }
        }

        private void BaseForm_Disposed(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("....BaseForm.Disposed....");
        }

        private Gala.Dolly.UI.Diagnostics.UIDebugger _debugger;
        private static BaseForm _current;
    }
}