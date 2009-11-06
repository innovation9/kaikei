namespace kaikei
{
    partial class Form1
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
            this.mnsMain = new System.Windows.Forms.MenuStrip();
            this.stsMain = new System.Windows.Forms.StatusStrip();
            this.sptMain = new System.Windows.Forms.SplitContainer();
            this.tabcMain = new System.Windows.Forms.TabControl();
            this.tabpBase = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tolTabControl = new System.Windows.Forms.ToolStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnsMain.SuspendLayout();
            this.sptMain.Panel2.SuspendLayout();
            this.sptMain.SuspendLayout();
            this.tabcMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnsMain
            // 
            this.mnsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem});
            this.mnsMain.Location = new System.Drawing.Point(0, 0);
            this.mnsMain.Name = "mnsMain";
            this.mnsMain.Size = new System.Drawing.Size(534, 24);
            this.mnsMain.TabIndex = 0;
            this.mnsMain.Text = "mnsTop";
            // 
            // stsMain
            // 
            this.stsMain.Location = new System.Drawing.Point(0, 392);
            this.stsMain.Name = "stsMain";
            this.stsMain.Size = new System.Drawing.Size(534, 22);
            this.stsMain.TabIndex = 1;
            this.stsMain.Text = "statusStrip1";
            // 
            // sptMain
            // 
            this.sptMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sptMain.Location = new System.Drawing.Point(0, 24);
            this.sptMain.Name = "sptMain";
            // 
            // sptMain.Panel2
            // 
            this.sptMain.Panel2.Controls.Add(this.tolTabControl);
            this.sptMain.Panel2.Controls.Add(this.tabcMain);
            this.sptMain.Size = new System.Drawing.Size(534, 368);
            this.sptMain.SplitterDistance = 150;
            this.sptMain.TabIndex = 2;
            // 
            // tabcMain
            // 
            this.tabcMain.Controls.Add(this.tabpBase);
            this.tabcMain.Controls.Add(this.tabPage2);
            this.tabcMain.Location = new System.Drawing.Point(3, 28);
            this.tabcMain.Multiline = true;
            this.tabcMain.Name = "tabcMain";
            this.tabcMain.SelectedIndex = 0;
            this.tabcMain.Size = new System.Drawing.Size(374, 337);
            this.tabcMain.TabIndex = 0;
            // 
            // tabpBase
            // 
            this.tabpBase.Location = new System.Drawing.Point(4, 22);
            this.tabpBase.Name = "tabpBase";
            this.tabpBase.Padding = new System.Windows.Forms.Padding(3);
            this.tabpBase.Size = new System.Drawing.Size(366, 311);
            this.tabpBase.TabIndex = 0;
            this.tabpBase.Text = "Cuentas";
            this.tabpBase.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(366, 336);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tolTabControl
            // 
            this.tolTabControl.Location = new System.Drawing.Point(0, 0);
            this.tolTabControl.Name = "tolTabControl";
            this.tolTabControl.Size = new System.Drawing.Size(380, 25);
            this.tolTabControl.TabIndex = 1;
            this.tolTabControl.Text = "toolStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "&Archivo";
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.salirToolStripMenuItem.Text = "&Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 414);
            this.Controls.Add(this.sptMain);
            this.Controls.Add(this.stsMain);
            this.Controls.Add(this.mnsMain);
            this.MainMenuStrip = this.mnsMain;
            this.Name = "Form1";
            this.Text = "Kaikei";
            this.mnsMain.ResumeLayout(false);
            this.mnsMain.PerformLayout();
            this.sptMain.Panel2.ResumeLayout(false);
            this.sptMain.Panel2.PerformLayout();
            this.sptMain.ResumeLayout(false);
            this.tabcMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnsMain;
        private System.Windows.Forms.StatusStrip stsMain;
        private System.Windows.Forms.SplitContainer sptMain;
        private System.Windows.Forms.TabControl tabcMain;
        private System.Windows.Forms.TabPage tabpBase;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStrip tolTabControl;
    }
}

