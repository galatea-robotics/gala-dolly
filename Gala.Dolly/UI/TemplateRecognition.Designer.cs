namespace Gala.Dolly.UI
{
    partial class TemplateRecognition
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

            // CA2213 : Microsoft.Usage : 'TemplateRecognition' contains field 'TemplateRecognition.displayImage' that is of IDisposable type: 'Bitmap'. Change the Dispose method on 'TemplateRecognition' to call Dispose or Close on this field.
            if (sourceImage != null) sourceImage.Dispose();
            // CA2213 : Microsoft.Usage : 'TemplateRecognition' contains field 'TemplateRecognition.blobImage' that is of IDisposable type: 'Bitmap'. Change the Dispose method on 'TemplateRecognition' to call Dispose or Close on this field.
            if (displayImage != null) displayImage.Dispose();

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "Blobify")]
        private void InitializeComponent()
        {
            this.display = new System.Windows.Forms.Panel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnLoad = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_R = new System.Windows.Forms.Label();
            this.txt_R = new System.Windows.Forms.TextBox();
            this.lbl_G = new System.Windows.Forms.Label();
            this.txt_G = new System.Windows.Forms.TextBox();
            this.lbl_B = new System.Windows.Forms.Label();
            this.txt_B = new System.Windows.Forms.TextBox();
            this.lbl_H = new System.Windows.Forms.Label();
            this.txt_H = new System.Windows.Forms.TextBox();
            this.lbl_S = new System.Windows.Forms.Label();
            this.txt_S = new System.Windows.Forms.TextBox();
            this.lbl_L = new System.Windows.Forms.Label();
            this.txt_L = new System.Windows.Forms.TextBox();
            this.btnRandom = new System.Windows.Forms.Button();
            this.btnBlobify = new System.Windows.Forms.Button();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // display
            // 
            this.display.BackColor = System.Drawing.Color.White;
            this.display.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.display.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.display.Location = new System.Drawing.Point(3, 3);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(151, 108);
            this.display.TabIndex = 2;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(22, 111);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(58, 23);
            this.btnLoad.TabIndex = 21;
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.BtnLoad_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.MinimumSize = new System.Drawing.Size(314, 114);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.display);
            this.splitContainer2.Panel1MinSize = 120;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panel1);
            this.splitContainer2.Panel2MinSize = 150;
            this.splitContainer2.Size = new System.Drawing.Size(320, 180);
            this.splitContainer2.SplitterDistance = 169;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_R);
            this.panel1.Controls.Add(this.txt_R);
            this.panel1.Controls.Add(this.lbl_G);
            this.panel1.Controls.Add(this.txt_G);
            this.panel1.Controls.Add(this.lbl_B);
            this.panel1.Controls.Add(this.txt_B);
            this.panel1.Controls.Add(this.lbl_H);
            this.panel1.Controls.Add(this.txt_H);
            this.panel1.Controls.Add(this.lbl_S);
            this.panel1.Controls.Add(this.txt_S);
            this.panel1.Controls.Add(this.lbl_L);
            this.panel1.Controls.Add(this.txt_L);
            this.panel1.Controls.Add(this.btnRandom);
            this.panel1.Controls.Add(this.btnLoad);
            this.panel1.Controls.Add(this.btnBlobify);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(149, 180);
            this.panel1.TabIndex = 1;
            // 
            // lbl_R
            // 
            this.lbl_R.AutoSize = true;
            this.lbl_R.Location = new System.Drawing.Point(3, 6);
            this.lbl_R.Name = "lbl_R";
            this.lbl_R.Size = new System.Drawing.Size(15, 13);
            this.lbl_R.TabIndex = 3;
            // 
            // txt_R
            // 
            this.txt_R.Location = new System.Drawing.Point(24, 3);
            this.txt_R.MaxLength = 3;
            this.txt_R.Name = "txt_R";
            this.txt_R.Size = new System.Drawing.Size(42, 20);
            this.txt_R.TabIndex = 4;
            this.txt_R.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_R.Click += new System.EventHandler(this.Txt_Click);
            this.txt_R.Validating += new System.ComponentModel.CancelEventHandler(this.Txt_R_Validating);
            this.txt_R.Validated += new System.EventHandler(this.Txt_Validated);
            // 
            // lbl_G
            // 
            this.lbl_G.AutoSize = true;
            this.lbl_G.Location = new System.Drawing.Point(3, 36);
            this.lbl_G.Name = "lbl_G";
            this.lbl_G.Size = new System.Drawing.Size(15, 13);
            this.lbl_G.TabIndex = 5;
            // 
            // txt_G
            // 
            this.txt_G.Location = new System.Drawing.Point(24, 29);
            this.txt_G.MaxLength = 3;
            this.txt_G.Name = "txt_G";
            this.txt_G.Size = new System.Drawing.Size(42, 20);
            this.txt_G.TabIndex = 6;
            this.txt_G.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_G.Click += new System.EventHandler(this.Txt_Click);
            this.txt_G.Validating += new System.ComponentModel.CancelEventHandler(this.Txt_G_Validating);
            this.txt_G.Validated += new System.EventHandler(this.Txt_Validated);
            // 
            // lbl_B
            // 
            this.lbl_B.AutoSize = true;
            this.lbl_B.Location = new System.Drawing.Point(4, 58);
            this.lbl_B.Name = "lbl_B";
            this.lbl_B.Size = new System.Drawing.Size(14, 13);
            this.lbl_B.TabIndex = 7;
            // 
            // txt_B
            // 
            this.txt_B.Location = new System.Drawing.Point(24, 55);
            this.txt_B.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.txt_B.MaxLength = 3;
            this.txt_B.Name = "txt_B";
            this.txt_B.Size = new System.Drawing.Size(42, 20);
            this.txt_B.TabIndex = 8;
            this.txt_B.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_B.Click += new System.EventHandler(this.Txt_Click);
            this.txt_B.Validating += new System.ComponentModel.CancelEventHandler(this.Txt_B_Validating);
            this.txt_B.Validated += new System.EventHandler(this.Txt_Validated);
            // 
            // lbl_H
            // 
            this.lbl_H.AutoSize = true;
            this.lbl_H.Location = new System.Drawing.Point(83, 6);
            this.lbl_H.Name = "lbl_H";
            this.lbl_H.Size = new System.Drawing.Size(15, 13);
            this.lbl_H.TabIndex = 9;
            // 
            // txt_H
            // 
            this.txt_H.Enabled = false;
            this.txt_H.Location = new System.Drawing.Point(102, 3);
            this.txt_H.MaxLength = 3;
            this.txt_H.Name = "txt_H";
            this.txt_H.Size = new System.Drawing.Size(42, 20);
            this.txt_H.TabIndex = 10;
            this.txt_H.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbl_S
            // 
            this.lbl_S.AutoSize = true;
            this.lbl_S.Location = new System.Drawing.Point(83, 32);
            this.lbl_S.Name = "lbl_S";
            this.lbl_S.Size = new System.Drawing.Size(14, 13);
            this.lbl_S.TabIndex = 11;
            // 
            // txt_S
            // 
            this.txt_S.Enabled = false;
            this.txt_S.Location = new System.Drawing.Point(102, 29);
            this.txt_S.MaxLength = 3;
            this.txt_S.Name = "txt_S";
            this.txt_S.Size = new System.Drawing.Size(42, 20);
            this.txt_S.TabIndex = 12;
            this.txt_S.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbl_L
            // 
            this.lbl_L.AutoSize = true;
            this.lbl_L.Location = new System.Drawing.Point(83, 58);
            this.lbl_L.Name = "lbl_L";
            this.lbl_L.Size = new System.Drawing.Size(13, 13);
            this.lbl_L.TabIndex = 13;
            // 
            // txt_L
            // 
            this.txt_L.Enabled = false;
            this.txt_L.Location = new System.Drawing.Point(102, 55);
            this.txt_L.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.txt_L.MaxLength = 3;
            this.txt_L.Name = "txt_L";
            this.txt_L.Size = new System.Drawing.Size(42, 20);
            this.txt_L.TabIndex = 14;
            this.txt_L.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnRandom
            // 
            this.btnRandom.Location = new System.Drawing.Point(86, 82);
            this.btnRandom.Name = "btnRandom";
            this.btnRandom.Size = new System.Drawing.Size(59, 23);
            this.btnRandom.TabIndex = 20;
            this.btnRandom.UseVisualStyleBackColor = true;
            // 
            // btnBlobify
            // 
            this.btnBlobify.Enabled = false;
            this.btnBlobify.Location = new System.Drawing.Point(86, 111);
            this.btnBlobify.Name = "btnBlobify";
            this.btnBlobify.Size = new System.Drawing.Size(58, 23);
            this.btnBlobify.TabIndex = 22;
            this.btnBlobify.UseVisualStyleBackColor = true;
            this.btnBlobify.Visible = false;
            this.btnBlobify.Click += new System.EventHandler(this.BtnBlobify_Click);
            // 
            // TemplateRecognition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer2);
            this.MinimumSize = new System.Drawing.Size(320, 180);
            this.Name = "TemplateRecognition";
            this.Size = new System.Drawing.Size(320, 180);
            this.Load += new System.EventHandler(this.TemplateRecognition_Load);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel display;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        internal System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_R;
        private System.Windows.Forms.TextBox txt_R;
        private System.Windows.Forms.Label lbl_G;
        private System.Windows.Forms.TextBox txt_G;
        private System.Windows.Forms.Label lbl_B;
        private System.Windows.Forms.TextBox txt_B;
        private System.Windows.Forms.Label lbl_H;
        private System.Windows.Forms.TextBox txt_H;
        private System.Windows.Forms.Label lbl_S;
        private System.Windows.Forms.TextBox txt_S;
        private System.Windows.Forms.Label lbl_L;
        private System.Windows.Forms.TextBox txt_L;
        internal System.Windows.Forms.Button btnRandom;
        internal System.Windows.Forms.Button btnBlobify;
    }
}
