namespace Gala.Dolly.UI
{
    partial class BaseForm
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "menuStrip")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Windows.Forms.Control.set_Text(System.String)")]
        private void InitializeComponent()
        {
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this._fileMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._viewMenu = new System.Windows.Forms.ToolStripMenuItem();
			this._toolsMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._fileMenu,
            this._viewMenu,
            this._toolsMenu});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(464, 24);
			this.menuStrip.TabIndex = 2;
			this.menuStrip.Text = "menuStrip";
			// 
			// _fileMenu
			// 
			this._fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitMenuItem});
			this._fileMenu.Name = "_fileMenu";
			this._fileMenu.Size = new System.Drawing.Size(37, 20);
			// 
			// exitMenuItem
			// 
			this.exitMenuItem.Enabled = false;
			this.exitMenuItem.Name = "exitMenuItem";
			this.exitMenuItem.Size = new System.Drawing.Size(92, 22);
			// 
			// _viewMenu
			// 
			this._viewMenu.Name = "_viewMenu";
			this._viewMenu.Size = new System.Drawing.Size(44, 20);
			// 
			// _toolsMenu
			// 
			this._toolsMenu.Name = "_toolsMenu";
			this._toolsMenu.Size = new System.Drawing.Size(47, 20);
			// 
			// BaseForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = global::Gala.Dolly.UI.Properties.Settings.Default.WindowClientSize;
			this.Controls.Add(this.menuStrip);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Gala.Dolly.UI.Properties.Settings.Default, "ApplicationTitle", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::Gala.Dolly.UI.Properties.Settings.Default, "WindowClientSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Gala.Dolly.UI.Properties.Settings.Default, "WindowLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.Location = global::Gala.Dolly.UI.Properties.Settings.Default.WindowLocation;
			this.Name = "BaseForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = global::Gala.Dolly.UI.Properties.Settings.Default.ApplicationTitle;
			this.Load += new System.EventHandler(this.BaseForm_Load);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _fileMenu;
        private System.Windows.Forms.ToolStripMenuItem _viewMenu;
        private System.Windows.Forms.ToolStripMenuItem _toolsMenu;
    }
}

