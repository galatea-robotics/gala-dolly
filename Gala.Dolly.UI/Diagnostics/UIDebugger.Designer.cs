using System.Windows.Forms;

namespace Gala.Dolly.UI.Diagnostics
{
    partial class UIDebugger
    {
        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "toolStrip")]
        private void InitializeComponent()
        {
            this.debugMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.debugDiagnosticMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugLogMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugEventMenuItem = new ToolStripMenuItem();
            this.debugMsgMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugShowAlertsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.speechMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.speechSilentMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip.SuspendLayout();
            // 
            // debugMenu
            // 
            this.debugMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.debugDiagnosticMenuItem,
            this.debugLogMenuItem,
            this.debugEventMenuItem,
            this.debugMsgMenuItem,
            this.debugShowAlertsMenuItem});
            this.debugMenu.Name = "debugMenu";
            this.debugMenu.Size = new System.Drawing.Size(54, 25);
            this.debugMenu.Text = "&Debug";
            this.debugMenu.ToolTipText = "Debug";
            // 
            // debugDiagnosticMenuItem
            // 
            this.debugDiagnosticMenuItem.Name = "debugDiagnosticMenuItem";
            this.debugDiagnosticMenuItem.Size = new System.Drawing.Size(136, 22);
            this.debugDiagnosticMenuItem.Text = "&Diagnostic";
            // 
            // debugLogMenuItem
            // 
            this.debugLogMenuItem.Name = "debugLogMenuItem";
            this.debugLogMenuItem.Size = new System.Drawing.Size(136, 22);
            this.debugLogMenuItem.Text = "&Log";
            // 
            // debugEventMenuItem
            // 
            this.debugEventMenuItem.Name = "debugEventMenuItem";
            this.debugEventMenuItem.Size = new System.Drawing.Size(136, 22);
            this.debugEventMenuItem.Text = "&Events";
            // 
            // debugMsgMenuItem
            // 
            this.debugMsgMenuItem.Name = "debugMsgMenuItem";
            this.debugMsgMenuItem.Size = new System.Drawing.Size(136, 22);
            this.debugMsgMenuItem.Text = "&Messages";
            // 
            // debugShowAlertsMenuItem
            // 
            this.debugShowAlertsMenuItem.CheckOnClick = true;
            this.debugShowAlertsMenuItem.Name = "debugShowAlertsMenuItem";
            this.debugShowAlertsMenuItem.Size = new System.Drawing.Size(136, 22);
            this.debugShowAlertsMenuItem.Text = "&Show Alerts";
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.debugMenu,
            this.speechMenu});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(100, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip";
            // 
            // speechMenu
            // 
            this.speechMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.speechSilentMenuItem});
            this.speechMenu.Name = "speechMenu";
            this.speechMenu.Size = new System.Drawing.Size(57, 25);
            this.speechMenu.Text = "&Speech";
            // 
            // speechSilentMenuItem
            // 
            this.speechSilentMenuItem.CheckOnClick = true;
            this.speechSilentMenuItem.Name = "speechSilentMenuItem";
            this.speechSilentMenuItem.Size = new System.Drawing.Size(103, 22);
            this.speechSilentMenuItem.Text = "&Silent";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();

        }

        #endregion

        private ToolStripMenuItem debugMenu;
        private ToolStripMenuItem debugDiagnosticMenuItem;
        private ToolStripMenuItem debugLogMenuItem;
        private ToolStripMenuItem debugEventMenuItem;
        private ToolStripMenuItem debugMsgMenuItem;
        private ToolStripMenuItem debugShowAlertsMenuItem;
        private ToolStrip toolStrip;
        private ToolStripMenuItem speechMenu;
        private ToolStripMenuItem speechSilentMenuItem;
    }
}
