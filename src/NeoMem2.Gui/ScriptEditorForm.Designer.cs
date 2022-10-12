namespace NeoMem2.Gui
{
    partial class ScriptEditorForm
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
            this.RtxtOutput = new System.Windows.Forms.RichTextBox();
            this.TvScripts = new System.Windows.Forms.TreeView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MiScript = new System.Windows.Forms.ToolStripMenuItem();
            this.MiScriptRun = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // RtxtOutput
            // 
            this.RtxtOutput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.RtxtOutput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RtxtOutput.Location = new System.Drawing.Point(124, 321);
            this.RtxtOutput.Name = "RtxtOutput";
            this.RtxtOutput.Size = new System.Drawing.Size(510, 96);
            this.RtxtOutput.TabIndex = 2;
            this.RtxtOutput.Text = "";
            this.RtxtOutput.WordWrap = false;
            // 
            // TvScripts
            // 
            this.TvScripts.Dock = System.Windows.Forms.DockStyle.Left;
            this.TvScripts.Location = new System.Drawing.Point(0, 24);
            this.TvScripts.Name = "TvScripts";
            this.TvScripts.Size = new System.Drawing.Size(121, 393);
            this.TvScripts.TabIndex = 3;
            this.TvScripts.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TvScriptsAfterSelect);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(121, 24);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 393);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter2.Location = new System.Drawing.Point(124, 318);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(510, 3);
            this.splitter2.TabIndex = 5;
            this.splitter2.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MiScript});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(634, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MiScript
            // 
            this.MiScript.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MiScriptRun});
            this.MiScript.Name = "MiScript";
            this.MiScript.Size = new System.Drawing.Size(49, 20);
            this.MiScript.Text = "Script";
            // 
            // MiScriptRun
            // 
            this.MiScriptRun.Name = "MiScriptRun";
            this.MiScriptRun.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.MiScriptRun.Size = new System.Drawing.Size(114, 22);
            this.MiScriptRun.Text = "Run";
            this.MiScriptRun.Click += new System.EventHandler(this.MiScriptRunClick);
            // 
            // ScriptEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 417);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.RtxtOutput);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.TvScripts);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ScriptEditorForm";
            this.Text = "Script Editor";
            this.Load += new System.EventHandler(this.ScriptEditorFormLoad);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox RtxtOutput;
        private System.Windows.Forms.TreeView TvScripts;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MiScript;
        public System.Windows.Forms.ToolStripMenuItem MiScriptRun;
    }
}