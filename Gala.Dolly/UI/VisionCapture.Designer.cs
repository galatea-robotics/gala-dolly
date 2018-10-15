namespace Gala.Dolly.UI
{
    partial class VisionCapture
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
            this.btnPOV = new System.Windows.Forms.Button();
            this.btnCapture = new System.Windows.Forms.Button();
            this.btnRelease = new System.Windows.Forms.Button();
            this.timer = new System.Timers.Timer();
            this.panControl = new System.Windows.Forms.TrackBar();
            this.tiltControl = new System.Windows.Forms.TrackBar();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.cbReverseY = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDisplaySize = new System.Windows.Forms.Label();
            this.txtDisplaySize = new System.Windows.Forms.TextBox();
            this.lblMousePosition = new System.Windows.Forms.Label();
            this.txtMousePosition = new System.Windows.Forms.TextBox();
            this.lblLimits = new System.Windows.Forms.Label();
            this.lblPan = new System.Windows.Forms.Label();
            this.txtPan = new System.Windows.Forms.TextBox();
            this.txtPanMin = new System.Windows.Forms.TextBox();
            this.txtPanMax = new System.Windows.Forms.TextBox();
            this.lblTilt = new System.Windows.Forms.Label();
            this.txtTilt = new System.Windows.Forms.TextBox();
            this.txtTiltMin = new System.Windows.Forms.TextBox();
            this.txtTiltMax = new System.Windows.Forms.TextBox();
            this.ui = new System.Windows.Forms.Label();
            this.ctlSpeed = new System.Windows.Forms.NumericUpDown();
            this.btnResetCamera = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.cameraWindow = new Gala.Dolly.UI.CameraWindow();
            ((System.ComponentModel.ISupportInitialize)(this.timer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tiltControl)).BeginInit();
            this.buttonPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctlSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPOV
            // 
            this.btnPOV.Location = new System.Drawing.Point(3, 32);
            this.btnPOV.Name = "btnPOV";
            this.btnPOV.Size = new System.Drawing.Size(160, 23);
            this.btnPOV.TabIndex = 13;
            this.btnPOV.TabStop = false;
            this.btnPOV.Text = "[ HOME ] - Center";
            this.btnPOV.UseVisualStyleBackColor = true;
            this.btnPOV.Click += new System.EventHandler(this.btnPOV_Click);
            // 
            // btnCapture
            // 
            this.btnCapture.Location = new System.Drawing.Point(3, 3);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(160, 23);
            this.btnCapture.TabIndex = 12;
            this.btnCapture.TabStop = false;
            this.btnCapture.Text = "[ PAGE UP ] - Capture Mouse";
            this.btnCapture.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // btnRelease
            // 
            this.btnRelease.Location = new System.Drawing.Point(3, 3);
            this.btnRelease.Name = "btnRelease";
            this.btnRelease.Size = new System.Drawing.Size(160, 23);
            this.btnRelease.TabIndex = 14;
            this.btnRelease.TabStop = false;
            this.btnRelease.Text = "[ PAGE DN ] - Release Mouse";
            this.btnRelease.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRelease.UseVisualStyleBackColor = true;
            this.btnRelease.Visible = false;
            this.btnRelease.Click += new System.EventHandler(this.btnRelease_Click);
            // 
            // timer
            // 
            this.timer.Interval = 1000D;
            this.timer.SynchronizingObject = this;
            // 
            // panControl
            // 
            this.panControl.Enabled = false;
            this.panControl.Location = new System.Drawing.Point(0, 180);
            this.panControl.Margin = new System.Windows.Forms.Padding(0);
            this.panControl.Maximum = 1500;
            this.panControl.MaximumSize = new System.Drawing.Size(0, 30);
            this.panControl.MinimumSize = new System.Drawing.Size(240, 30);
            this.panControl.Name = "panControl";
            this.panControl.Size = new System.Drawing.Size(240, 30);
            this.panControl.TabIndex = 2;
            this.panControl.TickFrequency = 250;
            this.panControl.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.panControl.Value = 750;
            // 
            // tiltControl
            // 
            this.tiltControl.Enabled = false;
            this.tiltControl.Location = new System.Drawing.Point(240, 0);
            this.tiltControl.Margin = new System.Windows.Forms.Padding(0);
            this.tiltControl.Maximum = 1500;
            this.tiltControl.MaximumSize = new System.Drawing.Size(30, 0);
            this.tiltControl.MinimumSize = new System.Drawing.Size(30, 180);
            this.tiltControl.Name = "tiltControl";
            this.tiltControl.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tiltControl.Size = new System.Drawing.Size(30, 180);
            this.tiltControl.TabIndex = 3;
            this.tiltControl.TickFrequency = 250;
            this.tiltControl.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.tiltControl.Value = 750;
            // 
            // buttonPanel
            // 
            this.buttonPanel.Controls.Add(this.btnLoad);
            this.buttonPanel.Controls.Add(this.cbReverseY);
            this.buttonPanel.Controls.Add(this.panel1);
            this.buttonPanel.Controls.Add(this.btnPOV);
            this.buttonPanel.Controls.Add(this.btnCapture);
            this.buttonPanel.Controls.Add(this.btnRelease);
            this.buttonPanel.Controls.Add(this.btnResetCamera);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonPanel.Location = new System.Drawing.Point(300, 0);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(180, 329);
            this.buttonPanel.TabIndex = 0;
            // 
            // cbReverseY
            // 
            this.cbReverseY.AutoSize = true;
            this.cbReverseY.Location = new System.Drawing.Point(5, 91);
            this.cbReverseY.Name = "cbReverseY";
            this.cbReverseY.Size = new System.Drawing.Size(101, 17);
            this.cbReverseY.TabIndex = 23;
            this.cbReverseY.Text = "Reverse Y- Axis";
            this.cbReverseY.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblDisplaySize);
            this.panel1.Controls.Add(this.txtDisplaySize);
            this.panel1.Controls.Add(this.lblMousePosition);
            this.panel1.Controls.Add(this.txtMousePosition);
            this.panel1.Controls.Add(this.lblLimits);
            this.panel1.Controls.Add(this.lblPan);
            this.panel1.Controls.Add(this.txtPan);
            this.panel1.Controls.Add(this.txtPanMin);
            this.panel1.Controls.Add(this.txtPanMax);
            this.panel1.Controls.Add(this.lblTilt);
            this.panel1.Controls.Add(this.txtTilt);
            this.panel1.Controls.Add(this.txtTiltMin);
            this.panel1.Controls.Add(this.txtTiltMax);
            this.panel1.Controls.Add(this.ui);
            this.panel1.Controls.Add(this.ctlSpeed);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 183);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(180, 146);
            this.panel1.TabIndex = 21;
            // 
            // lblDisplaySize
            // 
            this.lblDisplaySize.AutoSize = true;
            this.lblDisplaySize.Location = new System.Drawing.Point(2, 6);
            this.lblDisplaySize.Name = "lblDisplaySize";
            this.lblDisplaySize.Size = new System.Drawing.Size(41, 13);
            this.lblDisplaySize.TabIndex = 1;
            this.lblDisplaySize.Text = "Display";
            // 
            // txtDisplaySize
            // 
            this.txtDisplaySize.BackColor = System.Drawing.SystemColors.Window;
            this.txtDisplaySize.Location = new System.Drawing.Point(5, 22);
            this.txtDisplaySize.Name = "txtDisplaySize";
            this.txtDisplaySize.ReadOnly = true;
            this.txtDisplaySize.Size = new System.Drawing.Size(76, 20);
            this.txtDisplaySize.TabIndex = 2;
            // 
            // lblMousePosition
            // 
            this.lblMousePosition.AutoSize = true;
            this.lblMousePosition.Location = new System.Drawing.Point(85, 6);
            this.lblMousePosition.Name = "lblMousePosition";
            this.lblMousePosition.Size = new System.Drawing.Size(39, 13);
            this.lblMousePosition.TabIndex = 3;
            this.lblMousePosition.Text = "Mouse";
            // 
            // txtMousePosition
            // 
            this.txtMousePosition.BackColor = System.Drawing.SystemColors.Window;
            this.txtMousePosition.Location = new System.Drawing.Point(87, 22);
            this.txtMousePosition.Name = "txtMousePosition";
            this.txtMousePosition.ReadOnly = true;
            this.txtMousePosition.Size = new System.Drawing.Size(76, 20);
            this.txtMousePosition.TabIndex = 4;
            // 
            // lblLimits
            // 
            this.lblLimits.Location = new System.Drawing.Point(88, 51);
            this.lblLimits.Name = "lblLimits";
            this.lblLimits.Size = new System.Drawing.Size(75, 13);
            this.lblLimits.TabIndex = 5;
            this.lblLimits.Text = "Limits";
            this.lblLimits.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblPan
            // 
            this.lblPan.AutoSize = true;
            this.lblPan.Location = new System.Drawing.Point(6, 70);
            this.lblPan.Name = "lblPan";
            this.lblPan.Size = new System.Drawing.Size(26, 13);
            this.lblPan.TabIndex = 6;
            this.lblPan.Text = "Pan";
            // 
            // txtPan
            // 
            this.txtPan.BackColor = System.Drawing.SystemColors.Window;
            this.txtPan.Location = new System.Drawing.Point(39, 67);
            this.txtPan.Name = "txtPan";
            this.txtPan.ReadOnly = true;
            this.txtPan.Size = new System.Drawing.Size(42, 20);
            this.txtPan.TabIndex = 7;
            // 
            // txtPanMin
            // 
            this.txtPanMin.Location = new System.Drawing.Point(87, 67);
            this.txtPanMin.Name = "txtPanMin";
            this.txtPanMin.Size = new System.Drawing.Size(35, 20);
            this.txtPanMin.TabIndex = 8;
            this.txtPanMin.TextChanged += new System.EventHandler(this.MinMax_TextChanged);
            this.txtPanMin.Validating += new System.ComponentModel.CancelEventHandler(this.MinMax_Validating);
            // 
            // txtPanMax
            // 
            this.txtPanMax.Location = new System.Drawing.Point(128, 67);
            this.txtPanMax.Name = "txtPanMax";
            this.txtPanMax.Size = new System.Drawing.Size(35, 20);
            this.txtPanMax.TabIndex = 9;
            this.txtPanMax.TextChanged += new System.EventHandler(this.MinMax_TextChanged);
            this.txtPanMax.Validating += new System.ComponentModel.CancelEventHandler(this.MinMax_Validating);
            // 
            // lblTilt
            // 
            this.lblTilt.AutoSize = true;
            this.lblTilt.Location = new System.Drawing.Point(6, 96);
            this.lblTilt.Name = "lblTilt";
            this.lblTilt.Size = new System.Drawing.Size(21, 13);
            this.lblTilt.TabIndex = 10;
            this.lblTilt.Text = "Tilt";
            // 
            // txtTilt
            // 
            this.txtTilt.BackColor = System.Drawing.SystemColors.Window;
            this.txtTilt.Location = new System.Drawing.Point(39, 93);
            this.txtTilt.Name = "txtTilt";
            this.txtTilt.ReadOnly = true;
            this.txtTilt.Size = new System.Drawing.Size(42, 20);
            this.txtTilt.TabIndex = 11;
            // 
            // txtTiltMin
            // 
            this.txtTiltMin.Location = new System.Drawing.Point(87, 93);
            this.txtTiltMin.Name = "txtTiltMin";
            this.txtTiltMin.Size = new System.Drawing.Size(35, 20);
            this.txtTiltMin.TabIndex = 12;
            this.txtTiltMin.TextChanged += new System.EventHandler(this.MinMax_TextChanged);
            this.txtTiltMin.Validating += new System.ComponentModel.CancelEventHandler(this.MinMax_Validating);
            // 
            // txtTiltMax
            // 
            this.txtTiltMax.Location = new System.Drawing.Point(128, 93);
            this.txtTiltMax.Name = "txtTiltMax";
            this.txtTiltMax.Size = new System.Drawing.Size(35, 20);
            this.txtTiltMax.TabIndex = 13;
            this.txtTiltMax.TextChanged += new System.EventHandler(this.MinMax_TextChanged);
            this.txtTiltMax.Validating += new System.ComponentModel.CancelEventHandler(this.MinMax_Validating);
            // 
            // ui
            // 
            this.ui.AutoSize = true;
            this.ui.Location = new System.Drawing.Point(42, 121);
            this.ui.Name = "ui";
            this.ui.Size = new System.Drawing.Size(38, 13);
            this.ui.TabIndex = 14;
            this.ui.Text = "Speed";
            // 
            // ctlSpeed
            // 
            this.ctlSpeed.Location = new System.Drawing.Point(87, 119);
            this.ctlSpeed.Name = "ctlSpeed";
            this.ctlSpeed.Size = new System.Drawing.Size(63, 20);
            this.ctlSpeed.TabIndex = 15;
            // 
            // btnResetCamera
            // 
            this.btnResetCamera.Location = new System.Drawing.Point(3, 61);
            this.btnResetCamera.Name = "btnResetCamera";
            this.btnResetCamera.Size = new System.Drawing.Size(160, 23);
            this.btnResetCamera.TabIndex = 22;
            this.btnResetCamera.TabStop = false;
            this.btnResetCamera.Text = "[ INSERT ] - Reset Camera";
            this.btnResetCamera.UseVisualStyleBackColor = true;
            this.btnResetCamera.Click += new System.EventHandler(this.btnResetCamera_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(240, 180);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(5, 136);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(158, 23);
            this.btnLoad.TabIndex = 22;
            this.btnLoad.Text = "Load Image File";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // cameraWindow
            // 
            this.cameraWindow.Camera = null;
            this.cameraWindow.CenterOverlay = true;
            this.cameraWindow.Location = new System.Drawing.Point(0, 0);
            this.cameraWindow.Margin = new System.Windows.Forms.Padding(0);
            this.cameraWindow.MinimumSize = new System.Drawing.Size(240, 180);
            this.cameraWindow.Name = "cameraWindow";
            this.cameraWindow.Size = new System.Drawing.Size(240, 180);
            this.cameraWindow.TabIndex = 1;
            // 
            // VisionCapture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cameraWindow);
            this.Controls.Add(this.panControl);
            this.Controls.Add(this.tiltControl);
            this.Controls.Add(this.buttonPanel);
            this.MinimumSize = new System.Drawing.Size(480, 210);
            this.Name = "VisionCapture";
            this.Size = new System.Drawing.Size(480, 329);
            this.Load += new System.EventHandler(this.VisionCapture_Load);
            this.Resize += new System.EventHandler(this.VisionCapture_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.timer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tiltControl)).EndInit();
            this.buttonPanel.ResumeLayout(false);
            this.buttonPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctlSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.Button btnPOV;
        internal System.Windows.Forms.Button btnCapture;
        internal System.Windows.Forms.Button btnRelease;
        private System.Timers.Timer timer;
        internal Gala.Dolly.UI.CameraWindow cameraWindow;
        private System.Windows.Forms.TrackBar tiltControl;
        private System.Windows.Forms.TrackBar panControl;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.TextBox txtDisplaySize;
        private System.Windows.Forms.Label lblDisplaySize;
        private System.Windows.Forms.Label lblPan;
        private System.Windows.Forms.Label lblMousePosition;
        private System.Windows.Forms.TextBox txtMousePosition;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTilt;
        private System.Windows.Forms.TextBox txtTilt;
        private System.Windows.Forms.TextBox txtPan;
        private System.Windows.Forms.TextBox txtPanMin;
        private System.Windows.Forms.TextBox txtPanMax;
        private System.Windows.Forms.Label lblLimits;
        private System.Windows.Forms.NumericUpDown ctlSpeed;
        private System.Windows.Forms.Label ui;
        private System.Windows.Forms.TextBox txtTiltMin;
        private System.Windows.Forms.TextBox txtTiltMax;
        private System.Windows.Forms.PictureBox pictureBox1;
        internal System.Windows.Forms.Button btnResetCamera;
        private System.Windows.Forms.CheckBox cbReverseY;
        internal System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}
