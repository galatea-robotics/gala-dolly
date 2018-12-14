using System.Globalization;
using System.Windows.Forms;

namespace Gala.Dolly.UI
{
    using Galatea.Globalization;
    using Gala.Dolly.UI.Runtime;
    using Properties;

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

            #region CA1303
            menuStrip.Text = Resources.BaseForm_menuStrip_Text;
			FileMenu.Text = Resources.BaseForm_FileMenu_Text;
			exitMenuItem.Text = Resources.BaseForm_ExitMenuItem_Text;
			ViewMenu.Text = Resources.BaseForm_viewMenu_Text;
			ToolsMenu.Text = Resources.BaseForm_toolsMenu_Text;
			#endregion

			this.Icon = Galatea.IconResource.Galatea;
            this.Disposed += BaseForm_Disposed;

            this._debugger = new Diagnostics.UIDebugger();
            _debugger.LogLevel = Settings.Default.DebuggerLogLevel;
            _debugger.AlertLevel = Settings.Default.DebuggerAlertLevel;
            _debugger.ShowAlerts = Settings.Default.DebuggerShowAlerts;
            this._debugger.InitializeToolStrip(_toolsMenu);

            _current = this;
        }

        internal new bool DesignMode
        {
            get
            {
                bool result = base.DesignMode;

                // Do this thing
                if (!result)
                    result = (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime);

                // Finalize
                return result;
            }
        }

        /// <summary>
        /// Gets a reference to the <see cref="Galatea.Runtime.IEngine.Debugger"/> instance, 
        /// with a UI tools menu to control settings.
        /// </summary>
        public Gala.Dolly.UI.Diagnostics.UIDebugger UIDebugger { get { return _debugger; } }

        internal virtual UI.IConsole Console { get; set; }

        internal ToolStripMenuItem FileMenu { get { return _fileMenu; } }
        internal ToolStripMenuItem ViewMenu { get { return _viewMenu; } }
        internal ToolStripMenuItem ToolsMenu { get { return _toolsMenu; } }
        internal static BaseForm Current { get { return _current; } }

        private void BaseForm_Load(object sender, System.EventArgs e)
        {
            if (!DesignMode)
            {
                ValidateStartup();
            }
        }

        private void ValidateStartup()
        {
            bool isInitialized = false;

            // Validate Startup
            bool showAlertsConfig = _debugger.ShowAlerts;
            _debugger.ShowAlerts = true;

            string providerName = "";

            if(Program.RuntimeEngine == null)
            {
                providerName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            }
            else if(Program.RuntimeEngine.DataAccessManager == null)
            {
                providerName = Program.RuntimeEngine.ProviderName;
            }
            else if (!Program.RuntimeEngine.DataAccessManager.IsInitialized)
            {
                providerName = Program.RuntimeEngine.DataAccessManager.ProviderName;
            }
            else
            {
                isInitialized = true;
            }

            if(!isInitialized)
            {
                string msg = string.Format(CultureInfo.CurrentCulture,
                        DiagnosticResources.Runtime_Component_Initialization_Failed, 
                        providerName);

                _debugger.Log(Galatea.Diagnostics.DebuggerLogLevel.Error, msg, true, false);

                this.Close();
            }

            _debugger.ShowAlerts = showAlertsConfig;
        }

        private void BaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Gala.Dolly.UI.Properties.Settings.Default.DebuggerShowAlerts = _debugger.ShowAlerts;

        }

        private void BaseForm_Disposed(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("....BaseForm.Disposed....");
        }

        private Gala.Dolly.UI.Diagnostics.UIDebugger _debugger;
        private static BaseForm _current;
    }
}