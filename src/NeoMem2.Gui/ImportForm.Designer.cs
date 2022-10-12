namespace NeoMem2.Gui
{
    partial class ImportForm
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
            this.CboSourceFormat = new System.Windows.Forms.ComboBox();
            this.CboInputFile = new System.Windows.Forms.ComboBox();
            this.CboOutputFolder = new System.Windows.Forms.ComboBox();
            this.LblInput = new System.Windows.Forms.Label();
            this.LblOutputFolder = new System.Windows.Forms.Label();
            this.BtnBrowseInputFile = new System.Windows.Forms.Button();
            this.BtnBrowseOutputFolder = new System.Windows.Forms.Button();
            this.BtnOK = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.CboDestinationFormat = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // CboSourceFormat
            // 
            this.CboSourceFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CboSourceFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboSourceFormat.FormattingEnabled = true;
            this.CboSourceFormat.Location = new System.Drawing.Point(49, 12);
            this.CboSourceFormat.Name = "CboSourceFormat";
            this.CboSourceFormat.Size = new System.Drawing.Size(475, 21);
            this.CboSourceFormat.TabIndex = 0;
            // 
            // CboInputFile
            // 
            this.CboInputFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CboInputFile.FormattingEnabled = true;
            this.CboInputFile.Location = new System.Drawing.Point(49, 39);
            this.CboInputFile.Name = "CboInputFile";
            this.CboInputFile.Size = new System.Drawing.Size(475, 21);
            this.CboInputFile.TabIndex = 1;
            // 
            // CboOutputFolder
            // 
            this.CboOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CboOutputFolder.FormattingEnabled = true;
            this.CboOutputFolder.Location = new System.Drawing.Point(49, 93);
            this.CboOutputFolder.Name = "CboOutputFolder";
            this.CboOutputFolder.Size = new System.Drawing.Size(475, 21);
            this.CboOutputFolder.TabIndex = 3;
            // 
            // LblInput
            // 
            this.LblInput.AutoSize = true;
            this.LblInput.Location = new System.Drawing.Point(12, 15);
            this.LblInput.Name = "LblInput";
            this.LblInput.Size = new System.Drawing.Size(31, 13);
            this.LblInput.TabIndex = 4;
            this.LblInput.Text = "Input";
            // 
            // LblOutputFolder
            // 
            this.LblOutputFolder.AutoSize = true;
            this.LblOutputFolder.Location = new System.Drawing.Point(4, 69);
            this.LblOutputFolder.Name = "LblOutputFolder";
            this.LblOutputFolder.Size = new System.Drawing.Size(39, 13);
            this.LblOutputFolder.TabIndex = 5;
            this.LblOutputFolder.Text = "Output";
            // 
            // BtnBrowseInputFile
            // 
            this.BtnBrowseInputFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnBrowseInputFile.Location = new System.Drawing.Point(530, 37);
            this.BtnBrowseInputFile.Name = "BtnBrowseInputFile";
            this.BtnBrowseInputFile.Size = new System.Drawing.Size(25, 23);
            this.BtnBrowseInputFile.TabIndex = 2;
            this.BtnBrowseInputFile.Text = "...";
            this.BtnBrowseInputFile.UseVisualStyleBackColor = true;
            this.BtnBrowseInputFile.Click += new System.EventHandler(this.BtnBrowseInputFileClick);
            // 
            // BtnBrowseOutputFolder
            // 
            this.BtnBrowseOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnBrowseOutputFolder.Location = new System.Drawing.Point(530, 91);
            this.BtnBrowseOutputFolder.Name = "BtnBrowseOutputFolder";
            this.BtnBrowseOutputFolder.Size = new System.Drawing.Size(25, 23);
            this.BtnBrowseOutputFolder.TabIndex = 4;
            this.BtnBrowseOutputFolder.Text = "...";
            this.BtnBrowseOutputFolder.UseVisualStyleBackColor = true;
            this.BtnBrowseOutputFolder.Click += new System.EventHandler(this.BtnBrowseOutputFolderClick);
            // 
            // BtnOK
            // 
            this.BtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnOK.Location = new System.Drawing.Point(368, 120);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(75, 23);
            this.BtnOK.TabIndex = 5;
            this.BtnOK.Text = "OK";
            this.BtnOK.UseVisualStyleBackColor = true;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(449, 120);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.BtnCancel.TabIndex = 6;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // CboDestinationFormat
            // 
            this.CboDestinationFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CboDestinationFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboDestinationFormat.FormattingEnabled = true;
            this.CboDestinationFormat.Location = new System.Drawing.Point(49, 66);
            this.CboDestinationFormat.Name = "CboDestinationFormat";
            this.CboDestinationFormat.Size = new System.Drawing.Size(475, 21);
            this.CboDestinationFormat.TabIndex = 7;
            // 
            // ImportForm
            // 
            this.AcceptButton = this.BtnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(562, 146);
            this.Controls.Add(this.CboDestinationFormat);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.BtnBrowseOutputFolder);
            this.Controls.Add(this.BtnBrowseInputFile);
            this.Controls.Add(this.LblOutputFolder);
            this.Controls.Add(this.LblInput);
            this.Controls.Add(this.CboOutputFolder);
            this.Controls.Add(this.CboInputFile);
            this.Controls.Add(this.CboSourceFormat);
            this.Name = "ImportForm";
            this.Text = "Import/Export";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CboSourceFormat;
        private System.Windows.Forms.ComboBox CboInputFile;
        private System.Windows.Forms.ComboBox CboOutputFolder;
        private System.Windows.Forms.Label LblInput;
        private System.Windows.Forms.Label LblOutputFolder;
        private System.Windows.Forms.Button BtnBrowseInputFile;
        private System.Windows.Forms.Button BtnBrowseOutputFolder;
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ComboBox CboDestinationFormat;
    }
}