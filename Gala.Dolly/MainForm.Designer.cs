namespace Gala.Dolly
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.visionCapture = new Gala.Dolly.UI.VisionCapture();
            this.chatbotControl = new Gala.Dolly.UI.ChatbotControl();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.visionCapture);
            this.splitContainer.Panel1.Padding = new System.Windows.Forms.Padding(3);
            this.splitContainer.Panel1MinSize = 0;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.chatbotControl);
            this.splitContainer.Panel2.Padding = new System.Windows.Forms.Padding(6);
            this.splitContainer.Panel2MinSize = 0;
            this.splitContainer.Size = new System.Drawing.Size(624, 417);
            this.splitContainer.SplitterDistance = 254;
            this.splitContainer.SplitterWidth = 1;
            this.splitContainer.TabIndex = 17;
            // 
            // visionCapture
            // 
            this.visionCapture.AutoSize = true;
            this.visionCapture.Offset = new System.Drawing.Point(24, 64);
            this.visionCapture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.visionCapture.Location = new System.Drawing.Point(3, 3);
            this.visionCapture.MinimumSize = new System.Drawing.Size(480, 210);
            this.visionCapture.Name = "visionCapture";
            this.visionCapture.Size = new System.Drawing.Size(618, 248);
            this.visionCapture.TabIndex = 0;
            // 
            // chatBotControl
            // 
            this.chatbotControl.ChatbotButtonsVisible = true;
            this.chatbotControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatbotControl.Location = new System.Drawing.Point(6, 6);
            this.chatbotControl.MaximumSize = new System.Drawing.Size(0, 180);
            this.chatbotControl.Name = "chatBotControl";
            this.chatbotControl.Size = new System.Drawing.Size(612, 150);
            this.chatbotControl.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.splitContainer);
            this.MinimumSize = new System.Drawing.Size(560, 420);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.Controls.SetChildIndex(this.splitContainer, 0);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private UI.ChatbotControl chatbotControl;
        private UI.VisionCapture visionCapture;
    }
}
