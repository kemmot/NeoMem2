using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

using NeoMem2.Automation.Updates;
using NeoMem2.Core;
using NeoMem2.Core.Queries;
using NeoMem2.Core.Queries.Batch;
using NeoMem2.Core.Scripting;
using NeoMem2.Core.Stores;
using NeoMem2.Data.Nvpy;
using NeoMem2.Data.Sqlite;
using NeoMem2.Data.SqlServer;
using NeoMem2.Gui.Commands;
using NeoMem2.Gui.Models;
using NeoMem2.Gui.Properties;
using NeoMem2.Utils;
using NeoMem2.Utils.IO;

using NLog;

namespace NeoMem2.Gui
{
    public partial class MainForm : Form
    {
        private const int MaxAutoSaveErrors = 2;

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private int m_AutoSaveErrorCount;
        private readonly CommandManager m_CommandManager = new CommandManager();
        private NoteForm m_CurrentNoteForm;
        private readonly NoteQueryOptions m_DefaultNoteQueryOptions = new NoteQueryOptions { Separator = " " };
        private readonly NoteListModel m_NoteListModel;
        private NoteTreeNode m_TempSelectedNoteNode;
        private readonly GcMonitor m_GcMonitor = new GcMonitor();
        private readonly Dictionary<TabPage, NoteForm> m_TabForms = new Dictionary<TabPage, NoteForm>();
        private readonly Dictionary<NoteForm, TabPage> m_FormTabs =  new Dictionary<NoteForm, TabPage>();


        public MainForm()
        {
            InitializeComponent();

            AppDomain.CurrentDomain.UnhandledException += CurrentDomainUnhandledException;

            m_NoteListModel = new NoteListModel(LvNotes);
            m_NoteListModel.CurrentNoteChanged += m_NoteListModel_CurrentNoteChanged;
            m_NoteListModel.ExceptionOccurred += CommandManagerExceptionOccurred;

            m_GcMonitor.GcStatusChanged += GcMonitorOnGcStatusChanged;
            m_GcMonitor.Start();
        }

        private void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            using (ExceptionForm form = new ExceptionForm())
            {
                Exception ex = e.ExceptionObject as Exception;
                if (ex != null)
                {
                    form.SetException(ex);
                    form.ShowDialog(this);
                }
            }
        }

        void m_NoteListModel_CurrentNoteChanged(object sender, ItemEventArgs<Note> e)
        {
            Model.CurrentNote = e.Item;
        }


        public NoteForm CurrentNoteForm 
        {
            get
            {
                if (m_CurrentNoteForm == null)
                {
                    CreateNoteWindow(true);
                }

                return m_CurrentNoteForm;
            }
            set
            {
                if (m_CurrentNoteForm != null)
                {
                    m_CurrentNoteForm.CurrentNoteChanged -= CurrentNoteFormCurrentNoteChanged;
                }

                m_CurrentNoteForm = value;

                if (m_CurrentNoteForm != null)
                {
                    m_CurrentNoteForm.CurrentNoteChanged += CurrentNoteFormCurrentNoteChanged;

                    TabPage newTab;
                    if (m_FormTabs.TryGetValue(CurrentNoteForm, out newTab))
                    {
                        TcClients.SelectedTab = newTab;
                    }
                }
            }
        }

        public NoteQueryOptions DefaultNoteQueryOptions { get { return m_DefaultNoteQueryOptions; } }

        public GcMonitor GcMonitor { get { return m_GcMonitor; } }
        
        public Model Model { get; private set; }

        public NoteListModel NoteListModel {  get { return m_NoteListModel; } }


        private void BtnBrowseBack_Click(object sender, EventArgs e)
        {
            CurrentNoteForm?.NavigateBackwards();
        }

        private void BtnBrowseForward_Click(object sender, EventArgs e)
        {
            CurrentNoteForm?.NavigateForwards();
        }

        private void CboNoteModelSelectedIndexChanged(object sender, EventArgs e)
        {
            Model.SetModel(((EnumWrapper<NoteModelType>)CboNoteModel.SelectedItem).EnumValue);
        }

        private void CboSearchTextKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (!CboSearchText.Items.Contains(CboSearchText.Text))
                {
                    CboSearchText.Items.Add(CboSearchText.Text);
                }

                Search(false);
            }
        }

        private void CboSearchTextTextChanged(object sender, EventArgs e)
        {
            Search(true);
        }

        private void CboTreeModelSelectedIndexChanged(object sender, EventArgs e)
        {
            Model.SetModel(((EnumWrapper<CategoryModelType>)CboTreeModel.SelectedItem).EnumValue);
        }
        
        private void CmiNotesOpenInNewWindowClick(object sender, EventArgs e)
        {
            var node = m_TempSelectedNoteNode;
            if (node != null)
            {
                m_CommandManager.Execute(new NoteOpenInNewWindowCommand(this, node.Note), e);
            }
        }

        private void CmnuNotesOpening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CmiNotesAddLinkedNote.Enabled = Model.CurrentNote != null;
        }

        private void CommandManagerExceptionOccurred(object sender, ItemEventArgs<Exception> e)
        {
            HandleException(e.Item);
        }
        
        private void CurrentNoteFormCurrentNoteChanged(object sender, ItemEventArgs<Note> e)
        {
            Model.CurrentNote = e.Item;
        }

        private void GcMonitorOnGcStatusChanged(object sender, GcMonitorEventArgs e)
        {
            ThreadStart del = delegate
            {
                string lastMaxGenCollection;
                GcMonitorGenerationDetails maxGen;
                if (e.Generations.TryGetValue(GC.MaxGeneration, out maxGen))
                {
                    lastMaxGenCollection = DateTime.Now.Subtract(maxGen.LastCollectionTime).ToString(@"hh\:mm\:ss") + " ago";
                }
                else
                {
                    lastMaxGenCollection = "Never";
                }

                TslblMemory.Text = string.Format(
                    "{0} (Last Full Collection: {1})",
                    e.TotalMemory,
                    lastMaxGenCollection);
            };
            Invoke(del);
        }

        private void MainFormFormClosing(object sender, FormClosingEventArgs e)
        {
            ExceptionSafeBlock(SaveAllOpenNotes);
            Properties.Settings.Default.MatcherType = CboMatcherType.SelectedItem.ToString();
            Properties.Settings.Default.NoteModelType = ((EnumWrapper<NoteModelType>)CboNoteModel.SelectedItem).EnumValue.ToString();
            Properties.Settings.Default.TreeModelType = ((EnumWrapper<CategoryModelType>)CboTreeModel.SelectedItem).EnumValue.ToString();
            Settings.Default.Save();
            m_GcMonitor.Dispose();
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            try
            {
                StoreFactory.Instance.Register(new SqlServerStoreFactory());

                ExporterFactory.Instance.Register(new HtmlExporter());
                ExporterFactory.Instance.Register(new MarkdownExporter());
                ExporterFactory.Instance.Register(new NvpyJsonWriter());
                ExporterFactory.Instance.Register(new NeoMemFlatFileWriter());
                ExporterFactory.Instance.Register(new SqliteExporter());
                ExporterFactory.Instance.Register(new SqlServerExporter());

                ImporterFactory.Instance.Register(new NeoMem1CsvReader());
                ImporterFactory.Instance.Register(new NvpyImporter());
                ImporterFactory.Instance.Register(new SqlServerImporter());

                m_CommandManager.ExceptionOccurred += CommandManagerExceptionOccurred;
                m_CommandManager.Connect(BtnClear, new ClearSearchCommand(this));
                m_CommandManager.Connect(CmiNotesAddLinkedNote, new NoteNewLinkedCommand(this));
                m_CommandManager.Connect(MiAbout, new ShowAboutCommand(this));
                m_CommandManager.Connect(MiAddLinkedNote, new NoteNewLinkedCommand(this));
                m_CommandManager.Connect(MiBatchChanges, new ShowBatchChangeCommand(this));
                m_CommandManager.Connect(MiExit, new ExitCommand(this));
                m_CommandManager.Connect(MiFileBackup, new FileBackupCommand(this));
                m_CommandManager.Connect(MiFileImport, new FileImportCommand(this));
                m_CommandManager.Connect(MiFileNew, new FileNewCommand(this));
                m_CommandManager.Connect(MiFileOpen, new FileOpenCommand(this));
                m_CommandManager.Connect(MiNewNoteWindow, new NewNoteWindowCommand(this));
                m_CommandManager.Connect(MiNoteDelete, new NoteDeleteCommand(this));
                m_CommandManager.Connect(MiNoteFind, new NoteFindCommand(this));
                m_CommandManager.Connect(MiNoteNew, new NoteNewCommand(this));
                m_CommandManager.Connect(MiOptions, new ShowOptionsCommand(this));
                m_CommandManager.Connect(MiRefresh, new RefreshCommand(this));
                m_CommandManager.Connect(MiScriptEditor, new ShowScriptEditorCommand(this));
                m_CommandManager.Connect(MiShowNoteList, new ShowNoteListCommand(this));
                m_CommandManager.Connect(MiWindowCascade, new WindowsCascadeCommand(this));
                m_CommandManager.Connect(MiWindowTileHorizontal, new WindowsTileHorizontalCommand(this));
                m_CommandManager.Connect(MiWindowTileVertical, new WindowsTileVerticalCommand(this));

                menuStrip1.MdiWindowListItem = MiWindow;

                foreach (object o in Enum.GetValues(typeof(NoteMatcherType)))
                {
                    CboMatcherType.Items.Add(o);
                }

                NoteMatcherType initialMatcherType;
                if (!Enum.TryParse(Properties.Settings.Default.MatcherType, out initialMatcherType))
                {
                    initialMatcherType = NoteMatcherType.ContainsAll;
                }
                CboMatcherType.SelectedItem = initialMatcherType;

                Model = new Model(this);
                Model.CurrentNoteChanged += ModelCurrentNoteChanged;
                Model.ExceptionOccurred += ModelOnExceptionOccurred;
                Model.NotesSelected += ModelNotesSelected;
                Model.UpdatesFound += ModelUpdatesFound;
                Model.StatusChanged += ModelStatusChanged;

                TvNotes.AllowDrop = true;

                CategoryModelType initialCategoryNodel;
                if (!Enum.TryParse(Properties.Settings.Default.TreeModelType, out initialCategoryNodel))
                {
                    initialCategoryNodel = CategoryModelType.TagsFlat;
                }

                foreach (CategoryModelType value in Enum.GetValues(typeof(CategoryModelType)))
                {
                    var wrapper = new EnumWrapper<CategoryModelType>(value);
                    CboTreeModel.Items.Add(wrapper);

                    if (value == initialCategoryNodel)
                    {
                        CboTreeModel.SelectedItem = wrapper;
                    }
                }

                NoteModelType initialNoteNodel;
                if (!Enum.TryParse(Properties.Settings.Default.NoteModelType, out initialNoteNodel))
                {
                    initialNoteNodel = NoteModelType.AlphabeticalFlatNotes;
                }

                foreach (NoteModelType value in Enum.GetValues(typeof(NoteModelType)))
                {
                    var wrapper = new EnumWrapper<NoteModelType>(value);
                    CboNoteModel.Items.Add(wrapper);

                    if (value == initialNoteNodel)
                    {
                        CboNoteModel.SelectedItem = wrapper;
                    }
                }

                CreateNoteWindow(true);
                TmrAutoSaver.Start();

                DisplayScripts();

                Settings.Default.Upgrade();
                Model.Open(Settings.ReadStoreTypeFromSettings(), Settings.Default.MainConnectionString);
                
                try
                {
                    var variables = new Dictionary<string, object>();
                    variables[ScriptVariableNames.CurrentNoteForm] = CurrentNoteForm;
                    variables[ScriptVariableNames.MainForm] = this;
                    Model.ScriptHost.ExecuteScript(ScriptType.MainFormLoad, variables);
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }

                m_NoteListModel.SetColumns(Settings.Default.NoteColumns.Split(new []{',', ';'}));

                SetWindowsTabControlVisible();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void ModelOnExceptionOccurred(object sender, ItemEventArgs<Exception> e)
        {
            HandleException(e.Item);
        }

        private void MainFormMdiChildActivate(object sender, EventArgs e)
        {
            CurrentNoteForm = (NoteForm)ActiveMdiChild;
        }

        private void MiAddLinkedNoteClick(object sender, EventArgs e)
        {

        }

        private void MiMakeRootClick(object sender, EventArgs e)
        {
            if (m_TempSelectedNoteNode != null)
            {
                Model.MakeRoot(m_TempSelectedNoteNode.Note);
            }
        }

        private void MiShowTabsClick(object sender, EventArgs e)
        {
            SetWindowsTabControlVisible();
        }

        private void MiToolsDropDownOpening(object sender, EventArgs e)
        {
            MiAddLinkedNote.Enabled = Model.CurrentNote != null;
        }

        private void MiViewHistoryClick(object sender, EventArgs e)
        {
            if (m_TempSelectedNoteNode != null)
            {
                using (NoteHistoryForm form = new NoteHistoryForm(Model, m_TempSelectedNoteNode.Note))
                {
                    form.ShowDialog(this);
                }
            }
        }

        private void MiWindowDropDownOpening(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                Form activeChild = ActiveMdiChild;

                ActivateMdiChild(null);
                ActivateMdiChild(activeChild);
            }
        }

        private void ModelCurrentNoteChanged(object sender, ItemEventArgs<Note> e)
        {
            ViewNote(e.Item);
        }

        private void ModelNotesSelected(object sender, ItemEventArgs<List<Note>> e)
        {
            m_NoteListModel.SetNotes(e.Item);
        }

        private void ModelStatusChanged(object sender, ItemEventArgs<string> e)
        {
            LblNoteCounts.Text = e.Item;
        }

        private void ModelUpdatesFound(object sender, ItemEventArgs<List<Tuple<ComponentInfo, UpdateContext>>> e)
        {
            using (var form = new UpdateForm())
            {
                form.SetUpdates(e.Item);
                form.ShowDialog();
            }
        }

        private void TcClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            NoteForm form;
            if (TcClients.SelectedTab != null && m_TabForms.TryGetValue(TcClients.SelectedTab, out form))
            {
                ActivateMdiChild(null);
                ActivateMdiChild(form);
                form.BringToFront();
            }
        }

        private void TmrAutoSaverTick(object sender, EventArgs e)
        {
            SetAutoSaveState(false);
            try
            {
                if (ExceptionSafeBlock(SaveAllOpenNotes))
                {
                    m_AutoSaveErrorCount = 0;
                }
                else
                {
                    m_AutoSaveErrorCount++;
                }
            }
            finally
            {
                if (m_AutoSaveErrorCount >= MaxAutoSaveErrors)
                {
                    MessageBox.Show("Disabled auto save due to repetitive save errors");
                }
                else
                {
                    SetAutoSaveState(true);
                }
            }
        }
        
        private void TvNotesMouseDown(object sender, MouseEventArgs e)
        {
            var node = TvNotes.GetNodeAt(e.Location) as NoteTreeNode;
            if (node != null)
            {
                m_TempSelectedNoteNode = node;
            }
        }

        private void TxtCategoryFilterTextChanged(object sender, EventArgs e)
        {
            Model.SetCategoryFilter(TxtCategoryFilter.Text);
        }


        public void CreateNoteWindow(bool maximized = false)
        {
            var newForm = new NoteForm(this, Model, m_CommandManager) { MdiParent = this };
            if (maximized)
            {
                newForm.WindowState = FormWindowState.Maximized;
            }
            newForm.Show();
            
            TabPage tab = new TabPage(newForm.Text);
            TcClients.TabPages.Add(tab);
            m_TabForms[tab] = newForm;
            m_FormTabs[newForm] = tab;
            newForm.FormClosed += (sender, args) =>
            {
                var localNoteForm = (NoteForm)sender;
                TabPage localTab;
                if (m_FormTabs.TryGetValue(localNoteForm, out localTab))
                {
                    TcClients.TabPages.Remove(localTab);
                    m_FormTabs.Remove(localNoteForm);
                    m_TabForms.Remove(localTab);
                    SetWindowsTabControlVisible();
                }
            };
            newForm.TextChanged += (sender, args) =>
                {
                    var localNoteForm = (NoteForm)sender;
                    TabPage localTab;
                    if (m_FormTabs.TryGetValue(localNoteForm, out localTab))
                    {
                        localTab.Text = localNoteForm.Text;
                    }
                };
            SetWindowsTabControlVisible();
            
            CurrentNoteForm = newForm;

            try
            {
                var variables = new Dictionary<string, object>();
                variables[ScriptVariableNames.CurrentNoteForm] = CurrentNoteForm;
                variables[ScriptVariableNames.MainForm] = this;
                Model.ScriptHost.ExecuteScript(ScriptType.NoteFormLoad, variables);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void DisplayScripts()
        {
            foreach (var script in Model.ScriptHost.GetAvailableScripts())
            {
                if (script.ScriptType == ScriptType.Custom)
                {
                    MiTools.DropDownItems.Add(new ScriptMenuItem(Model.ScriptHost, script));
                }
            }
        }

        private bool ExceptionSafeBlock(Action action)
        {
            bool completedSuccessfully;
            try
            {
                action();
                completedSuccessfully = true;
            }
            catch (Exception ex)
            {
                HandleException(ex);
                completedSuccessfully = false;
            }

            return completedSuccessfully;
        }

        private void HandleException(Exception ex)
        {
            using (ExceptionForm form = new ExceptionForm())
            {
                form.SetException(ex);
                form.ShowDialog(this);
            }
        }

        private void Search(bool isImmediate)
        {
            try
            {
                NoteQueryOptions options = GetSearchOptions();

                if (!isImmediate ^ Model.SupportsImmediateSearch(options.MatcherType))
                {
                    Model.Refresh(options);
                    errorProvider1.SetError(CboSearchText, string.Empty);
                }
                else
                {
                    Model.SelectFirstNote();
                }
            }
            catch (Exception ex)
            {
                errorProvider1.SetError(CboSearchText, ex.Message);
            }
        }

        private NoteQueryOptions GetSearchOptions()
        {
            NoteQueryOptions options = new NoteQueryOptions { HighlightMatches = MiHighlightMatches.Checked };

            var queryString = CboSearchText.Text;
            if (string.IsNullOrEmpty(queryString))
            {
                options.MatcherType = NoteMatcherType.AllMatch;
            }
            else
            {
                var matcherType = (NoteMatcherType)CboMatcherType.SelectedItem;
                options.BatchStrategy = BatchStrategy.AnyMustMatch;
                options.IsCaseSensitive = MiSearchCaseSensitive.Checked;
                options.MatcherType = matcherType;
                options.QueryText = queryString;
                options.Separator = m_DefaultNoteQueryOptions.Separator;
                if (MiSearchAllFields.Checked)
                {
                    options.SearchFields.Add(SearchField.All);
                }
                else
                {
                    options.SearchFields.Add(SearchField.Name);
                }

                if (MiSearchNoteText.Checked)
                {
                    options.SearchFields.Add(SearchField.Text);
                }
            }

            return options;
        }

        private void SaveAllOpenNotes()
        {
            foreach (Form form in MdiChildren)
            {
                var noteForm = form as NoteForm;
                if (noteForm != null)
                {
                    noteForm.SaveNote();
                }
            }
        }

        public void ViewNote(Note note)
        {
            CurrentNoteForm.CurrentNote = note;
        }

        private void TslblMemoryClick(object sender, EventArgs e)
        {
            byte[] tempData = new byte[DataSize.FromMegabytes(10)];
            Random r = new Random();
            r.NextBytes(tempData);
        }

        private void TslblAutoSaveEnabledClick(object sender, EventArgs e)
        {
            m_AutoSaveErrorCount = 0;
            ToggleAutoSave();
        }

        private void ToggleAutoSave()
        {
            SetAutoSaveState(!TmrAutoSaver.Enabled);
        }

        private void SetAutoSaveState(bool enable)
        {
            TmrAutoSaver.Enabled = enable;
            TslblAutoSaveEnabled.Text = "Auto Save: " + (TmrAutoSaver.Enabled ? "Enabled" : "Disabled");
        }

        private void MiImportFiles_Click(object sender, EventArgs e)
        {
            ImportFilesForm form = new ImportFilesForm(Model);
            form.Show(this);
        }

        private void MiCleanNotes_Click(object sender, EventArgs e)
        {
            CleanNotesForm form = new CleanNotesForm(Model);
            form.Show(this);
        }

        private void SetWindowsTabControlVisible()
        {
            TcClients.Visible = MiShowTabs.Checked && TcClients.TabPages.Count > 1;
        }

        private void MiLinkToCurrentNoteClick(object sender, EventArgs e)
        {
            ExceptionSafeBlock(() =>
                {
                    if (m_TempSelectedNoteNode != null)
                    {
                        CurrentNoteForm?.CurrentNote?.AddLinkedNote(m_TempSelectedNoteNode.Note);
                    }
                });
        }
    }
}
