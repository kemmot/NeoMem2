namespace NeoMem2.Gui
{
    partial class CleanNotesForm
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
            this.components = new System.ComponentModel.Container();
            this.ChkAnalyseAttachments = new System.Windows.Forms.CheckBox();
            this.ChkAnalyseFileNotes = new System.Windows.Forms.CheckBox();
            this.BtnAnalyse = new System.Windows.Forms.Button();
            this.LvNoteIssues = new System.Windows.Forms.ListView();
            this.ChEntityType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ChIssue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ChSolution = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MiSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.MiDeselectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.MiInvertSelection = new System.Windows.Forms.ToolStripMenuItem();
            this.MiSelectAllAttachmentIssues = new System.Windows.Forms.ToolStripMenuItem();
            this.MiSelectAllFileNoteIssues = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnProcess = new System.Windows.Forms.Button();
            this.BwAnalyse = new System.ComponentModel.BackgroundWorker();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BwProcess = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.LblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChkAnalyseAttachments
            // 
            this.ChkAnalyseAttachments.AutoSize = true;
            this.ChkAnalyseAttachments.Checked = true;
            this.ChkAnalyseAttachments.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkAnalyseAttachments.Location = new System.Drawing.Point(12, 12);
            this.ChkAnalyseAttachments.Name = "ChkAnalyseAttachments";
            this.ChkAnalyseAttachments.Size = new System.Drawing.Size(125, 17);
            this.ChkAnalyseAttachments.TabIndex = 0;
            this.ChkAnalyseAttachments.Text = "Analyse Attachments";
            this.ChkAnalyseAttachments.UseVisualStyleBackColor = true;
            // 
            // ChkAnalyseFileNotes
            // 
            this.ChkAnalyseFileNotes.AutoSize = true;
            this.ChkAnalyseFileNotes.Checked = true;
            this.ChkAnalyseFileNotes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkAnalyseFileNotes.Location = new System.Drawing.Point(12, 41);
            this.ChkAnalyseFileNotes.Name = "ChkAnalyseFileNotes";
            this.ChkAnalyseFileNotes.Size = new System.Drawing.Size(113, 17);
            this.ChkAnalyseFileNotes.TabIndex = 1;
            this.ChkAnalyseFileNotes.Text = "Analyse File Notes";
            this.ChkAnalyseFileNotes.UseVisualStyleBackColor = true;
            // 
            // BtnAnalyse
            // 
            this.BtnAnalyse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAnalyse.Location = new System.Drawing.Point(13, 190);
            this.BtnAnalyse.Name = "BtnAnalyse";
            this.BtnAnalyse.Size = new System.Drawing.Size(75, 23);
            this.BtnAnalyse.TabIndex = 2;
            this.BtnAnalyse.Text = "Analyse";
            this.BtnAnalyse.UseVisualStyleBackColor = true;
            this.BtnAnalyse.Click += new System.EventHandler(this.BtnAnalyse_Click);
            // 
            // LvNoteIssues
            // 
            this.LvNoteIssues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LvNoteIssues.CheckBoxes = true;
            this.LvNoteIssues.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ChEntityType,
            this.ChIssue,
            this.ChSolution});
            this.LvNoteIssues.ContextMenuStrip = this.contextMenuStrip1;
            this.LvNoteIssues.HideSelection = false;
            this.LvNoteIssues.Location = new System.Drawing.Point(12, 66);
            this.LvNoteIssues.Name = "LvNoteIssues";
            this.LvNoteIssues.Size = new System.Drawing.Size(238, 118);
            this.LvNoteIssues.TabIndex = 4;
            this.LvNoteIssues.UseCompatibleStateImageBehavior = false;
            this.LvNoteIssues.View = System.Windows.Forms.View.Details;
            this.LvNoteIssues.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.LvNoteIssues_ItemChecked);
            // 
            // ChEntityType
            // 
            this.ChEntityType.Text = "Entity Type";
            this.ChEntityType.Width = 80;
            // 
            // ChIssue
            // 
            this.ChIssue.Text = "Issue";
            // 
            // ChSolution
            // 
            this.ChSolution.Text = "Solution";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MiSelectAll,
            this.MiDeselectAll,
            this.MiInvertSelection,
            this.MiSelectAllAttachmentIssues,
            this.MiSelectAllFileNoteIssues});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(223, 114);
            // 
            // MiSelectAll
            // 
            this.MiSelectAll.Name = "MiSelectAll";
            this.MiSelectAll.Size = new System.Drawing.Size(222, 22);
            this.MiSelectAll.Text = "Select All";
            this.MiSelectAll.Click += new System.EventHandler(this.MiSelectAll_Click);
            // 
            // MiDeselectAll
            // 
            this.MiDeselectAll.Name = "MiDeselectAll";
            this.MiDeselectAll.Size = new System.Drawing.Size(222, 22);
            this.MiDeselectAll.Text = "Deselect All";
            this.MiDeselectAll.Click += new System.EventHandler(this.MiDeselectAll_Click);
            // 
            // MiInvertSelection
            // 
            this.MiInvertSelection.Name = "MiInvertSelection";
            this.MiInvertSelection.Size = new System.Drawing.Size(222, 22);
            this.MiInvertSelection.Text = "Invert Selection";
            this.MiInvertSelection.Click += new System.EventHandler(this.MiInvertSelection_Click);
            // 
            // MiSelectAllAttachmentIssues
            // 
            this.MiSelectAllAttachmentIssues.Name = "MiSelectAllAttachmentIssues";
            this.MiSelectAllAttachmentIssues.Size = new System.Drawing.Size(222, 22);
            this.MiSelectAllAttachmentIssues.Text = "Select All Attachment Issues";
            this.MiSelectAllAttachmentIssues.Click += new System.EventHandler(this.MiSelectAllAttachmentIssues_Click);
            // 
            // MiSelectAllFileNoteIssues
            // 
            this.MiSelectAllFileNoteIssues.Name = "MiSelectAllFileNoteIssues";
            this.MiSelectAllFileNoteIssues.Size = new System.Drawing.Size(222, 22);
            this.MiSelectAllFileNoteIssues.Text = "Select All File Note Issues";
            this.MiSelectAllFileNoteIssues.Click += new System.EventHandler(this.MiSelectAllFileNoteIssues_Click);
            // 
            // BtnProcess
            // 
            this.BtnProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnProcess.Enabled = false;
            this.BtnProcess.Location = new System.Drawing.Point(94, 190);
            this.BtnProcess.Name = "BtnProcess";
            this.BtnProcess.Size = new System.Drawing.Size(75, 23);
            this.BtnProcess.TabIndex = 5;
            this.BtnProcess.Text = "Process";
            this.BtnProcess.UseVisualStyleBackColor = true;
            this.BtnProcess.Click += new System.EventHandler(this.BtnProcess_Click);
            // 
            // BwAnalyse
            // 
            this.BwAnalyse.WorkerReportsProgress = true;
            this.BwAnalyse.WorkerSupportsCancellation = true;
            this.BwAnalyse.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BwAnalyse_DoWork);
            this.BwAnalyse.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BwAnalyse_ProgressChanged);
            this.BwAnalyse.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BwAnalyse_RunWorkerCompleted);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.Enabled = false;
            this.BtnCancel.Location = new System.Drawing.Point(175, 190);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.BtnCancel.TabIndex = 6;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BwProcess
            // 
            this.BwProcess.WorkerReportsProgress = true;
            this.BwProcess.WorkerSupportsCancellation = true;
            this.BwProcess.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BwProcess_DoWork);
            this.BwProcess.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BwProcess_ProgressChanged);
            this.BwProcess.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BwProcess_RunWorkerCompleted);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.LblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 216);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(258, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // LblStatus
            // 
            this.LblStatus.Name = "LblStatus";
            this.LblStatus.Size = new System.Drawing.Size(39, 17);
            this.LblStatus.Text = "Status";
            this.LblStatus.Visible = false;
            // 
            // CleanNotesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(258, 238);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnProcess);
            this.Controls.Add(this.LvNoteIssues);
            this.Controls.Add(this.BtnAnalyse);
            this.Controls.Add(this.ChkAnalyseFileNotes);
            this.Controls.Add(this.ChkAnalyseAttachments);
            this.Name = "CleanNotesForm";
            this.Text = "Clean Notes";
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ChkAnalyseAttachments;
        private System.Windows.Forms.CheckBox ChkAnalyseFileNotes;
        private System.Windows.Forms.Button BtnAnalyse;
        private System.Windows.Forms.ListView LvNoteIssues;
        private System.Windows.Forms.ColumnHeader ChEntityType;
        private System.Windows.Forms.ColumnHeader ChIssue;
        private System.Windows.Forms.ColumnHeader ChSolution;
        private System.Windows.Forms.Button BtnProcess;
        private System.ComponentModel.BackgroundWorker BwAnalyse;
        private System.Windows.Forms.Button BtnCancel;
        private System.ComponentModel.BackgroundWorker BwProcess;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel LblStatus;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MiSelectAll;
        private System.Windows.Forms.ToolStripMenuItem MiDeselectAll;
        private System.Windows.Forms.ToolStripMenuItem MiInvertSelection;
        private System.Windows.Forms.ToolStripMenuItem MiSelectAllAttachmentIssues;
        private System.Windows.Forms.ToolStripMenuItem MiSelectAllFileNoteIssues;
    }
}