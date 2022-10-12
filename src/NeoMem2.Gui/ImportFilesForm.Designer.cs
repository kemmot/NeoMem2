namespace NeoMem2.Gui
{
    partial class ImportFilesForm
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
            this.BtnOK = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.CboFolder = new System.Windows.Forms.ComboBox();
            this.BtnBrowseFolder = new System.Windows.Forms.Button();
            this.LblFolder = new System.Windows.Forms.Label();
            this.LblFilter = new System.Windows.Forms.Label();
            this.CboFilter = new System.Windows.Forms.ComboBox();
            this.OptMultipleFiles = new System.Windows.Forms.RadioButton();
            this.OptSingleFile = new System.Windows.Forms.RadioButton();
            this.CboFile = new System.Windows.Forms.ComboBox();
            this.BtnBrowseFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.LblFile = new System.Windows.Forms.Label();
            this.ChkRecurse = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnOK
            // 
            this.BtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOK.Location = new System.Drawing.Point(320, 93);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(75, 23);
            this.BtnOK.TabIndex = 10;
            this.BtnOK.Text = "Import";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.Enabled = false;
            this.BtnCancel.Location = new System.Drawing.Point(401, 93);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.BtnCancel.TabIndex = 11;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // CboFolder
            // 
            this.CboFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CboFolder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CboFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.CboFolder.FormattingEnabled = true;
            this.CboFolder.Location = new System.Drawing.Point(147, 12);
            this.CboFolder.Name = "CboFolder";
            this.CboFolder.Size = new System.Drawing.Size(329, 21);
            this.CboFolder.TabIndex = 2;
            // 
            // BtnBrowseFolder
            // 
            this.BtnBrowseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnBrowseFolder.Location = new System.Drawing.Point(482, 10);
            this.BtnBrowseFolder.Name = "BtnBrowseFolder";
            this.BtnBrowseFolder.Size = new System.Drawing.Size(27, 23);
            this.BtnBrowseFolder.TabIndex = 3;
            this.BtnBrowseFolder.Text = "...";
            this.BtnBrowseFolder.UseVisualStyleBackColor = true;
            this.BtnBrowseFolder.Click += new System.EventHandler(this.BtnBrowseFolder_Click);
            // 
            // LblFolder
            // 
            this.LblFolder.AutoSize = true;
            this.LblFolder.Location = new System.Drawing.Point(105, 15);
            this.LblFolder.Name = "LblFolder";
            this.LblFolder.Size = new System.Drawing.Size(36, 13);
            this.LblFolder.TabIndex = 1;
            this.LblFolder.Text = "Folder";
            // 
            // LblFilter
            // 
            this.LblFilter.AutoSize = true;
            this.LblFilter.Location = new System.Drawing.Point(112, 42);
            this.LblFilter.Name = "LblFilter";
            this.LblFilter.Size = new System.Drawing.Size(29, 13);
            this.LblFilter.TabIndex = 4;
            this.LblFilter.Text = "Filter";
            // 
            // CboFilter
            // 
            this.CboFilter.FormattingEnabled = true;
            this.CboFilter.Location = new System.Drawing.Point(147, 39);
            this.CboFilter.Name = "CboFilter";
            this.CboFilter.Size = new System.Drawing.Size(149, 21);
            this.CboFilter.TabIndex = 5;
            this.CboFilter.Text = "*";
            // 
            // OptMultipleFiles
            // 
            this.OptMultipleFiles.AutoSize = true;
            this.OptMultipleFiles.Checked = true;
            this.OptMultipleFiles.Location = new System.Drawing.Point(14, 13);
            this.OptMultipleFiles.Name = "OptMultipleFiles";
            this.OptMultipleFiles.Size = new System.Drawing.Size(85, 17);
            this.OptMultipleFiles.TabIndex = 0;
            this.OptMultipleFiles.TabStop = true;
            this.OptMultipleFiles.Text = "Multiple Files";
            this.OptMultipleFiles.UseVisualStyleBackColor = true;
            this.OptMultipleFiles.CheckedChanged += new System.EventHandler(this.OptMultipleFiles_CheckedChanged);
            // 
            // OptSingleFile
            // 
            this.OptSingleFile.AutoSize = true;
            this.OptSingleFile.Location = new System.Drawing.Point(14, 67);
            this.OptSingleFile.Name = "OptSingleFile";
            this.OptSingleFile.Size = new System.Drawing.Size(73, 17);
            this.OptSingleFile.TabIndex = 6;
            this.OptSingleFile.Text = "Single File";
            this.OptSingleFile.UseVisualStyleBackColor = true;
            // 
            // CboFile
            // 
            this.CboFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CboFile.Enabled = false;
            this.CboFile.FormattingEnabled = true;
            this.CboFile.Location = new System.Drawing.Point(147, 66);
            this.CboFile.Name = "CboFile";
            this.CboFile.Size = new System.Drawing.Size(329, 21);
            this.CboFile.TabIndex = 8;
            // 
            // BtnBrowseFile
            // 
            this.BtnBrowseFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnBrowseFile.Enabled = false;
            this.BtnBrowseFile.Location = new System.Drawing.Point(482, 64);
            this.BtnBrowseFile.Name = "BtnBrowseFile";
            this.BtnBrowseFile.Size = new System.Drawing.Size(27, 23);
            this.BtnBrowseFile.TabIndex = 9;
            this.BtnBrowseFile.Text = "...";
            this.BtnBrowseFile.UseVisualStyleBackColor = true;
            this.BtnBrowseFile.Click += new System.EventHandler(this.BtnBrowseFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // LblFile
            // 
            this.LblFile.AutoSize = true;
            this.LblFile.Location = new System.Drawing.Point(118, 69);
            this.LblFile.Name = "LblFile";
            this.LblFile.Size = new System.Drawing.Size(23, 13);
            this.LblFile.TabIndex = 7;
            this.LblFile.Text = "File";
            // 
            // ChkRecurse
            // 
            this.ChkRecurse.AutoSize = true;
            this.ChkRecurse.Location = new System.Drawing.Point(302, 41);
            this.ChkRecurse.Name = "ChkRecurse";
            this.ChkRecurse.Size = new System.Drawing.Size(66, 17);
            this.ChkRecurse.TabIndex = 12;
            this.ChkRecurse.Text = "Recurse";
            this.ChkRecurse.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 124);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(521, 22);
            this.statusStrip1.TabIndex = 13;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(101, 17);
            this.toolStripStatusLabel1.Text = "Waiting to import";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // ImportFilesForm
            // 
            this.AcceptButton = this.BtnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(521, 146);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ChkRecurse);
            this.Controls.Add(this.LblFile);
            this.Controls.Add(this.BtnBrowseFile);
            this.Controls.Add(this.CboFile);
            this.Controls.Add(this.OptSingleFile);
            this.Controls.Add(this.OptMultipleFiles);
            this.Controls.Add(this.CboFilter);
            this.Controls.Add(this.LblFilter);
            this.Controls.Add(this.LblFolder);
            this.Controls.Add(this.BtnBrowseFolder);
            this.Controls.Add(this.CboFolder);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOK);
            this.Name = "ImportFilesForm";
            this.Text = "Import Files";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.ComboBox CboFolder;
        private System.Windows.Forms.Button BtnBrowseFolder;
        private System.Windows.Forms.Label LblFolder;
        private System.Windows.Forms.Label LblFilter;
        private System.Windows.Forms.ComboBox CboFilter;
        private System.Windows.Forms.RadioButton OptMultipleFiles;
        private System.Windows.Forms.RadioButton OptSingleFile;
        private System.Windows.Forms.ComboBox CboFile;
        private System.Windows.Forms.Button BtnBrowseFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label LblFile;
        private System.Windows.Forms.CheckBox ChkRecurse;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}