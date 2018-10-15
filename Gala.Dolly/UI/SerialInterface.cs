using System;
using System.ComponentModel;
using System.Linq;
using System.IO.Ports;
using System.Windows.Forms;
using Galatea.AI.Robotics;

namespace Gala.Dolly.UI
{
    using Gala.Dolly.Properties;

    public partial class SerialInterface : UserControl
    {
        internal ToolStripMenuItem viewSerialInterfaceMenuItem;
        internal ToolStripMenuItem serialToolStripMenuItem;
        internal ToolStripMenuItem serialOpenPortMenuItem;
        internal ToolStripMenuItem serialClosePortMenuItem;
        internal ToolStripMenuItem serialOpenAtStartupMenuItem;
        internal ToolStripMenuItem serialDisableWarnings;

        public SerialInterface()
        {
            InitializeComponent();

            // Add View >> "Serial Interface" Menu Item
            viewSerialInterfaceMenuItem = new ToolStripMenuItem("&Serial Interface") { CheckOnClick = true };
            viewSerialInterfaceMenuItem.Click += ViewSerialInterfaceMenuItem_Click;

            // Add Serial >> Open Port, Close Port, and Open at Startup Menu and Menu Items
            serialOpenPortMenuItem = new ToolStripMenuItem("&Open Port");
            serialClosePortMenuItem = new ToolStripMenuItem("&Close Port");
            serialOpenAtStartupMenuItem = new ToolStripMenuItem("O&pen at Startup") { CheckOnClick = true };

            serialToolStripMenuItem = new ToolStripMenuItem("&Serial Interface");
            serialToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                serialOpenPortMenuItem,
                serialClosePortMenuItem,
                new ToolStripSeparator(),
                serialOpenAtStartupMenuItem });

            // Add Disable Warnings
            serialDisableWarnings = new ToolStripMenuItem("Disable Log Warnings") { CheckOnClick = true };
            serialDisableWarnings.Click += SerialDisableWarnings_Click;
            serialToolStripMenuItem.DropDownItems.Add(serialDisableWarnings);
        }

        private void SerialInterface_Load(object sender, EventArgs e)
        {
            if (!Program.Started) return;
            Program.Engine.Debugger.Log(Galatea.Diagnostics.DebuggerLogLevel.Log, Resources.SerialPortLoading);

            txtInterval.Text = Settings.Default.SerialPortDefaultInterval.ToString();
            txtInterval.KeyPress += Gala.Dolly.UI.EventHandlers.Numeric_KeyPress;

            // Add Menu Items to the Form at Runtime
            UI.BaseForm myForm = this.ParentForm as UI.BaseForm;
            myForm.ToolsMenu.DropDownItems.Insert(0, new ToolStripSeparator());
            myForm.ToolsMenu.DropDownItems.Insert(0, serialToolStripMenuItem);
            myForm.ViewMenu.DropDownItems.Add(viewSerialInterfaceMenuItem);

            // Initialize Robotics Serial Port
            string[] ports = SerialPort.GetPortNames();

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
            Settings.Default.SerialPortDefaultInterval = Program.Engine.Machine.SerialPortController.WaitInterval;
            Settings.Default.WindowShowSerialInterface = viewSerialInterfaceMenuItem.Checked;
            // TODO:  Why setting not saved???
            Settings.Default.SerialOpenPortAtStartup = serialOpenAtStartupMenuItem.Checked;

            if (comboPorts.SelectedItem != null)
                Settings.Default.SerialPortDefaultDevice = comboPorts.SelectedItem.ToString();
            if (comboBaudRate.SelectedItem != null)
                Settings.Default.SerialPortDefaultBaudRate = Convert.ToInt32(comboBaudRate.SelectedItem);

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

        private void txtInterval_Validating(object sender, CancelEventArgs e)
        {
            TextBox ctl = (TextBox)sender;
            int value = Convert.ToInt32(ctl.Text);

            if (value < Settings.Default.SerialPortIntervalMin || value > Settings.Default.SerialPortIntervalMax)
            {
                e.Cancel = true;

                // Prompt
                string msg = string.Format(Resources.SerialPortInvalidInterval,
                    Settings.Default.SerialPortIntervalMin, Settings.Default.SerialPortIntervalMax);

                MessageBox.Show(msg, this.FindForm().Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void txtInterval_TextChanged(object sender, EventArgs e)
        {
            if (Program.Engine.Machine.SerialPortController != null)
                Program.Engine.Machine.SerialPortController.WaitInterval = Convert.ToInt32(((TextBox)sender).Text);
        }

        private void OpenPort()
        {
            // Validate ComboBox Selections
            if (comboPorts.Text == "" || comboBaudRate.Text == "")
            {
                Program.Engine.Debugger.Log(Galatea.Diagnostics.DebuggerLogLevel.Warning,
                    Resources.SerialPortSelectPrompt);

                return;
            }

            // Open the Port
            Program.Engine.Machine.SerialPortController.OpenSerialPort(comboPorts.Text, Convert.ToInt32(comboBaudRate.Text));

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
            if (txtCommand.Text == "") return;

            int cmd = Convert.ToInt32(txtCommand.Text);
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
