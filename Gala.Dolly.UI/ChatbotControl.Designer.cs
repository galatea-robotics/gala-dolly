namespace Gala.Dolly.UI
{
    partial class ChatbotControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblChatbotSelector = new System.Windows.Forms.Label();
            this.radDefault = new System.Windows.Forms.RadioButton();
            this.txtDisplay = new System.Windows.Forms.TextBox();
            this.pnlChatbotSelector = new System.Windows.Forms.Panel();
            this.btnSend = new System.Windows.Forms.Button();
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.txtInput = new System.Windows.Forms.TextBox();
            this.pnlInput = new System.Windows.Forms.Panel();
            this.pnlChatbotSelector.SuspendLayout();
            this.pnlInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblChatbotSelector
            // 
            this.lblChatbotSelector.AutoSize = true;
            this.lblChatbotSelector.Location = new System.Drawing.Point(3, 5);
            this.lblChatbotSelector.Margin = new System.Windows.Forms.Padding(3, 0, 9, 0);
            this.lblChatbotSelector.Name = "lblChatbotSelector";
            this.lblChatbotSelector.Size = new System.Drawing.Size(0, 13);
            this.lblChatbotSelector.TabIndex = 4;
            // 
            // radDefault
            // 
            this.radDefault.AutoSize = true;
            this.radDefault.Checked = true;
            this.radDefault.Location = new System.Drawing.Point(76, 3);
            this.radDefault.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.radDefault.Name = "radDefault";
            this.radDefault.Size = new System.Drawing.Size(14, 13);
            this.radDefault.TabIndex = 7;
            this.radDefault.TabStop = true;
            this.radDefault.Tag = "Default";
            this.radDefault.UseVisualStyleBackColor = true;
            // 
            // txtDisplay
            // 
            this.txtDisplay.BackColor = System.Drawing.SystemColors.Window;
            this.txtDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDisplay.Location = new System.Drawing.Point(0, 19);
            this.txtDisplay.Multiline = true;
            this.txtDisplay.Name = "txtDisplay";
            this.txtDisplay.ReadOnly = true;
            this.txtDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDisplay.Size = new System.Drawing.Size(320, 188);
            this.txtDisplay.TabIndex = 8;
            this.txtDisplay.TabStop = false;
            // 
            // pnlChatbotSelector
            // 
            this.pnlChatbotSelector.AutoSize = true;
            this.pnlChatbotSelector.Controls.Add(this.lblChatbotSelector);
            this.pnlChatbotSelector.Controls.Add(this.radDefault);
            this.pnlChatbotSelector.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlChatbotSelector.Location = new System.Drawing.Point(0, 0);
            this.pnlChatbotSelector.Name = "pnlChatbotSelector";
            this.pnlChatbotSelector.Size = new System.Drawing.Size(320, 19);
            this.pnlChatbotSelector.TabIndex = 3;
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(264, 10);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(56, 23);
            this.btnSend.TabIndex = 2;
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.BtnSend_Click);
            // 
            // Timer
            // 
            this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(0, 12);
            this.txtInput.Margin = new System.Windows.Forms.Padding(0);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(261, 20);
            this.txtInput.TabIndex = 1;
            this.txtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtInput_KeyDown);
            this.txtInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtInput_KeyPress);
            // 
            // pnlInput
            // 
            this.pnlInput.Controls.Add(this.txtInput);
            this.pnlInput.Controls.Add(this.btnSend);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlInput.Location = new System.Drawing.Point(0, 207);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Size = new System.Drawing.Size(320, 33);
            this.pnlInput.TabIndex = 0;
            this.pnlInput.TabStop = true;
            // 
            // ChatbotControl
            // 
            this.Controls.Add(this.txtDisplay);
            this.Controls.Add(this.pnlChatbotSelector);
            this.Controls.Add(this.pnlInput);
            this.Name = "ChatbotControl";
            this.Size = new System.Drawing.Size(320, 240);
            this.Enter += new System.EventHandler(this.Chatbot_Enter);
            this.Resize += new System.EventHandler(this.Chatbot_Resize);
            this.pnlChatbotSelector.ResumeLayout(false);
            this.pnlChatbotSelector.PerformLayout();
            this.pnlInput.ResumeLayout(false);
            this.pnlInput.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lblChatbotSelector;
        internal System.Windows.Forms.RadioButton radDefault;
        internal System.Windows.Forms.TextBox txtDisplay;
        internal System.Windows.Forms.Panel pnlChatbotSelector;
        internal System.Windows.Forms.Button btnSend;
        internal System.Windows.Forms.Timer Timer;
        internal System.Windows.Forms.TextBox txtInput;
        internal System.Windows.Forms.Panel pnlInput;
    }
}
