using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

using NeoMem2.Core;
using NeoMem2.Gui.Models;
using NeoMem2.Utils;

namespace NeoMem2.Gui
{
    public partial class CleanNotesForm : Form
    {
        private readonly Model m_Model;

        public CleanNotesForm(Model model)
            : this()
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            m_Model = model;
        }

        public CleanNotesForm()
        {
            InitializeComponent();
        }

        private void BtnAnalyse_Click(object sender, EventArgs e)
        {
            BtnCancel.Enabled = true;
            LvNoteIssues.Items.Clear();

            var arguments = new AnalysisArguments { AnalyseFileNotes = ChkAnalyseFileNotes.Checked, AnalyseAttachments = ChkAnalyseAttachments.Checked };
            BwAnalyse.RunWorkerAsync(arguments);
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            BwAnalyse.CancelAsync();
            BwProcess.CancelAsync();
        }

        private void BtnProcess_Click(object sender, EventArgs e)
        {
            BtnCancel.Enabled = true;

            var issuesToProcess = new List<Issue>();
            foreach (ListViewItem item in LvNoteIssues.Items)
            {
                if (item.Checked)
                {
                    issuesToProcess.Add((Issue)item.Tag);
                }
            }

            if (issuesToProcess.Count > 0)
            {
                BwProcess.RunWorkerAsync(issuesToProcess);
            }
        }

        private void BwAnalyse_DoWork(object sender, DoWorkEventArgs e)
        {
            var arguments = (AnalysisArguments)e.Argument;
            var result = new AnalysisResult();

            List<Note> fileNotes;
            List<Note> nonFileNotes;

            if (arguments.AnalyseFileNotes)
            {
                BwAnalyse.ReportProgress(0, "Retrieving file notes for analysis...");
                fileNotes = m_Model.GetFiles().ToList();
            }
            else
            {
                fileNotes = new List<Note>();
            }

            if (arguments.AnalyseAttachments)
            {
                BwAnalyse.ReportProgress(0, "Retrieving notes for attachment analysis...");
                nonFileNotes = m_Model.GetActiveNotes().ToList();
            }
            else
            {
                nonFileNotes = new List<Note>();
            }

            ThreadStart del = delegate
            {
                toolStripProgressBar1.Maximum = fileNotes.Count + nonFileNotes.Count;
            };
            Invoke(del);

            if (arguments.AnalyseAttachments)
            {
                AnalyseAttachments(result, nonFileNotes);
            }

            if (arguments.AnalyseFileNotes)
            {
                AnalyseFileNotes(result, fileNotes);
            }

            e.Result = result;
        }

        private void AnalyseFileNotes(AnalysisResult results, List<Note> fileNotes)
        {
            int noteCounter = 0;
            foreach (Note note in fileNotes)
            {
                if (BwAnalyse.CancellationPending)
                {
                    break;
                }

                var fileNote = new FileNoteWrapper(note);
                if (!File.Exists(fileNote.Path))
                {
                    results.Issues.Add(new DeleteFileNoteIssue(m_Model, fileNote, "File note does not reference a file that exists"));
                }

                var verifier = new FileImportVerifier();
                if (!verifier.IsValid(fileNote))
                {
                    results.Issues.Add(new DeleteFileNoteIssue(m_Model, fileNote, "File is in disallowed list"));
                }

                results.FileNotesAnalysed++;
                noteCounter++;

                if (noteCounter % 10 == 0 || noteCounter == fileNotes.Count)
                {
                    string status = string.Format("Analysed {0}/{1} file notes", noteCounter, fileNotes.Count);
                    BwAnalyse.ReportProgress(results.TotalNotesAnalysed, status);
                }
            }
        }

        private void AnalyseAttachments(AnalysisResult results, List<Note> nonFileNotes)
        {
            int noteCounter = 0;
            foreach (var note in nonFileNotes)
            {
                if (BwAnalyse.CancellationPending) break;

                foreach (var attachment in note.Attachments)
                {
                    if (!File.Exists(attachment.Filename))
                    {
                        results.Issues.Add(new NoteAttachmentDoesNotExistsIssue(m_Model, attachment));
                    }

                    results.AttachmentsAnalysed++;
                }

                results.NotesAnalysed++;
                noteCounter++;

                if (noteCounter % 10 == 0 || noteCounter == nonFileNotes.Count)
                {
                    string status = string.Format("Analysed {0}/{1} notes for attachments", noteCounter, nonFileNotes.Count);
                    BwAnalyse.ReportProgress(results.TotalNotesAnalysed, status);
                }
            }
        }

        private void BwAnalyse_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            SetProgress(e.ProgressPercentage, e.UserState.ToString());
        }

        private void BwAnalyse_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BtnCancel.Enabled = false;

            string status;
            if (e.Cancelled)
            {
                status = "Analysis cancelled";
            }
            else if (e.Error != null)
            {
                status = e.Error.ToString();
            }
            else
            {
                var result = (AnalysisResult)e.Result;
                status = result.ToString();
                SetStatus(status + ", displaying results");

                LvNoteIssues.BeginUpdate();
                try
                {
                    foreach (var issue in result.Issues)
                    {
                        var item = new ListViewItem();
                        item.Text = issue.Problem;
                        item.SubItems.Add(issue.Detail);
                        item.SubItems.Add(issue.Solution);
                        item.Tag = issue;
                        LvNoteIssues.Items.Add(item);
                    }

                    ColumnHeaderAutoResizeStyle resizeStyle = result.Issues.Count > 0 ? ColumnHeaderAutoResizeStyle.ColumnContent : ColumnHeaderAutoResizeStyle.HeaderSize;
                    foreach (ColumnHeader header in LvNoteIssues.Columns)
                    {
                        header.AutoResize(resizeStyle);
                    }
                }
                finally
                {
                    LvNoteIssues.EndUpdate();
                }
            }

            SetStatus(status);
        }

        private void BwProcess_DoWork(object sender, DoWorkEventArgs e)
        {
            var issuesToProcess = (List<Issue>)e.Argument;

            int issueCounter = 0;
            foreach (var issue in issuesToProcess)
            {
                if (BwProcess.CancellationPending) break;
                
                issue.Solve();

                issueCounter++;
                string status = string.Format("Processed {0}/{1} issues", issueCounter, issuesToProcess.Count);
                BwProcess.ReportProgress(Math2.CalculatePercentage(issueCounter, issuesToProcess.Count), status);
            }

            e.Result = issuesToProcess;
        }

        private void BwProcess_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            SetProgress(e.ProgressPercentage, e.UserState.ToString());
        }

        private void BwProcess_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BtnCancel.Enabled = false;

            string status;
            if (e.Cancelled)
            {
                status = "Processing cancelled";
            }
            else if (e.Error != null)
            {
                status = e.Error.ToString();
            }
            else
            {
                var issuesProcessed = (List<Issue>)e.Result;
                status = string.Format("{0} issues processed", issuesProcessed.Count);
            }

            SetStatus(status);
        }

        private void SetProgress(int progressPercentage, string message)
        {
            toolStripProgressBar1.Value = Math.Min(toolStripProgressBar1.Maximum, Math.Max(toolStripProgressBar1.Minimum, progressPercentage));
            SetStatus(message);
        }

        private void SetStatus(string message)
        {
            LblStatus.Text = message;
            LblStatus.Visible = !string.IsNullOrEmpty(message);
            LblStatus.Invalidate();
        }

        private void MiDeselectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LvNoteIssues.Items)
            {
                item.Checked = false;
            }
        }

        private void MiSelectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LvNoteIssues.Items)
            {
                item.Checked = true;
            }
        }

        private void MiInvertSelection_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LvNoteIssues.Items)
            {
                item.Checked = !item.Checked;
            }
        }

        private void MiSelectAllAttachmentIssues_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LvNoteIssues.Items)
            {
                item.Checked = item.Tag is NoteAttachmentDoesNotExistsIssue;
            }
        }

        private void MiSelectAllFileNoteIssues_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in LvNoteIssues.Items)
            {
                item.Checked = item.Tag is DeleteFileNoteIssue;
            }
        }

        private void LvNoteIssues_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            IssueSelectionChanged();
        }

        private void IssueSelectionChanged()
        {
            bool itemsToProcess = false;
            foreach (ListViewItem item in LvNoteIssues.Items)
            {
                if (item.Checked)
                {
                    itemsToProcess = true;
                    break;
                }
            }

            BtnProcess.Enabled = itemsToProcess;
        }
    }

    public class AnalysisArguments
    {
        public bool AnalyseAttachments { get; set; }

        public bool AnalyseFileNotes { get; set; }
    }

    public class AnalysisResult
    {
        public List<Issue> Issues { get; } = new List<Issue>();
        public int FileNotesAnalysed { get; set; }
        public int NotesAnalysed { get; set; }
        public int AttachmentsAnalysed { get; set; }

        public int TotalNotesAnalysed
        {
            get { return FileNotesAnalysed + NotesAnalysed; }
        }

        public override string ToString()
        {
            return String.Format(
                "{0} issues, out of {1} notes, {2} attachments, {3} file notes",
                Issues.Count,
                NotesAnalysed,
                AttachmentsAnalysed,
                FileNotesAnalysed);
        }
    }

    public abstract class Issue
    {
        protected Issue(Model model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            Model = model;
        }

        protected Model Model { get; }

        public string Problem { get; protected set; }

        public string Detail { get; protected set; }

        public string Solution { get; protected set; }

        public abstract void Solve();
    }
    
    public class DeleteFileNoteIssue : Issue
    {
        public DeleteFileNoteIssue(Model model, FileNoteWrapper fileNote, string reason)
            : base(model)
        {
            if (fileNote == null) throw new ArgumentNullException(nameof(fileNote));

            FileNote = fileNote;
            Problem = reason;
            Detail = FileNote.Path;
            Solution = "Delete note";
        }

        public FileNoteWrapper FileNote { get; }

        public override void Solve()
        {
            Model.Delete(FileNote.Note);
        }
    }

    public class NoteAttachmentDoesNotExistsIssue : Issue
    {
        public NoteAttachmentDoesNotExistsIssue(Model model, Attachment attachment)
            : base(model)
        {
            if (attachment == null) throw new ArgumentNullException(nameof(attachment));

            Attachment = attachment;
            Problem = "Note attachment does not reference a file that exists";
            Detail = Attachment.Filename;
            Solution = "Delete attachment";
        }

        public Attachment Attachment { get; }

        public override void Solve()
        {
            Attachment.IsDeleted = true;
            Model.Save(Attachment.Note);
        }
    }
}
