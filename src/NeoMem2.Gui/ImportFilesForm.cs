using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;

using NeoMem2.Core;
using NeoMem2.Gui.Models;
using NLog;

namespace NeoMem2.Gui
{
    public partial class ImportFilesForm : Form
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        private readonly Model m_Model;
        private readonly FileImportVerifier m_Verifier = new FileImportVerifier();

        public ImportFilesForm(Model model)
            : this()
        {
            m_Model = model;
        }

        public ImportFilesForm()
        {
            InitializeComponent();
        }

        private void BtnBrowseFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                CboFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void BtnBrowseFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                CboFile.Text = openFileDialog1.FileName;
            }
        }

        private void OptMultipleFiles_CheckedChanged(object sender, EventArgs e)
        {
            CboFolder.Enabled = OptMultipleFiles.Checked;
            CboFilter.Enabled = OptMultipleFiles.Checked;
            BtnBrowseFolder.Enabled = OptMultipleFiles.Checked;

            CboFile.Enabled = !OptMultipleFiles.Checked;
            BtnBrowseFile.Enabled = !OptMultipleFiles.Checked;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Import files?", "Import Files", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.Yes)
            {
                BtnOK.Enabled = false;
                BtnCancel.Enabled = true;

                var options = new ImportOptions
                {
                    MultipleFiles = OptMultipleFiles.Checked,
                    Path = CboFolder.Text,
                    Filter = CboFilter.Text,
                    Recurse = ChkRecurse.Checked
                };

                toolStripProgressBar1.Minimum = 0;
                toolStripProgressBar1.Maximum = 100;
                toolStripProgressBar1.Value = 100;
                backgroundWorker1.RunWorkerAsync(options);
            }
        }

        private List<string> GetFiles(ImportOptions options)
        {
            List<string> files = new List<string>();
            if (options.MultipleFiles)
            {
                if (options.Recurse)
                {
                    files = GetFilesFromFolder(options.Path, options.Filter);
                }
                else
                {
                    files = new List<string>(Directory.GetFiles(options.Path, options.Filter, SearchOption.TopDirectoryOnly));
                }
            }
            else
            {
                files = new List<string> {options.Path};
            }

            return files;
        }

        private List<string> GetFilesFromFolder(string path, string filter)
        {
            int totalFolderCount = 0;
            int foldersProcessedCount = 0;

            var folders = new Queue<string>();
            folders.Enqueue(path);
            totalFolderCount++;

            List<string> files = new List<string>();
            while (folders.Count > 0 && !backgroundWorker1.CancellationPending)
            {
                string folder = folders.Dequeue();
                foldersProcessedCount++;
                try
                {
                    files.AddRange(Directory.GetFiles(folder, filter, SearchOption.TopDirectoryOnly));
                }
                catch (Exception ex)
                {
                    Logger.ErrorException($"Failed to import files from folder: [{folder}]", ex);
                }

                foreach (string subFolder in Directory.GetDirectories(folder))
                {
                    folders.Enqueue(subFolder);
                    totalFolderCount++;
                }
                
                int percentageComplete = (foldersProcessedCount / totalFolderCount) * 100;
                backgroundWorker1.ReportProgress(percentageComplete, $"Finding files, {files.Count} files so far, {folder.Length} subfolders remain: {folder}");
            }

            return files;
        }

        private Dictionary<string, Note> GetFilesByPath()
        {
            Dictionary<string, Note> filesByPath = new Dictionary<string, Note>();

            IEnumerable<Note> files = m_Model.GetFiles();
            foreach (Note file in files)
            {
                string filename = file.Text.ToUpper();
                filesByPath[filename] = file;
            }

            return filesByPath;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (m_Model == null) throw new NullReferenceException("Model should have been specified in constructor");

            var options = (ImportOptions) e.Argument;
            backgroundWorker1.ReportProgress(0, "Finding Files");

            List<string> paths = GetFiles(options);

            for (int pathIndex = paths.Count - 1; pathIndex >= 0; pathIndex--)
            {
                if (!m_Verifier.IsValid(paths[pathIndex]))
                {
                    paths.RemoveAt(pathIndex);
                }
            }
            
            ThreadStart del = delegate
            {
                toolStripProgressBar1.Maximum = paths.Count;
                toolStripProgressBar1.Value = toolStripProgressBar1.Minimum;
            };
            Invoke(del);

            Dictionary<string, Note> filesByPath = GetFilesByPath();

            int updatededCount = 0;
            int importedCount = 0;
            ImportResult result = new ImportResult();
            for (int pathCounter = 0; pathCounter < paths.Count && !backgroundWorker1.CancellationPending; pathCounter++)
            {
                string path = paths[pathCounter];

                if (!filesByPath.ContainsKey(path.ToUpper()))
                {
                    result.Notes.Add(ImportFile(path));
                    importedCount++;
                }
                else
                {
                    Note note = filesByPath[path.ToUpper()];
                    UpdateFile(note, path);
                    updatededCount++;
                }
                
                string progressMessage = string.Format(
                    "Processing file {0}/{1}: {2}",
                    pathCounter + 1,
                    paths.Count,
                    path);
                backgroundWorker1.ReportProgress(pathCounter + 1, progressMessage);
            }

            result.SummaryMessage = string.Format("{0}{1} new files imported, {2} updated", backgroundWorker1.CancellationPending ? "Cancelled, " : "", importedCount, updatededCount);
            e.Result = result;
        }

        private Note ImportFile(string path)
        {
            var note = m_Model.CreateNewNote(attach: false);
            var fileNote = FileNoteWrapper.ConvertToFileNote(note, path);
            m_Model.Save(fileNote.Note);
            return fileNote.Note;
        }

        private void UpdateFile(Note note, string path)
        {
            m_Model.Save(FileNoteWrapper.ConvertToFileNote(note, path).Note);
        }
        
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripStatusLabel1.Text = e.UserState.ToString();
            toolStripProgressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ImportResult result = (ImportResult)e.Result;
            m_Model.AttachNotes(result.Notes);

            toolStripProgressBar1.Value = toolStripProgressBar1.Maximum;
            toolStripStatusLabel1.Text = result.SummaryMessage;

            BtnOK.Enabled = true;
            BtnCancel.Enabled = false;
        }

        public class ImportResult
        {
            public string SummaryMessage;

            public List<Note> Notes = new List<Note>();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        public class ImportOptions
        {
            public string Path { get; set; }
            public string Filter { get; set; }
            public bool Recurse { get; set; }
            public bool MultipleFiles { get; set; }
        }
    }
}
