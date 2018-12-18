using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Gala.Dolly.UI
{
    using Gala.Dolly.Properties;

    internal partial class SerialInterface : UserControl
    {
        internal ToolStripMenuItem viewSerialInterfaceMenuItem;
        internal ToolStripMenuItem serialToolStripMenuItem;
        internal ToolStripMenuItem serialOpenPortMenuItem;
        internal ToolStripMenuItem serialClosePortMenuItem;
        internal ToolStripMenuItem serialOpenAtStartupMenuItem;
        internal ToolStripMenuItem serialDisableWarnings;
        private ToolStripSeparator ts;

        public SerialInterface()
        {
            InitializeComponent();

            #region CA1303
            btnOpenPort.Text = Resources.SerialInterface_btnOpenPort_Text;
            btnClosePort.Text = Resources.SerialInterface_btnClosePort_Text;
            btnSend.Text = Resources.SerialInterface_btnSend_Text;
            lblInterval.Text = Resources.SerialInterface_lblInterval_Text;
            #endregion

            ToolStripMenuItem viewSerialInterfaceMenuItemTemp = null;
            ToolStripMenuItem serialOpenAtStartupMenuItemTemp = null;
            ToolStripMenuItem serialDisableWarningsTemp = null;

            //if (DesignMode) return;

            try
            {
                // Add View >> "Serial Interface" Menu Item
                viewSerialInterfaceMenuItemTemp = new ToolStripMenuItem(Resources.SerialInterface_viewSerialInterfaceMenuItem_Text);
                viewSerialInterfaceMenuItem = viewSerialInterfaceMenuItemTemp;
                viewSerialInterfaceMenuItem.CheckOnClick = true;
                viewSerialInterfaceMenuItem.Click += ViewSerialInterfaceMenuItem_Click;
                viewSerialInterfaceMenuItemTemp = null;

                // Add Serial >> Open Port, Close Port, and Open at Startup Menu and Menu Items
                serialOpenPortMenuItem = new ToolStripMenuItem(Resources.SerialInterface_serialOpenPortMenuItem_Text);
                serialClosePortMenuItem = new ToolStripMenuItem(Resources.SerialInterface_serialClosePortMenuItem_Text);
                serialOpenAtStartupMenuItemTemp = new ToolStripMenuItem(Resources.SerialInterface_serialOpenAtStartupMenuItem_Text);
                serialOpenAtStartupMenuItem = serialOpenAtStartupMenuItemTemp;
                serialOpenAtStartupMenuItem.CheckOnClick = true;
                serialOpenAtStartupMenuItemTemp = null;
                serialToolStripMenuItem = new ToolStripMenuItem(Resources.SerialInterface_serialToolStripMenuItem_Text);
                serialToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
                {
                    serialOpenPortMenuItem,
                    serialClosePortMenuItem,
                    new ToolStripSeparator(),
                    serialOpenAtStartupMenuItem
                });

                // Add Disable Warnings
                serialDisableWarningsTemp = new ToolStripMenuItem(Resources.SerialInterface_serialDisableWarnings_Text);
                serialDisableWarnings = serialDisableWarningsTemp;
                serialDisableWarnings.CheckOnClick = true;
                serialDisableWarnings.Click += SerialDisableWarnings_Click;
                serialDisableWarningsTemp = null;

                serialToolStripMenuItem.DropDownItems.Add(serialDisableWarnings);
            }
            catch
            {
                viewSerialInterfaceMenuItemTemp?.Dispose();
                serialOpenAtStartupMenuItemTemp?.Dispose();
                serialDisableWarningsTemp?.Dispose();
                throw;
            }           
        }

        internal new bool DesignMode
        {
            get
            {
                bool result = base.DesignMode;

                // Do this thing
                if (!result)
                    result = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);

                // Finalize
                return result;
            }
        }

        private void SerialInterface_Load(object sender, EventArgs e)
        {
            if (!Program.Started) return;
            Program.Engine.Debugger.Log(Galatea.Diagnostics.DebuggerLogLevel.Log, Resources.SerialPortLoading);

            txtInterval.Text = Settings.Default.SerialPortDefaultInterval.ToString(CultureInfo.CurrentCulture);
            txtInterval.KeyPress += Gala.Dolly.UI.EventHandlers.Numeric_KeyPress;

            // Add Menu Items to the Form at Runtime
            UI.BaseForm myForm = this.ParentForm as UI.BaseForm;

            ToolStripSeparator tsTemp = null;
            try
            {
                tsTemp = new ToolStripSeparator();
                ts = tsTemp;
                myForm.ToolsMenu.DropDownItems.Insert(0, ts);
                tsTemp = null;
            }
            catch
            {
                tsTemp.Dispose();
                throw;
            }

            myForm.ToolsMenu.DropDownItems.Insert(0, serialToolStripMenuItem);
            myForm.ViewMenu.DropDownItems.Add(viewSerialInterfaceMenuItem);

            // Initialize Robotics Serial Port
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();

            comboPorts.Items.AddRange(ports);
            string defaultDevice = Settings.Default.SerialPortDefaultDevice;
            if (ports.Contains(defaultDevice)) comboPorts.SelectedItem = defaultDevice;

            object[] baudRates = new object[] { 2400, 4800, 9600 };

            comboBaudRate.Items.AddRange(baudRates);
            int defaultBaudRate = Settings.Default.SerialPortDefaultBaudRate;
            if (baudRates.Contains(defaultBaudRate)) comboBaudRate.SelectedItem = defaultBaudRate;

            // Open the Port if the Default Settings is True
            if(Settings.Default.SerialOpenPortAtStartup)
            {
                OpenPort();
            }
            else
            {
                ClosePort();
            }

            // Finalize the UI 
            viewSerialInterfaceMenuItem.Checked = Settings.Default.WindowShowSerialInterface;
            serialOpenAtStartupMenuItem.Checked = Settings.Default.SerialOpenPortAtStartup;
            this.Visible = viewSerialInterfaceMenuItem.Checked;
        }

        private void SerialInterface_Disposed(object sender, System.EventArgs e)
        {
            // Save Settings
            if(Program.Engine != null)
            {
            Settings.Default.SerialPortDefaultInterval = Program.Engine.Machine.SerialPortController.WaitInterval;
            }

            Settings.Default.WindowShowSerialInterface = viewSerialInterfaceMenuItem.Checked;
            // TODO:  Why setting not saved???
            Settings.Default.SerialOpenPortAtStartup = serialOpenAtStartupMenuItem.Checked;

            if (comboPorts.SelectedItem != null)
            {
                Settings.Default.SerialPortDefaultDevice = comboPorts.SelectedItem.ToString();
            }
            if (comboBaudRate.SelectedItem != null)
            {
                Settings.Default.SerialPortDefaultBaudRate = Convert.ToInt32(comboBaudRate.SelectedItem, CultureInfo.CurrentCulture);
            }

            Properties.Settings.Default.Save();

            // Garbage Collection
            viewSerialInterfaceMenuItem = null;
        }

        #region Toolstrip Events

        private void ViewSerialInterfaceMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = viewSerialInterfaceMenuItem.Checked;
        }

        private void SerialDisableWarnings_Click(object sender, EventArgs e)
        {
            Program.Engine.Machine.SerialPortController.DisableWarning = serialDisableWarnings.Checked;
        }

        #endregion

        #region UI Events

        private void OpenPort(object sender, EventArgs e)
        {
            OpenPort();
        }

        private void ClosePort(object sender, EventArgs e)
        {
            ClosePort();
        }

        private void SendCommand(object sender, EventArgs e)
        {
            SendCommand();
        }

        #endregion

        private void TxtInterval_Validating(object sender, CancelEventArgs e)
        {
            TextBox ctl = (TextBox)sender;
            int value = Convert.ToInt32(ctl.Text, CultureInfo.CurrentCulture);

            if (value < Settings.Default.SerialPortIntervalMin || value > Settings.Default.SerialPortIntervalMax)
            {
                e.Cancel = true;

                // Prompt
                string msg = string.Format(CultureInfo.CurrentCulture, Resources.SerialPortInvalidInterval,
                    Settings.Default.SerialPortIntervalMin, Settings.Default.SerialPortIntervalMax);

                MessageBoxOptions options = (this.RightToLeft == RightToLeft.Yes) ?
                    MessageBoxOptions.RightAlign & MessageBoxOptions.RtlReading
                    : MessageBoxOptions.DefaultDesktopOnly;

                MessageBox.Show(msg, this.FindForm().Text, MessageBoxButtons.OK, MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1, options);
            }
        }
        private void TxtInterval_TextChanged(object sender, EventArgs e)
        {
            if (Program.Engine.Machine.SerialPortController != null)
            {
                Program.Engine.Machine.SerialPortController.WaitInterval = Convert.ToInt32(
                    ((TextBox)sender).Text, CultureInfo.CurrentCulture);
            }
        }

        private void OpenPort()
        {
            // Validate ComboBox Selections
            if (string.IsNullOrEmpty(comboPorts.Text) || string.IsNullOrEmpty(comboBaudRate.Text))
            {
                Program.Engine.Debugger.Log(Galatea.Diagnostics.DebuggerLogLevel.Warning,
                    Resources.SerialPortSelectPrompt);

                return;
            }

            // Open the Port
            Program.Engine.Machine.SerialPortController.OpenSerialPort(comboPorts.Text,
                Convert.ToInt32(comboBaudRate.Text, CultureInfo.CurrentCulture));

            // Set the Form
            if (Program.Engine.Machine.SerialPortController.IsSerialPortOpen)
                SetForm(true);
        }

        private void ClosePort()
        {
            // Close the Port
            Program.Engine.Machine.SerialPortController.CloseSerialPort();

            // Set the Form
            if (!Program.Engine.Machine.SerialPortController.IsSerialPortOpen)
                SetForm(false);
        }

        private void SendCommand()
        {
            if (string.IsNullOrEmpty(txtCommand.Text)) return;

            int cmd = Convert.ToInt32(txtCommand.Text, CultureInfo.CurrentCulture);
            Program.Engine.Machine.SerialPortController.SendCommand(cmd);
        }

        private void SetForm(bool open)
        {
            txtCommand.Focus();

            // Set Buttons
            btnOpenPort.Enabled = !open;
            btnClosePort.Enabled = open;
            btnSend.Enabled = open;

            // Set Menus
            serialOpenPortMenuItem.Enabled = !open;
            serialClosePortMenuItem.Enabled = open;
        }
    }
}
