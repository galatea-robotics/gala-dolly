using System;
using System.Collections.Generic;
using System.Globalization;
using System.Drawing;
using System.Windows.Forms;

namespace Gala.Dolly.UI
{
    using Galatea;
    using Galatea.Runtime;
    using Gala.Dolly.UI.Runtime;

    /// <summary>
    /// Represents a UI Consule for input to and output from a Chatbot.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Chatbot")]
    public sealed partial class ChatbotControl : UserControl, IConsole, IProvider
    {
        private ToolStripMenuItem viewChatbotsMenuItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatbotControl"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "Chatbot")]
        public ChatbotControl() : base()
        {
            InitializeComponent();

			#region CA1303
			lblChatbotSelector.Text = Properties.Resources.ChatbotControl_lblChatBotSelector_Text;
            radDefault.Text = Properties.Resources.ChatbotControl_radDefault_Text;
            btnSend.Text = Properties.Resources.ChatbotControl_btnSend_Text;
			#endregion

			// Initialize Chatbot Buttons
			pnlChatbotSelector.Controls.Add(radDefault);

            // Do Menu Item
            ToolStripMenuItem tempMenuItem = null;

            try
            {
                tempMenuItem = new ToolStripMenuItem();

                viewChatbotsMenuItem = tempMenuItem;
                viewChatbotsMenuItem.Text = Properties.Resources.ChatbotControl_viewChatbotsMenuItem_Text;
                viewChatbotsMenuItem.CheckOnClick = true;
            }
            catch
            {
                tempMenuItem.Dispose();
                throw;
            }

            this.Load += ChatbotControl_Load;
        }

        private void ChatbotControl_Load(object sender, EventArgs e)
        {
            viewChatbotsMenuItem.Checked = this.ChatbotButtonsVisible;
            viewChatbotsMenuItem.Click += ViewChatbotsMenuItem_Click;

            ((BaseForm)FindForm()).ViewMenu.DropDownItems.Add(viewChatbotsMenuItem);
        }
        private void ViewChatbotsMenuItem_Click(object sender, EventArgs e)
        {
            ChatbotButtonsVisible = viewChatbotsMenuItem.Checked;
        }

        /// <summary>
        /// Adds the <see cref="IChatbot"/> instances contained in an <see cref="IChatbotManager"/>
        /// collection to the User Interface.
        /// </summary>
        /// <param name="chatbots">
        /// A collection of <see cref="IChatbot"/> instances.
        /// </param>
        [CLSCompliant(false)]
        public void InitializeChatbots(IChatbotManager chatbots)
        {
            if (chatbots == null) throw new TeaArgumentNullException(nameof(chatbots));

            // Initialize Radio Buttons
            int x = 153;
            foreach (IChatbot chatbot in chatbots)
            {
                // Validate the chatbot
                if (string.IsNullOrEmpty(chatbot.Greeting))
                    throw new TeaArgumentNullException(nameof(chatbot.Greeting));

                // Do UI
                RadioButton rb = null;
                try
                {
                    rb = new RadioButton();
                    pnlChatbotSelector.Controls.Add(rb);
                    rb.Text = chatbot.Name.ToUpper(CultureInfo.CurrentCulture);
                    rb.Name = chatbot.Name;
                    rb.CheckedChanged += RadChatbot_CheckedChanged;

                    // Placement
                    pnlChatbotSelector.Controls.Add(rb);

                    rb.Location = new Point(x, radDefault.Location.Y);
                    rb.AutoSize = true;
                    x += rb.Width + 3;

                    rb = null;
                }
                catch
                {
                    rb.Dispose();
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets or sets a boolean value determining if the Chatbot selector buttons are visible.
        /// </summary>
        public bool ChatbotButtonsVisible
        {
            get
            {
                return pnlChatbotSelector.Visible;
            }
            set
            {
                pnlChatbotSelector.Visible = value;
            }
        }

        string IProvider.ProviderId => "Gala.Dolly.UI.ChatbotControl";
        string IProvider.ProviderName => "ChatbotControl";

        void IConsole.SendResponse(string response)
        {
            SendResponse(response);
        }
        bool IConsole.IsSilent
        {
            get { return _consoleIsSilent; }
            set { _consoleIsSilent = value; }
        }

        internal void SendResponse(string response)
        {
            #region Speak
            this.Cursor = Cursors.WaitCursor;

            if (!_consoleIsSilent && Program.RuntimeEngine != null)
            {
            Galatea.Speech.ISpeechModule speechModule = Program.RuntimeEngine.AI.LanguageModel.SpeechModule;

            if (speechModule != null && !speechModule.StaySilent)
                    speechModule.TextToSpeech.Speak(response, this);
            }

            this.Cursor = Cursors.Default;
            #endregion

            responseText = response;
            DisplayResponse();
        }
        internal void GetResponse()
        {
            // Get and display input 
            string inputText = this.txtInput.Text.Trim();

            // Get Response
            if (!string.IsNullOrEmpty(inputText))
            {
                string sender = Program.RuntimeEngine != null ?
                    Program.RuntimeEngine.User.Name.ToUpper(CultureInfo.CurrentCulture)
                    : "USER";

                string msg = string.Format(CultureInfo.CurrentCulture,
                    Chatbots.Properties.Settings.Default.ChatbotMessageFormat,
                    sender, inputText);

                this.txtDisplay.AppendText(msg + "\r\n");

                // Save input to short term UI History
                history.Add(inputText);
                historyLine = -1;

                // Get response from input
                this.Cursor = Cursors.WaitCursor;
                responseText = Program.RuntimeEngine.ExecutiveFunctions.GetResponse(Program.RuntimeEngine.AI.LanguageModel, inputText);
                this.Cursor = Cursors.Default;

                // Display response
                Timer.Interval = waitTime;
                Timer.Start();
            }

            this.txtInput.Clear();
            this.txtInput.Focus();
        }
        internal void DisplayResponse()
        {
            if (!responseText.Contains("No match found"))
            {
                //LunaPOC.SerialInterface.Wait(240)     ' Don't talk over the Human!

                string msg = string.Format(CultureInfo.CurrentCulture,
                    Chatbots.Properties.Settings.Default.ChatbotMessageFormat,
                    Program.RuntimeEngine.AI.LanguageModel.ChatbotManager.Current.Name.ToUpper(CultureInfo.CurrentCulture), 
                    responseText);

                this.txtDisplay.AppendText(msg + "\r\n");

                //Me.TTSInterface.Speak(response)
            }
            else
            {
                //Me.TTSInterface.txtSpeech.Text = response
                //Me.TTSInterface.LEDComponent.SetLED(5, True)
            }
        }

        /// <summary>
        /// Gets or sets an integer that delays the Chatbot response, in milliseconds.
        /// </summary>
        public short DisplayResponseWaitTime { get { return waitTime; } set { waitTime = value; } }

        private bool _consoleIsSilent;
        private string responseText;
        private short waitTime = Gala.Dolly.Chatbots.Properties.Settings.Default.ChatbotDisplayResponseWaitTime;
        private List<string> history = new List<string>();
        private int historyLine = -1;

        #region Private

        private void RadChatbot_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (!rb.Checked) return;

            // Set Engine ChatBot
            IChatbot chatbot = Program.RuntimeEngine.AI.LanguageModel.ChatbotManager[rb.Name];
            Program.RuntimeEngine.AI.LanguageModel.ChatbotManager.Current = chatbot;

            // Speak the Default Greeting
            SendResponse(chatbot.Greeting);

            // Disable the Default 
            this.radDefault.Enabled = false;

            // Focus Input
            txtInput.Focus();
        }

        private void TxtInput_KeyDown(object sender, KeyEventArgs e)
        {
            // No Data
            if (history.Count == 0) return;
            try
            {
                // Key Up
                if (e.KeyCode == Keys.Up)
                {
                    // Go to the last line entered on initial Key Up
                    if (historyLine == -1) historyLine = history.Count;

                    // Go to the Previous Line
                    historyLine--;

                    // Set the input
                    txtInput.Text = history[historyLine];
                    txtInput.Select(txtInput.Text.Length - 1, 1);
                }
                // Key Down
                else if (e.KeyCode == Keys.Down)
                {
                    // Go to the last line entered on initial Key Up
                    if (historyLine == history.Count)
                    {
                        txtInput.Clear();
                        return;
                    }

                    if(string.IsNullOrEmpty( txtInput.Text ))
                    {
                        historyLine = -1;
                    }

                    // Go to the Next Line
                    historyLine++;

                    // Set the input
                    txtInput.Text = history[historyLine];
                    txtInput.Select(txtInput.Text.Length - 1, 1);
                }
            }
            catch { throw; }
        }
        private void TxtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) { GetResponse(); }
        }
        private void BtnSend_Click(object sender, EventArgs e)
        {
            GetResponse();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            DisplayResponse();
            Timer.Stop();
        }
        private void Chatbot_Resize(object sender, EventArgs e)
        {
            txtInput.Width = this.Width - (btnSend.Width + btnSend.Margin.Left + btnSend.Margin.Right);
        }
        private void Chatbot_Enter(object sender, EventArgs e)
        {
            txtInput.Focus();
        }

        #endregion
    }
}
