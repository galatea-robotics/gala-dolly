namespace Gala.Dolly
{
    partial class TemplateRecognitionForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.templateRecognition = new Gala.Dolly.UI.TemplateRecognition();
            this.chatbotControl = new Gala.Dolly.UI.ChatbotControl();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.templateRecognition);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(3);
            this.splitContainer1.Panel1MinSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.chatbotControl);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(6);
            this.splitContainer1.Panel2MinSize = 120;
            this.splitContainer1.Size = new System.Drawing.Size(464, 298);
            this.splitContainer1.SplitterDistance = 142;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 17;
            // 
            // templateRecognition
            // 
            this.templateRecognition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.templateRecognition.Location = new System.Drawing.Point(3, 3);
            this.templateRecognition.MinimumSize = new System.Drawing.Size(320, 240);
            this.templateRecognition.Name = "templateRecognition";
            this.templateRecognition.Size = new System.Drawing.Size(458, 240);
            this.templateRecognition.TabIndex = 0;
            this.templateRecognition.TemplateLoaded += new System.EventHandler(this.TemplateRecognition_TemplateLoaded);
            // 
            // chatbotControl
            // 
            this.chatbotControl.ChatbotButtonsVisible = true;
            this.chatbotControl.DisplayResponseWaitTime = ((short)(255));
            this.chatbotControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatbotControl.Location = new System.Drawing.Point(6, 6);
            this.chatbotControl.MaximumSize = new System.Drawing.Size(0, 180);
            this.chatbotControl.Name = "chatbotControl";
            this.chatbotControl.Size = new System.Drawing.Size(452, 143);
            this.chatbotControl.TabIndex = 0;
            // 
            // TemplateRecognitionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(464, 322);
            this.Controls.Add(this.splitContainer1);
            this.Name = "TemplateRecognitionForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private UI.ChatbotControl chatbotControl;
        private UI.TemplateRecognition templateRecognition;
    }
}
