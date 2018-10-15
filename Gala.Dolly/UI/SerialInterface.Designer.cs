namespace Gala.Dolly.UI
{
    partial class SerialInterface
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
            this.btnOpenPort = new System.Windows.Forms.Button();
            this.btnClosePort = new System.Windows.Forms.Button();
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.comboPorts = new System.Windows.Forms.ComboBox();
            this.comboBaudRate = new System.Windows.Forms.ComboBox();
            this.lblInterval = new System.Windows.Forms.Label();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.lblCommand = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOpenPort
            // 
            this.btnOpenPort.Location = new System.Drawing.Point(172, 14);
            this.btnOpenPort.Name = "btnOpenPort";
            this.btnOpenPort.Size = new System.Drawing.Size(64, 23);
            this.btnOpenPort.TabIndex = 15;
            this.btnOpenPort.Text = "Open Port";
            this.btnOpenPort.UseVisualStyleBackColor = true;
            this.btnOpenPort.Click += new System.EventHandler(this.OpenPort);
            // 
            // btnClosePort
            // 
            this.btnClosePort.Location = new System.Drawing.Point(242, 14);
            this.btnClosePort.Name = "btnClosePort";
            this.btnClosePort.Size = new System.Drawing.Size(64, 23);
            this.btnClosePort.TabIndex = 16;
            this.btnClosePort.Text = "Close Port";
            this.btnClosePort.UseVisualStyleBackColor = true;
            this.btnClosePort.Click += new System.EventHandler(this.ClosePort);
            // 
            // txtCommand
            // 
            this.txtCommand.Location = new System.Drawing.Point(142, 45);
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(94, 20);
            this.txtCommand.TabIndex = 11;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(260, 43);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(46, 23);
            this.btnSend.TabIndex = 12;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.SendCommand);
            // 
            // comboPorts
            // 
            this.comboPorts.FormattingEnabled = true;
            this.comboPorts.Location = new System.Drawing.Point(17, 14);
            this.comboPorts.Name = "comboPorts";
            this.comboPorts.Size = new System.Drawing.Size(68, 21);
            this.comboPorts.TabIndex = 13;
            this.comboPorts.SelectedIndexChanged += new System.EventHandler(this.ClosePort);
            // 
            // comboBaudRate
            // 
            this.comboBaudRate.FormattingEnabled = true;
            this.comboBaudRate.Location = new System.Drawing.Point(91, 14);
            this.comboBaudRate.Name = "comboBaudRate";
            this.comboBaudRate.Size = new System.Drawing.Size(68, 21);
            this.comboBaudRate.TabIndex = 14;
            this.comboBaudRate.SelectedIndexChanged += new System.EventHandler(this.ClosePort);
            // 
            // lblInterval
            // 
            this.lblInterval.AutoSize = true;
            this.lblInterval.Location = new System.Drawing.Point(14, 50);
            this.lblInterval.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lblInterval.Name = "lblInterval";
            this.lblInterval.Size = new System.Drawing.Size(42, 13);
            this.lblInterval.TabIndex = 17;
            this.lblInterval.Text = "Interval";
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(59, 45);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(37, 20);
            this.txtInterval.TabIndex = 18;
            this.txtInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtInterval.TextChanged += new System.EventHandler(this.txtInterval_TextChanged);
            this.txtInterval.Validating += new System.ComponentModel.CancelEventHandler(this.txtInterval_Validating);
            // 
            // lblCommand
            // 
            this.lblCommand.AutoSize = true;
            this.lblCommand.Location = new System.Drawing.Point(109, 50);
            this.lblCommand.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lblCommand.Name = "lblCommand";
            this.lblCommand.Size = new System.Drawing.Size(30, 13);
            this.lblCommand.TabIndex = 10;
            this.lblCommand.Text = "Data";
            // 
            // SerialInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.btnOpenPort);
            this.Controls.Add(this.btnClosePort);
            this.Controls.Add(this.txtCommand);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.comboPorts);
            this.Controls.Add(this.comboBaudRate);
            this.Controls.Add(this.lblInterval);
            this.Controls.Add(this.txtInterval);
            this.Controls.Add(this.lblCommand);
            this.MinimumSize = new System.Drawing.Size(320, 0);
            this.Name = "SerialInterface";
            this.Padding = new System.Windows.Forms.Padding(14, 7, 0, 7);
            this.Size = new System.Drawing.Size(320, 76);
            this.Load += new System.EventHandler(this.SerialInterface_Load);
            this.Disposed += new System.EventHandler(this.SerialInterface_Disposed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btnOpenPort;
        internal System.Windows.Forms.Button btnClosePort;
        internal System.Windows.Forms.TextBox txtCommand;
        internal System.Windows.Forms.Button btnSend;
        internal System.Windows.Forms.ComboBox comboPorts;
        internal System.Windows.Forms.ComboBox comboBaudRate;
        internal System.Windows.Forms.Label lblInterval;
        internal System.Windows.Forms.TextBox txtInterval;
        internal System.Windows.Forms.Label lblCommand;
    }
}
