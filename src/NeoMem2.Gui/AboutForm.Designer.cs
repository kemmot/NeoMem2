namespace NeoMem2.Gui
{
    partial class AboutForm
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
            this.BtnClose = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TpVersion = new System.Windows.Forms.TabPage();
            this.TpReleaseNotes = new System.Windows.Forms.TabPage();
            this.RtxtReleaseNotes = new System.Windows.Forms.RichTextBox();
            this.TpFeatures = new System.Windows.Forms.TabPage();
            this.RtxtFeatures = new System.Windows.Forms.RichTextBox();
            this.TpRoadmap = new System.Windows.Forms.TabPage();
            this.RtxtRoadmap = new System.Windows.Forms.RichTextBox();
            this.TpMemory = new System.Windows.Forms.TabPage();
            this.LvMemoryDetails = new System.Windows.Forms.ListView();
            this.ChMemoryDetailsKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ChMemoryDetailsValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TpStatistics = new System.Windows.Forms.TabPage();
            this.RtxtStatistics = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LblVersion = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.TpVersion.SuspendLayout();
            this.TpReleaseNotes.SuspendLayout();
            this.TpFeatures.SuspendLayout();
            this.TpRoadmap.SuspendLayout();
            this.TpMemory.SuspendLayout();
            this.TpStatistics.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnClose
            // 
            this.BtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnClose.Location = new System.Drawing.Point(366, 3);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(75, 23);
            this.BtnClose.TabIndex = 1;
            this.BtnClose.Text = "Close";
            this.BtnClose.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.TpVersion);
            this.tabControl1.Controls.Add(this.TpReleaseNotes);
            this.tabControl1.Controls.Add(this.TpFeatures);
            this.tabControl1.Controls.Add(this.TpRoadmap);
            this.tabControl1.Controls.Add(this.TpMemory);
            this.tabControl1.Controls.Add(this.TpStatistics);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(444, 232);
            this.tabControl1.TabIndex = 2;
            // 
            // TpVersion
            // 
            this.TpVersion.Controls.Add(this.LblVersion);
            this.TpVersion.Location = new System.Drawing.Point(4, 22);
            this.TpVersion.Name = "TpVersion";
            this.TpVersion.Padding = new System.Windows.Forms.Padding(3);
            this.TpVersion.Size = new System.Drawing.Size(436, 206);
            this.TpVersion.TabIndex = 5;
            this.TpVersion.Text = "Version";
            this.TpVersion.UseVisualStyleBackColor = true;
            // 
            // TpReleaseNotes
            // 
            this.TpReleaseNotes.Controls.Add(this.RtxtReleaseNotes);
            this.TpReleaseNotes.Location = new System.Drawing.Point(4, 22);
            this.TpReleaseNotes.Name = "TpReleaseNotes";
            this.TpReleaseNotes.Padding = new System.Windows.Forms.Padding(3);
            this.TpReleaseNotes.Size = new System.Drawing.Size(436, 206);
            this.TpReleaseNotes.TabIndex = 0;
            this.TpReleaseNotes.Text = "Release Notes";
            this.TpReleaseNotes.UseVisualStyleBackColor = true;
            // 
            // RtxtReleaseNotes
            // 
            this.RtxtReleaseNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RtxtReleaseNotes.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RtxtReleaseNotes.Location = new System.Drawing.Point(3, 3);
            this.RtxtReleaseNotes.Name = "RtxtReleaseNotes";
            this.RtxtReleaseNotes.ReadOnly = true;
            this.RtxtReleaseNotes.Size = new System.Drawing.Size(430, 200);
            this.RtxtReleaseNotes.TabIndex = 1;
            this.RtxtReleaseNotes.Text = "";
            // 
            // TpFeatures
            // 
            this.TpFeatures.Controls.Add(this.RtxtFeatures);
            this.TpFeatures.Location = new System.Drawing.Point(4, 22);
            this.TpFeatures.Name = "TpFeatures";
            this.TpFeatures.Padding = new System.Windows.Forms.Padding(3);
            this.TpFeatures.Size = new System.Drawing.Size(436, 206);
            this.TpFeatures.TabIndex = 1;
            this.TpFeatures.Text = "Features";
            this.TpFeatures.UseVisualStyleBackColor = true;
            // 
            // RtxtFeatures
            // 
            this.RtxtFeatures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RtxtFeatures.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RtxtFeatures.Location = new System.Drawing.Point(3, 3);
            this.RtxtFeatures.Name = "RtxtFeatures";
            this.RtxtFeatures.ReadOnly = true;
            this.RtxtFeatures.Size = new System.Drawing.Size(430, 200);
            this.RtxtFeatures.TabIndex = 0;
            this.RtxtFeatures.Text = "";
            // 
            // TpRoadmap
            // 
            this.TpRoadmap.Controls.Add(this.RtxtRoadmap);
            this.TpRoadmap.Location = new System.Drawing.Point(4, 22);
            this.TpRoadmap.Name = "TpRoadmap";
            this.TpRoadmap.Padding = new System.Windows.Forms.Padding(3);
            this.TpRoadmap.Size = new System.Drawing.Size(436, 206);
            this.TpRoadmap.TabIndex = 2;
            this.TpRoadmap.Text = "Roadmap";
            this.TpRoadmap.UseVisualStyleBackColor = true;
            // 
            // RtxtRoadmap
            // 
            this.RtxtRoadmap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RtxtRoadmap.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RtxtRoadmap.Location = new System.Drawing.Point(3, 3);
            this.RtxtRoadmap.Name = "RtxtRoadmap";
            this.RtxtRoadmap.ReadOnly = true;
            this.RtxtRoadmap.Size = new System.Drawing.Size(430, 200);
            this.RtxtRoadmap.TabIndex = 0;
            this.RtxtRoadmap.Text = "";
            // 
            // TpMemory
            // 
            this.TpMemory.Controls.Add(this.LvMemoryDetails);
            this.TpMemory.Location = new System.Drawing.Point(4, 22);
            this.TpMemory.Name = "TpMemory";
            this.TpMemory.Padding = new System.Windows.Forms.Padding(3);
            this.TpMemory.Size = new System.Drawing.Size(436, 206);
            this.TpMemory.TabIndex = 3;
            this.TpMemory.Text = "Memory Details";
            this.TpMemory.UseVisualStyleBackColor = true;
            // 
            // LvMemoryDetails
            // 
            this.LvMemoryDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ChMemoryDetailsKey,
            this.ChMemoryDetailsValue});
            this.LvMemoryDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LvMemoryDetails.Location = new System.Drawing.Point(3, 3);
            this.LvMemoryDetails.Name = "LvMemoryDetails";
            this.LvMemoryDetails.Size = new System.Drawing.Size(430, 200);
            this.LvMemoryDetails.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.LvMemoryDetails.TabIndex = 0;
            this.LvMemoryDetails.UseCompatibleStateImageBehavior = false;
            this.LvMemoryDetails.View = System.Windows.Forms.View.Details;
            // 
            // ChMemoryDetailsKey
            // 
            this.ChMemoryDetailsKey.Text = "Key";
            // 
            // ChMemoryDetailsValue
            // 
            this.ChMemoryDetailsValue.Text = "Value";
            // 
            // TpStatistics
            // 
            this.TpStatistics.Controls.Add(this.RtxtStatistics);
            this.TpStatistics.Location = new System.Drawing.Point(4, 22);
            this.TpStatistics.Name = "TpStatistics";
            this.TpStatistics.Padding = new System.Windows.Forms.Padding(3);
            this.TpStatistics.Size = new System.Drawing.Size(436, 206);
            this.TpStatistics.TabIndex = 4;
            this.TpStatistics.Text = "Statistics";
            this.TpStatistics.UseVisualStyleBackColor = true;
            // 
            // RtxtStatistics
            // 
            this.RtxtStatistics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RtxtStatistics.Location = new System.Drawing.Point(3, 3);
            this.RtxtStatistics.Name = "RtxtStatistics";
            this.RtxtStatistics.Size = new System.Drawing.Size(430, 200);
            this.RtxtStatistics.TabIndex = 0;
            this.RtxtStatistics.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 232);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(444, 30);
            this.panel1.TabIndex = 3;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel1Paint);
            // 
            // LblVersion
            // 
            this.LblVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblVersion.Location = new System.Drawing.Point(3, 3);
            this.LblVersion.Name = "LblVersion";
            this.LblVersion.Size = new System.Drawing.Size(430, 200);
            this.LblVersion.TabIndex = 0;
            this.LblVersion.Text = "Unknown";
            this.LblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AboutForm
            // 
            this.AcceptButton = this.BtnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnClose;
            this.ClientSize = new System.Drawing.Size(444, 262);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Name = "AboutForm";
            this.Text = "About NeoMem2";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AboutFormFormClosed);
            this.Load += new System.EventHandler(this.AboutFormLoad);
            this.tabControl1.ResumeLayout(false);
            this.TpVersion.ResumeLayout(false);
            this.TpReleaseNotes.ResumeLayout(false);
            this.TpFeatures.ResumeLayout(false);
            this.TpRoadmap.ResumeLayout(false);
            this.TpMemory.ResumeLayout(false);
            this.TpStatistics.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage TpReleaseNotes;
        private System.Windows.Forms.RichTextBox RtxtReleaseNotes;
        private System.Windows.Forms.TabPage TpFeatures;
        private System.Windows.Forms.RichTextBox RtxtFeatures;
        private System.Windows.Forms.TabPage TpRoadmap;
        private System.Windows.Forms.RichTextBox RtxtRoadmap;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabPage TpMemory;
        private System.Windows.Forms.ListView LvMemoryDetails;
        private System.Windows.Forms.ColumnHeader ChMemoryDetailsKey;
        private System.Windows.Forms.ColumnHeader ChMemoryDetailsValue;
        private System.Windows.Forms.TabPage TpStatistics;
        private System.Windows.Forms.RichTextBox RtxtStatistics;
        private System.Windows.Forms.TabPage TpVersion;
        private System.Windows.Forms.Label LblVersion;
    }
}