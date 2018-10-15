namespace Gala.Dolly.UI
{
    partial class SpeechRecognition
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
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.SpeechRecogMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ConversationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PanAndTiltToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MotorControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MediaPlayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TurnMicOnAtStartupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMicOn = new System.Windows.Forms.Button();
            this.btnMicOff = new System.Windows.Forms.Button();
            this.MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SpeechRecogMenu});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(307, 24);
            this.MenuStrip.TabIndex = 30;
            this.MenuStrip.Text = "Speech &Recognition";
            this.MenuStrip.Visible = false;
            // 
            // SpeechRecogMenu
            // 
            this.SpeechRecogMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConversationToolStripMenuItem,
            this.PanAndTiltToolStripMenuItem,
            this.MotorControlToolStripMenuItem,
            this.MediaPlayerToolStripMenuItem,
            this.ToolStripSeparator1,
            this.TurnMicOnAtStartupToolStripMenuItem});
            this.SpeechRecogMenu.Name = "SpeechRecogMenu";
            this.SpeechRecogMenu.Size = new System.Drawing.Size(124, 20);
            this.SpeechRecogMenu.Text = "Speech &Recognition";
            // 
            // ConversationToolStripMenuItem
            // 
            this.ConversationToolStripMenuItem.CheckOnClick = true;
            this.ConversationToolStripMenuItem.Name = "ConversationToolStripMenuItem";
            this.ConversationToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.ConversationToolStripMenuItem.Text = "&Conversation";
            // 
            // PanAndTiltToolStripMenuItem
            // 
            this.PanAndTiltToolStripMenuItem.CheckOnClick = true;
            this.PanAndTiltToolStripMenuItem.Name = "PanAndTiltToolStripMenuItem";
            this.PanAndTiltToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.PanAndTiltToolStripMenuItem.Text = "&Pan and Tilt";
            // 
            // MotorControlToolStripMenuItem
            // 
            this.MotorControlToolStripMenuItem.Checked = true;
            this.MotorControlToolStripMenuItem.CheckOnClick = true;
            this.MotorControlToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MotorControlToolStripMenuItem.Name = "MotorControlToolStripMenuItem";
            this.MotorControlToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.MotorControlToolStripMenuItem.Text = "&Motor Control";
            // 
            // MediaPlayerToolStripMenuItem
            // 
            this.MediaPlayerToolStripMenuItem.CheckOnClick = true;
            this.MediaPlayerToolStripMenuItem.Name = "MediaPlayerToolStripMenuItem";
            this.MediaPlayerToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.MediaPlayerToolStripMenuItem.Text = "M&edia Player";
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(192, 6);
            // 
            // TurnMicOnAtStartupToolStripMenuItem
            // 
            this.TurnMicOnAtStartupToolStripMenuItem.Checked = true;
            this.TurnMicOnAtStartupToolStripMenuItem.CheckOnClick = true;
            this.TurnMicOnAtStartupToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TurnMicOnAtStartupToolStripMenuItem.Name = "TurnMicOnAtStartupToolStripMenuItem";
            this.TurnMicOnAtStartupToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.TurnMicOnAtStartupToolStripMenuItem.Text = "&Turn Mic On at Startup";
            // 
            // btnMicOn
            // 
            this.btnMicOn.Image = global::Gala.Dolly.Properties.Resources.mic_on;
            this.btnMicOn.Location = new System.Drawing.Point(0, 3);
            this.btnMicOn.Name = "btnMicOn";
            this.btnMicOn.Size = new System.Drawing.Size(28, 29);
            this.btnMicOn.TabIndex = 31;
            this.btnMicOn.TabStop = false;
            this.btnMicOn.UseVisualStyleBackColor = true;
            this.btnMicOn.Click += new System.EventHandler(this.btnMicOn_Click);
            // 
            // btnMicOff
            // 
            this.btnMicOff.Image = global::Gala.Dolly.Properties.Resources.mic_off;
            this.btnMicOff.Location = new System.Drawing.Point(0, 3);
            this.btnMicOff.Name = "btnMicOff";
            this.btnMicOff.Size = new System.Drawing.Size(28, 29);
            this.btnMicOff.TabIndex = 33;
            this.btnMicOff.TabStop = false;
            this.btnMicOff.UseVisualStyleBackColor = true;
            this.btnMicOff.Click += new System.EventHandler(this.btnMicOff_Click);
            // 
            // SpeechRecognition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.btnMicOn);
            this.Controls.Add(this.btnMicOff);
            this.Controls.Add(this.MenuStrip);
            this.Name = "SpeechRecognition";
            this.Size = new System.Drawing.Size(31, 35);
1            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.MenuStrip MenuStrip;
        internal System.Windows.Forms.ToolStripMenuItem SpeechRecogMenu;
        internal System.Windows.Forms.ToolStripMenuItem ConversationToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem PanAndTiltToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem MotorControlToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem MediaPlayerToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        internal System.Windows.Forms.ToolStripMenuItem TurnMicOnAtStartupToolStripMenuItem;
        internal System.Windows.Forms.Button btnMicOn;
        internal System.Windows.Forms.Button btnMicOff;
    }
}
