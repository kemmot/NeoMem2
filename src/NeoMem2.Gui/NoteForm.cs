using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using Fireball.CodeEditor.SyntaxFiles;

using NeoMem2.Core;
using NeoMem2.Core.Queries;
using NeoMem2.Core.Reporting;
using NeoMem2.Gui.Models;

using Fireball.Windows.Forms;

using Markdig;

using NeoMem2.Gui.Commands;
using NeoMem2.Gui.Properties;
using NeoMem2.Utils;

using NLog;

using Attachment = NeoMem2.Core.Attachment;
using Exception = System.Exception;

namespace NeoMem2.Gui
{
    public partial class NoteForm : Form
    {
        private const string CategoryCustom = "Custom";
        private const string CategoryGeneral = "General";
        private const string CategorySystem = "System";

        private const string FilterFieldFilename = "Filename";

        private const string NoteStateModified = "Modified";
        private const string NoteStateSaved = "Saved";
        private const string NoteStateUnmodified = "Unmodified";

        private const string IsExternalEditorText = "Is External Editor";
        private const string IsNotExternalEditorText = "Is Not External Editor";

        public event EventHandler<ItemEventArgs<Note>> CurrentNoteChanged;

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly CommandManager m_CommandManager;
        private Note m_CurrentNote;
        private Control m_FormattedTextControl;
        private readonly IEditorHelper m_Helper;
        private bool m_IsDirty;
        private DateTime m_ExternalEditorLastWriteTime;
        private readonly MainForm m_MainForm;
        private readonly Model m_Model;
        private readonly Navigator<Note> m_Navigator = new Navigator<Note>();
        private readonly NoteListModel m_NoteListModel;
        private bool m_RefreshingLinkedNoteFilters;
        private readonly RichTextBox m_RichTextBox = new RichTextBox();
        private readonly UrlNavigator m_UrlNavigator;
        private readonly WebBrowser m_WebBrowser = new WebBrowser();

        public NoteForm(MainForm mainForm, Model model, CommandManager commandManager)
            : this()
        {
            if (mainForm == null) throw new ArgumentNullException("mainForm");
            if (model == null) throw new ArgumentNullException("model");
            if (commandManager == null) throw new ArgumentNullException("commandManager");

            m_MainForm = mainForm;
            m_Model = model;
            m_CommandManager = commandManager;
            m_Navigator.CurrentChanged += NavigatorCurrentChanged;

            TxtName.TextChanged += (sender, e) => IsDirty = true;
            m_Helper = new CodeEditorHelper(CmnuEditor);
            m_Helper.TextChanged += (sender, e) => IsDirty = true;
            m_RichTextBox.TextChanged += (sender, e) => IsDirty = true;
            m_WebBrowser.DocumentText = "<HTML><BODY><H1>TEST</H1></BODY></HTML>";
            m_WebBrowser.Navigating += M_WebBrowser_Navigating;
            TsMarkdown.Visible = false;
            TsRichText.Visible = false;
            m_UrlNavigator = new UrlNavigator(m_Model);
        }

        private void M_WebBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            var result = m_UrlNavigator.IdentifyLink(e.Url);
            switch (result.NavigateType)
            {
                case UrlNavigateType.None:
                    break;
                case UrlNavigateType.Note:
                    CurrentNote = result.Note;
                    e.Cancel = true;
                    break;
                case UrlNavigateType.Search:
                    e.Cancel = true;
                    break;
                case UrlNavigateType.Web:
                    break;
            }

            Logger.Debug("Navigating, {0}", result);
        }

        public NoteForm()
        {
            InitializeComponent();

            m_NoteListModel = new NoteListModel(LvLinks);
            m_NoteListModel.SetColumns(Properties.Settings.Default.NoteColumns.Split(new[] { ',', ';' }));
            m_NoteListModel.ExceptionOccurred += NoteListModelOnExceptionOccurred;
            //m_NoteListModel.CurrentNoteChanged += m_NoteListModel_CurrentNoteChanged;

            foreach (TextFormat textFormat in Enum.GetValues(typeof(TextFormat)))
            {
                CboTextFormat.Items.Add(new EnumWrapper<TextFormat>(textFormat));
            }

            MoveLinksTab();
            MovePropertiesTab();
            LblExternalEditor.Text = IsNotExternalEditorText;
        }

        private void NoteListModelOnExceptionOccurred(object sender, ItemEventArgs<Exception> e)
        {
            HandleException(e.Item);
        }

        public Note CurrentNote
        {
            get
            {
                SaveNote();
                return m_CurrentNote;
            }
            set { m_Navigator.SetCurrent(value); }
        }

        private Control FormattedTextControl
        {
            get { return m_FormattedTextControl; }
            set
            {
                if (m_FormattedTextControl != null)
                {
                    TpText.Controls.Remove(m_FormattedTextControl);
                }

                m_FormattedTextControl = value;

                if (m_FormattedTextControl != null)
                {
                    TpText.Controls.Add(m_FormattedTextControl);
                    m_FormattedTextControl.Dock = DockStyle.Fill;
                    m_FormattedTextControl.BringToFront();
                }
            }
        }

        public IEditorHelper Helper { get { return m_Helper; } }
        public NoteListModel NoteListModel { get { return m_NoteListModel; } }

        private void SetCurrentNote(Note note)
        {
            if (m_CurrentNote != null)
            {
                SaveNote();
                m_CurrentNote.Attachments.CollectionChanged -= AttachmentsCollectionChanged;
                m_CurrentNote.LinkedNotes.CollectionChanged -= LinkedNotesCollectionChanged;
                m_CurrentNote.PropertyChanged -= CurrentNotePropertyChanged;
            }

            Note oldNote = m_CurrentNote;
            m_CurrentNote = note;
            try
            {
                Logger.Debug("Current note changed from {0} to {1}", oldNote, m_CurrentNote);

                bool edittingEnabled = m_CurrentNote != null;
                PgProperties.SelectedObject = null;
                PgProperties.Enabled = edittingEnabled;
                Helper.GetEditor().Enabled = edittingEnabled;

                if (m_CurrentNote != null)
                {
                    // ensure template is up-to-date
                    if (!string.IsNullOrEmpty(m_CurrentNote.Class))
                    {
                        Note template = m_Model.GetTemplate(m_CurrentNote.Class);
                        m_CurrentNote.ApplyTemplate(template, m_Model.ScriptHost, includeText: false);
                    }

                    TxtName.Text = m_CurrentNote.Name;
                    CboTextFormat.SelectedItem = m_CurrentNote.TextFormat;
                    DisplayName();
                    DisplayIsPinned();
                    DisplayIsTemplate();
                    DisplayProperties();
                    DisplayLinkedNotes();
                    m_CurrentNote.Attachments.CollectionChanged += AttachmentsCollectionChanged;
                    m_CurrentNote.LinkedNotes.CollectionChanged += LinkedNotesCollectionChanged;
                    m_CurrentNote.PropertyChanged += CurrentNotePropertyChanged;
                }
                else
                {
                    TxtName.Text = string.Empty;
                }

                SetFormattedTextControl();

                IsDirty = false;
                LblNoteState.Text = NoteStateUnmodified;
            }
            catch (Exception ex)
            {
                Logger.ErrorException("Failed setting note, setting current note to [null]", ex);

                m_CurrentNote = null;
                TxtName.Text = string.Empty;
                SetFormattedTextControl();
                IsDirty = false;
                LblNoteState.Text = NoteStateUnmodified;

                throw;
            }
        }

        private void SetFormattedTextControl()
        {
            SuspendLayout();
            try
            {
                Control editor;
                if (m_CurrentNote != null)
                {
                    TsMarkdown.Visible = false;
                    TsRichText.Visible = false;

                    if (m_CurrentNote.Namespace == NoteNamespace.File)
                    {
                        TxtName.Enabled = false;
                        editor = SetFormattedTextControlFile();
                    }
                    else
                    {
                        TxtName.Enabled = true;
                        editor = SetFormattedTextControlNonFile();
                    }
                }
                else
                {
                    editor = null;
                }

                FormattedTextControl = editor;
            }
            finally
            {
                ResumeLayout();
            }
        }

        private Control SetFormattedTextControlFile()
        {
            Control editor;
            Helper.SetReadOnly(true);

            string filename = m_CurrentNote.Text;
            string extension = Path.GetExtension(filename);
            switch (extension.ToUpper())
            {
                case ".RTF":
                    m_RichTextBox.LoadFile(filename, RichTextBoxStreamType.RichText);
                    editor = m_RichTextBox;
                    break;
                case ".CS":
                case ".CSPROJ":
                case ".HTML":
                case ".LOG":
                case ".MD":
                case ".PS1":
                case ".PSD1":
                case ".PSM1":
                case ".TXT":
                case ".XML":
                    Helper.SetEditorText(File.ReadAllText(filename), m_CurrentNote.TextFormat);
                    editor = Helper.GetEditor();
                    break;
                default:
                    Helper.SetEditorText("Binary file", m_CurrentNote.TextFormat);
                    editor = Helper.GetEditor();
                    break;
            }

            return editor;
        }

        private Control SetFormattedTextControlNonFile()
        {
            Control editor;
            switch (m_CurrentNote.TextFormat)
            {
                case TextFormat.Markdown:
                    TsMarkdown.Visible = true;
                    editor = m_WebBrowser;
                    m_Helper.SetEditorText(m_CurrentNote.Text, m_CurrentNote.TextFormat);
                    RefreshMarkdown();
                    RefreshExternalEditor();
                    break;
                case TextFormat.Rtf:
                    TsRichText.Visible = true;
                    editor = m_RichTextBox;
                    if (string.IsNullOrEmpty(m_CurrentNote.FormattedText))
                    {
                        m_RichTextBox.Text = m_CurrentNote.Text;
                    }
                    else
                    {
                        m_RichTextBox.Rtf = m_CurrentNote.FormattedText;
                    }
                    break;
                case TextFormat.Txt:
                    editor = m_Helper.GetEditor();
                    Helper.SetEditorText(m_CurrentNote.Text, m_CurrentNote.TextFormat);
                    RefreshExternalEditor();
                    break;
                default:
                    string errorMessage = string.Format(
                        "Note text format not supported: {0}",
                        m_CurrentNote.TextFormat);
                    throw new NotSupportedException(errorMessage);
            }

            return editor;
        }

        private void RefreshExternalEditor()
        {
            bool canUseExternalEditor = CanUseExternalEditor();
            RefreshExternalEditor(canUseExternalEditor);
            TimerExternalEditorCheck.Enabled = canUseExternalEditor;
        }

        private bool CanUseExternalEditor()
        {
            return LblExternalEditor.Text == IsExternalEditorText;
        }

        private void RefreshExternalEditor(bool useExternalEditor)
        {
            m_Helper.SetReadOnly(useExternalEditor);
            File.WriteAllText(Properties.Settings.Default.ExternalEditorTempFile, Helper.GetEditorText());
            m_ExternalEditorLastWriteTime = File.GetLastWriteTimeUtc(Properties.Settings.Default.ExternalEditorTempFile);
        }

        private void AttachmentsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            DisplayLinkedNotes();
        }

        private void LinkedNotesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            DisplayLinkedNotes();
        }

        private void DisplayProperties()
        {
            PropertyBag bag = new PropertyBag();
            bag.AddProperty(new NotePropertyDescriptor(new Property("Class", m_CurrentNote.Class, true), CategorySystem));
            bag.AddProperty(new NotePropertyDescriptor(new Property("CreatedDate", m_CurrentNote.CreatedDate, true), CategorySystem));
            bag.AddProperty(new NotePropertyDescriptor(new Property("Id", m_CurrentNote.Id, true), CategorySystem));
            //bag.AddProperty(new NotePropertyDescriptor(new Property("LastAccessedDate", m_CurrentNote.LastAccessedDate, true)));
            bag.AddProperty(new NotePropertyDescriptor(new Property("LastModifiedDate", m_CurrentNote.LastModifiedDate, true), CategorySystem));
            bag.AddProperty(new NotePropertyDescriptor(new Property("Namespace", m_CurrentNote.Namespace, true), CategorySystem));
            bag.AddProperty(new NotePropertyDescriptor(new Property("TextFormat", m_CurrentNote.TextFormat.ToString(), true), CategorySystem));

            if (m_CurrentNote.Namespace == NoteNamespace.File)
            {
                bag.AddProperty(new NotePropertyDescriptor(new Property("File Path", m_CurrentNote.Text, true), CategorySystem));
            }

            var tagsProperty = new Property("Tags", m_CurrentNote.TagText);
            tagsProperty.PropertyChanged += (sender, e) =>
            {
                var existingTags = m_Model.GetTags();

                string newTagText = ((Property)sender).Value;
                var newTagNames = new List<string>(newTagText.Split(new []{";"}, StringSplitOptions.RemoveEmptyEntries));

                // add new tags
                foreach (string newTagName in newTagNames)
                {
                    Tag existingTag = existingTags.FirstOrDefault(t => t.Name.ToUpper() == newTagName.ToUpper());
                    if (existingTag == null)
                    {
                        existingTag = new Tag(newTagName);
                    }

                    var existingNoteTag = m_CurrentNote.Tags.FirstOrDefault(t => t.Tag.Id == existingTag.Id);
                    if (existingNoteTag == null)
                    {
                        m_CurrentNote.Tags.Add(new NoteTag(m_CurrentNote, existingTag));
                    }
                }

                // remove old tags
                foreach (var noteTag in m_CurrentNote.Tags)
                {
                    if (!newTagNames.Contains(noteTag.Tag.Name.ToLower()))
                    {
                        noteTag.IsDeleted = true;
                    }
                }
            };

            bag.AddProperty(new NotePropertyDescriptor(tagsProperty, CategoryGeneral));
            foreach (Property property in m_CurrentNote.Properties)
            {
                bag.AddProperty(new NotePropertyDescriptor(property, CategoryCustom, m_Model.GetExistingPropertyValues(property.Name)));
            }
            PgProperties.SelectedObject = bag;
        }

        private void DisplayLinkedNotes()
        {
            DisplayLinkedNotes(true, true);
        }

        private void DisplayLinkedNotes(bool refreshFields, bool refreshValues)
        {
            string filterField;
            string filterValue;
            GetLinkedNoteFilters(out filterField, out filterValue);

            if (refreshFields)
            {
                DisplayLinkedNoteFilterFields(filterField);
            }

            if (refreshValues)
            { 
                DisplayLinkedNoteFilterValues(filterField);
            }

            var notes = new List<Note>();

            foreach (NoteLink noteLink in m_CurrentNote.LinkedNotes)
            {
                var other = m_CurrentNote == noteLink.Note1 ? noteLink.Note2 : noteLink.Note1;

                bool canAdd;
                if (string.IsNullOrEmpty(filterValue))
                {
                    canAdd = true;
                }
                else if ((filterField == SearchField.All || filterField == SearchField.Class) && other.Class.ToUpper().Contains(filterValue))
                {
                    canAdd = true;
                }
                else if ((filterField == SearchField.All || filterField == SearchField.Name) && other.Name.ToUpper().Contains(filterValue))
                {
                    canAdd = true;
                }
                else
                {
                    canAdd = false;

                    if (filterField == SearchField.All || filterField == SearchField.Tags)
                    {
                        foreach (var tag in other.Tags)
                        {
                            if (tag.Tag.Name.ToUpper().Contains(filterValue))
                            {
                                canAdd = true;
                                break;
                            }
                        }
                    }

                    if (!canAdd)
                    {
                        foreach (var property in other.Properties)
                        {
                            if ((filterField == SearchField.All || filterField == property.Name) && property.Value.ToUpper().Contains(filterValue))
                            {
                                canAdd = true;
                                break;
                            }
                        }
                    }
                }

                if (canAdd && !other.IsDeleted)
                {
                    notes.Add(other);
                }
            }

            foreach (Attachment attachment in m_CurrentNote.Attachments)
            {
                if (string.IsNullOrEmpty(filterValue) || ((filterField == SearchField.All || filterField == FilterFieldFilename) && attachment.Filename.ToUpper().Contains(filterValue)))
                {
                    var attachmentNote = new Note();
                    attachmentNote.Class = NoteNamespace.Attachment;
                    attachmentNote.Name = attachment.Filename;
                    attachmentNote.Namespace = NoteNamespace.Attachment;
                    notes.Add(attachmentNote);
                }
            }

            m_NoteListModel.SetNotes(notes);
        }

        private void GetLinkedNoteFilters(out string field, out string value)
        {
            field = CboLinkNoteFilterField.SelectedItem?.ToString() ?? string.Empty;
            value = CboLinkNoteFilterText.Text.ToUpper();
        }

        private void DisplayLinkedNoteFilterFields(string field)
        {
            m_RefreshingLinkedNoteFilters = true;
            CboLinkNoteFilterField.BeginUpdate();
            try
            {
                CboLinkNoteFilterField.Items.Clear();
                AddLinkNoteFilterField(SearchField.All);
                AddLinkNoteFilterField(SearchField.Class);
                AddLinkNoteFilterField(SearchField.Name);

                foreach (NoteLink noteLink in m_CurrentNote.LinkedNotes)
                {
                    var other = m_CurrentNote == noteLink.Note1 ? noteLink.Note2 : noteLink.Note1;

                    if (other.Tags.Count > 0)
                    {
                        AddLinkNoteFilterField(SearchField.Tags);
                    }

                    foreach (var property in other.Properties)
                    {
                        AddLinkNoteFilterField(property.Name);
                    }
                }

                if (m_CurrentNote.Attachments.Count > 0)
                {
                    AddLinkNoteFilterField(FilterFieldFilename);
                }

                CboLinkNoteFilterField.SelectedItem = CboLinkNoteFilterField.Items.Contains(field) ? field : SearchField.All;
            }
            finally
            {
                CboLinkNoteFilterField.EndUpdate();
                m_RefreshingLinkedNoteFilters = false;
            }
        }

        private void DisplayLinkedNoteFilterValues(string field)
        {
            CboLinkNoteFilterText.BeginUpdate();
            try
            {
                CboLinkNoteFilterText.Items.Clear();

                foreach (NoteLink noteLink in m_CurrentNote.LinkedNotes)
                {
                    var other = m_CurrentNote == noteLink.Note1 ? noteLink.Note2 : noteLink.Note1;

                    if (field == SearchField.All || field == SearchField.Class)
                    {
                        AddLinkNoteFilterText(other.Class);
                    }

                    if (field == SearchField.All || field == SearchField.Name)
                    {
                        AddLinkNoteFilterText(other.Name);
                    }

                    if (other.Tags.Count > 0)
                    {
                        if (field == SearchField.All || field == SearchField.Tags)
                        {
                            foreach (var tag in other.Tags)
                            {
                                AddLinkNoteFilterText(tag.Tag.Name);
                            }
                        }
                    }

                    foreach (var property in other.Properties)
                    {
                        if (field == SearchField.All || field == property.Name)
                        {
                            AddLinkNoteFilterText(property.Value);
                        }
                    }
                }

                if (field == SearchField.All || field == FilterFieldFilename)
                {
                    foreach (Attachment attachment in m_CurrentNote.Attachments)
                    {
                        AddLinkNoteFilterText(attachment.Filename);
                    }
                }
            }
            finally
            {
                CboLinkNoteFilterText.EndUpdate();
            }
        }

        private void AddLinkNoteFilterField(string text)
        {
            if (!string.IsNullOrEmpty(text) && !CboLinkNoteFilterField.Items.Contains(text))
            {
                CboLinkNoteFilterField.Items.Add(text);
            }
        }

        private void AddLinkNoteFilterText(string text)
        {
            if (!string.IsNullOrEmpty(text) && !CboLinkNoteFilterText.Items.Contains(text))
            {
                CboLinkNoteFilterText.Items.Add(text);
            }
        }

        private void CurrentNotePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case Note.PropertyNameIsPinned:
                    DisplayIsPinned();
                    break;
                case Note.PropertyNameNamespace:
                    DisplayIsTemplate();
                    break;
                case Note.PropertyNameName:
                    DisplayName();
                    break;
                case Note.PropertyNameText:
                    Helper.SetEditorText(m_CurrentNote.Text, m_CurrentNote.TextFormat);
                    if (m_CurrentNote.TextFormat == TextFormat.Markdown && m_FormattedTextControl == m_WebBrowser)
                    {
                        RefreshMarkdown();
                    }
                    break;
                case Note.PropertyNameTextFormat:
                    var existingTextFormat = ((EnumWrapper<TextFormat>)CboTextFormat.SelectedItem).EnumValue;
                    if (existingTextFormat != m_CurrentNote.TextFormat)
                    {
                        CboTextFormat.SelectedItem = m_CurrentNote.TextFormat;
                    }

                    SetFormattedTextControl();
                    break;
            }

            IsDirty = true;
        }

        private void DisplayName()
        {
            Text = m_CurrentNote.Name;
            TxtName.Text = m_CurrentNote.Name;
        }

        private void DisplayIsPinned()
        {
            LblPinned.Text = m_CurrentNote.IsPinned ? "Pinned" : "Unpinned";
        }

        private void DisplayIsTemplate()
        {
            LblIsTemplate.Text = m_CurrentNote.Namespace == NoteNamespace.NoteTemplate ? "Is Template" : "Not Template";
        }

        private void HandleException(Exception ex)
        {
            ExceptionForm form = new ExceptionForm();
            form.SetException(ex);
            form.ShowDialog(this);
        }

        public void HighlightQuery(NoteQueryOptions query)
        {
            RegexOptions options = RegexOptions.Compiled;
            if (!query.IsCaseSensitive)
            {
                options |= RegexOptions.IgnoreCase;
            }
            MatchCollection matches = Regex.Matches(Helper.GetEditorText(), query.QueryText, options);
            Helper.HighlightMatches(matches);
        }

        public bool IsDirty
        {
            get
            {
                if (CanUseExternalEditor() && IsExternalEditorFileDirty())
                {
                    IsDirty = true;
                }

                return m_IsDirty;
            }
            private set
            {
                if (m_IsDirty != value)
                {
                    m_IsDirty = value;
                    LblNoteState.Text = m_IsDirty ? NoteStateModified : NoteStateSaved;
                }
            }
        }

        public void NavigateBackwards()
        {
            m_Navigator.NavigateBackwards();
        }

        public void NavigateForwards()
        {
            m_Navigator.NavigateForwards();
        }

        protected virtual void OnCurrentNoteChanged(ItemEventArgs<Note> e)
        {
            if (CurrentNoteChanged != null) CurrentNoteChanged(this, e);
        }

        public void SaveNote()
        {
            if (m_CurrentNote != null)
            {
                if (IsDirty)
                {
                    m_CurrentNote.Name = TxtName.Text;

                    if (m_CurrentNote.Namespace == NoteNamespace.File)
                    {
                        SaveFileNote();
                    }
                    else
                    {
                        SaveNonFileNote();
                    }

                    m_Model.Save(m_CurrentNote);

                    IsDirty = false;
                }
            }
        }

        private void SaveFileNote()
        {
            string filename = m_CurrentNote.Text;
            string extension = Path.GetExtension(filename);
            switch (extension.ToUpper())
            {
                case ".RTF":
                    m_RichTextBox.SaveFile(filename);
                    break;
                default:
                    File.WriteAllText(filename, Helper.GetEditorText());
                    break;
            }
        }

        private void SaveNonFileNote()
        {
            m_CurrentNote.TextFormat = ((EnumWrapper<TextFormat>)CboTextFormat.SelectedItem).EnumValue;
            switch (m_CurrentNote.TextFormat)
            {
                case TextFormat.Markdown:
                case TextFormat.Txt:
                    if (CanUseExternalEditor())
                    {
                        SaveExternalEditor();
                        Helper.SetEditorText(m_CurrentNote.Text, TextFormat.Txt);
                    }
                    else
                    {
                        m_CurrentNote.Text = Helper.GetEditorText();
                    }
                    break;
                case TextFormat.Rtf:
                    m_CurrentNote.Text = m_RichTextBox.Text;
                    m_CurrentNote.FormattedText = m_RichTextBox.Rtf;
                    break;
                default:
                    string errorMessage = string.Format(
                        "Note text format not supported: {0}",
                        m_CurrentNote.TextFormat);
                    throw new NotSupportedException(errorMessage);
            }
        }

        private void SaveExternalEditor()
        {
            DateTime lastWriteTime = File.GetLastWriteTimeUtc(Properties.Settings.Default.ExternalEditorTempFile);
            m_CurrentNote.Text = File.ReadAllText(Properties.Settings.Default.ExternalEditorTempFile);
            m_ExternalEditorLastWriteTime = lastWriteTime;
        }

        private void LblExternalEditorClick(object sender, EventArgs e)
        {
            if (CanUseExternalEditor())
            {
                SaveExternalEditor();
            }
            else
            {
                RefreshExternalEditor(true);
            }

            LblExternalEditor.Text = LblExternalEditor.Text == IsExternalEditorText ? IsNotExternalEditorText : IsExternalEditorText;
        }

        private void LblPinnedClick(object sender, EventArgs e)
        {
            m_CurrentNote.IsPinned = !m_CurrentNote.IsPinned;
        }

        private void LblIsTemplateClick(object sender, EventArgs e)
        {
            m_CurrentNote.Namespace = m_CurrentNote.Namespace == NoteNamespace.NoteTemplate ? NoteNamespace.Note : NoteNamespace.NoteTemplate;
        }

        private void MiPastePlainClick(object sender, EventArgs e)
        {
            Helper.Paste();
        }

        private void ContextMenuStrip1Opening(object sender, CancelEventArgs e)
        {
            MiPastePlain.Enabled = Clipboard.ContainsText();
        }
        
        private void MiNotesNavigateForwardsClick(object sender, EventArgs e)
        {
            NavigateForwards();
        }

        private void MiNotesNavigateBackwardsClick(object sender, EventArgs e)
        {
            NavigateBackwards();
        }

        private void NavigatorCurrentChanged(object sender, EventArgs e)
        {
            MiNotesNavigateBackwards.Enabled = m_Navigator.BackwardsStackCount > 0;
            MiNotesNavigateForwards.Enabled = m_Navigator.ForwardsStackCount > 0;
            SetCurrentNote(m_Navigator.Current);
        }

        private void BtnNewPropertyClick(object sender, EventArgs e)
        {
            using (NewPropertyForm form = new NewPropertyForm())
            {
                form.Model = m_Model;
                form.SetPropertyTypes(m_Model.GetPropertyTypes());
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Property property = new Property(
                        form.PropertyName,
                        form.PropertyValue)
                    {
                        ClrDataType = form.PropertyClrDataType
                    };
                    CurrentNote.AddProperty(property);
                    DisplayProperties();
                }
            }
        }

        private void BtnDeletePropertyClick(object sender, EventArgs e)
        {
            try
            {
                string propertyName = PgProperties.SelectedGridItem.Label;
                string message = string.Format("Delete property: {0}", propertyName);
                var result = MessageBox.Show(message, "Delete Property", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    m_CurrentNote.RemoveProperty(propertyName);
                    DisplayProperties();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Failed to delete property", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TsbtnFormatClick(object sender, EventArgs e)
        {
            FontStyle style = FontStyle.Regular;
            if (TsbtnBold.Checked) style |= FontStyle.Bold;
            if (TsbtnItalic.Checked) style |= FontStyle.Italic;
            if (TsbtnUnderline.Checked) style |= FontStyle.Underline;
            //Helper.SetFont(style);
            m_RichTextBox.SelectionFont = new Font(m_RichTextBox.Font, style);
        }

        private void TsbtnRemoveFormattingClick(object sender, EventArgs e)
        {
            var newFont = new Font(m_RichTextBox.Font, FontStyle.Regular);
            m_RichTextBox.Font = newFont;
            m_RichTextBox.SelectionFont = newFont;
        }

        private void TsbtnApplyTemplateDropDownOpening(object sender, EventArgs e)
        {
            var templates = new List<Note>(m_Model.GetTemplates());
            templates.SortByName(false);

            TsbtnApplyTemplate.DropDownItems.Clear();
            if (templates.Count > 0)
            {
                Dictionary<string, List<Note>> templatesByInitial = new Dictionary<string, List<Note>>();
                foreach (Note template in templates)
                {
                    string initial = template.Name.Substring(0, 1).ToUpper();
                    List<Note> initialNotes;
                    if (!templatesByInitial.TryGetValue(initial, out initialNotes))
                    {
                        initialNotes = new List<Note>();
                        templatesByInitial[initial] = initialNotes;
                    }

                    initialNotes.Add(template);
                }

                foreach (string initial in templatesByInitial.Keys.OrderBy(i => i))
                {
                    List<Note> notes = templatesByInitial[initial];
                    if (notes.Count == 1)
                    {
                        Note template = notes[0];
                        var item = TsbtnApplyTemplate.DropDownItems.Add(template.Name);
                        item.Tag = template;
                    }
                    else
                    {
                        var initialItem = new ToolStripDropDownButton(initial);
                        TsbtnApplyTemplate.DropDownItems.Add(initialItem);
                        foreach (Note template in notes.OrderBy(n => n.Name))
                        {
                            var templateItem = initialItem.DropDownItems.Add(template.Name);
                            templateItem.Tag = template;
                        }
                        initialItem.DropDownItemClicked += TsbtnApplyTemplateDropDownItemClicked;
                    }
                }
            }
            else
            {
                TsbtnApplyTemplate.DropDownItems.Add("No templates available");
            }
        }

        private void TsbtnApplyTemplateDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Note template = e.ClickedItem.Tag as Note;
            if (template != null)
            {
                DialogResult result = MessageBox.Show(string.Format("Apply template: {0}?", template.Name), "Template", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    m_CurrentNote.ApplyTemplate(template, m_Model.ScriptHost, includeText: true);
                    DisplayProperties();
                }
            }
        }

        private void LvLinksDoubleClick(object sender, EventArgs e)
        {
            ExceptionSafeBlock(() =>
            {
                foreach (Note selectedNote in m_NoteListModel.GetSelectedNotes())
                {
                    if (selectedNote.Namespace == NoteNamespace.Attachment)
                    {
                        Process.Start(new ProcessStartInfo(selectedNote.Name)
                        {
                            UseShellExecute = true
                        });
                    }
                    else
                    {
                        OnCurrentNoteChanged(new ItemEventArgs<Note>(selectedNote));
                    }
                }
            });
        }

        private void LvLinksDragDrop(object sender, DragEventArgs e)
        {
            Note note = null;

            if (e.Data.GetDataPresent(typeof(NoteListViewItem)))
            {
                note = ((NoteListViewItem)e.Data.GetData(typeof(NoteListViewItem))).Note;
            }
            else if (e.Data.GetDataPresent(typeof(NoteTreeNode)))
            {
                note = ((NoteTreeNode)e.Data.GetData(typeof(NoteTreeNode))).Note;
            }
            else if (e.Data.GetDataPresent(typeof(Note)))
            {
                note = (Note)e.Data.GetData(typeof(Note));
            }
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                AddAttachments(files);
            }

            AddNoteLink(note);
        }

        private void LvLinksDragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(typeof(Note)) || e.Data.GetDataPresent(typeof(NoteTreeNode)) || e.Data.GetDataPresent(typeof(NoteListViewItem))
                || e.Data.GetDataPresent(DataFormats.FileDrop)
                ? DragDropEffects.Copy
                : DragDropEffects.None;
        }
        
        private void LvLinksKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                foreach (Note selectedNote in m_NoteListModel.GetSelectedNotes())
                {
                    if (selectedNote.Namespace == NoteNamespace.Attachment)
                    {
                        string message = string.Format(
                            "Delete attachment: {0}? (actual file won't be deleted)",
                            selectedNote.Name);
                        var result = MessageBox.Show(message, "Delete attachment", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            foreach (Attachment attachment in m_CurrentNote.Attachments)
                            {
                                if (attachment.Filename == selectedNote.Name)
                                {
                                    attachment.IsDeleted = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        string message = string.Format(
                            "Delete link between notes '{0}' and '{1}'?",
                            m_CurrentNote,
                            selectedNote);
                        var result = MessageBox.Show(message, "Delete note link", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            foreach (NoteLink link in m_CurrentNote.LinkedNotes)
                            {
                                if (link.Note1 == selectedNote || link.Note2 == selectedNote)
                                {
                                    link.IsDeleted = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void MiLinkedNoteReportClick(object sender, EventArgs e)
        {
            var report = new NoteLinkReport();
            string output = report.Generate(CurrentNote);

            var dialog = new ReportForm();
            dialog.RtxtReportOutput.Text = output;
            dialog.Show(this);
        }

        private void MiNotePropertiesReportClick(object sender, EventArgs e)
        {
            var report = new NotePropertyReport();
            string output = report.Generate(CurrentNote);

            var dialog = new ReportForm();
            dialog.RtxtReportOutput.Text = output;
            dialog.Show(this);
        }

        private void MiLinksBottomClick(object sender, EventArgs e)
        {
            MoveLinksTab(DockStyle.Bottom);
        }

        private void MiLinksCentreClick(object sender, EventArgs e)
        {
            MoveLinksTab(DockStyle.Fill);
        }

        private void MiLinksLeftClick(object sender, EventArgs e)
        {
            MoveLinksTab(DockStyle.Left);
        }

        private void MiLinksRightClick(object sender, EventArgs e)
        {
            MoveLinksTab(DockStyle.Right);
        }

        private void MiPropertiesBottomClick(object sender, EventArgs e)
        {
            MovePropertiesTab(DockStyle.Bottom);
        }

        private void MiPropertiesCentreClick(object sender, EventArgs e)
        {
            MovePropertiesTab(DockStyle.Fill);
        }

        private void MiPropertiesLeftClick(object sender, EventArgs e)
        {
            MovePropertiesTab(DockStyle.Left);
        }

        private void MiPropertiesRightClick(object sender, EventArgs e)
        {
            MovePropertiesTab(DockStyle.Right);
        }
        
        private void MoveLinksTab(DockStyle dockStyle)
        {
            Settings.Default.NoteFormDefaultLinksLocation = dockStyle;
            MoveLinksTab();
        }

        private void MoveLinksTab()
        {
            ToolStripMenuItem keepChecked;
            TabControl destination;
            switch (Settings.Default.NoteFormDefaultLinksLocation)
            {
                case DockStyle.Bottom:
                    destination = TcBottom;
                    keepChecked = MiLinksBottom;
                    break;
                case DockStyle.Fill:
                    destination = TcMain;
                    keepChecked = MiLinksCentre;
                    break;
                case DockStyle.Left:
                    destination = TcLeft;
                    keepChecked = MiLinksLeft;
                    break;
                case DockStyle.Right:
                    destination = TcRight;
                    keepChecked = MiLinksRight;
                    break;
                default:
                    throw new NotSupportedException("DockStyle not supported: " + Settings.Default.NoteFormDefaultLinksLocation);
            }

            UncheckMenuItems(keepChecked, MiLinksLocation.DropDownItems);
            MoveTab(TpLinks, destination);
        }

        private void MovePropertiesTab(DockStyle dockStyle)
        {
            Settings.Default.NoteFormDefaultPropertiesLocation = dockStyle;
            MovePropertiesTab();
        }

        private void MovePropertiesTab()
        {
            ToolStripMenuItem keepChecked;
            TabControl destination;
            switch (Settings.Default.NoteFormDefaultPropertiesLocation)
            {
                case DockStyle.Bottom:
                    destination = TcBottom;
                    keepChecked = MiPropertiesBottom;
                    break;
                case DockStyle.Fill:
                    destination = TcMain;
                    keepChecked = MiPropertiesCentre;
                    break;
                case DockStyle.Left:
                    destination = TcLeft;
                    keepChecked = MiPropertiesLeft;
                    break;
                case DockStyle.Right:
                    destination = TcRight;
                    keepChecked = MiPropertiesRight;
                    break;
                default:
                    throw new NotSupportedException("DockStyle not supported: " + Settings.Default.NoteFormDefaultPropertiesLocation);
            }

            UncheckMenuItems(keepChecked, MiPropertiesLocation.DropDownItems);
            MoveTab(TpProperties, destination);
        }

        private static void UncheckMenuItems(ToolStripMenuItem keepChecked, ToolStripItemCollection items)
        {
            foreach (var item in items)
            {
                var menuItem = item as ToolStripMenuItem;
                if (menuItem != null)
                {
                    if (menuItem == keepChecked) menuItem.Checked = true;
                    else menuItem.Checked = false;
                }
            }
        }

        private void MoveTab(TabPage page, TabControl destination)
        {
            var parent = page.Parent as TabControl;
            if (parent != null)
            {
                parent.TabPages.Remove(page);
                var parentSplitter = GetSplitter(parent);
                if (parentSplitter != null) parentSplitter.Visible = parent.TabPages.Count > 0;
                parent.Visible = parent.TabPages.Count > 0;
            }

            destination.TabPages.Add(page);
            var destinationSplitter = GetSplitter(destination);
            if (destinationSplitter != null) destinationSplitter.Visible = destination.TabPages.Count > 0;
            destination.Visible = destination.TabPages.Count > 0;
        }

        private Splitter GetSplitter(TabControl tabControl)
        {
            Splitter result;
            if (tabControl == TcBottom) result = SplitterBottom;
            else if (tabControl == TcLeft) result = SplitterLeft;
            else if (tabControl == TcRight) result = SplitterRight;
            else result = null;

            return result;
        }

        private void CboTextFormatSelectedIndexChanged(object sender, EventArgs e)
        {
            var newValue = ((EnumWrapper<TextFormat>)CboTextFormat.SelectedItem).EnumValue;
            if (newValue != m_CurrentNote.TextFormat)
            {
                m_CurrentNote.TextFormat = newValue;
            }
        }
        
        private void RefreshMarkdown()
        {
            var builder = new MarkdownPipelineBuilder();
            var extensions = builder.UseAdvancedExtensions();
            var pipeline = extensions.Build();
            try
            {
                m_WebBrowser.DocumentText = Markdown.ToHtml(m_CurrentNote.Text, pipeline);
            }
            catch (Exception ex)
            {
                m_WebBrowser.DocumentText = "Failed converting markdown" + Environment.NewLine + ex.ToString();
            }
        }

        private void TsbtnMarkdownSwitchClick(object sender, EventArgs e)
        {
            Switch();
        }

        public void Switch()
        {
            bool toEditor = FormattedTextControl == m_WebBrowser;
            try
            {
                if (toEditor)
                {
                    m_Helper.SetEditorText(m_CurrentNote.Text, m_CurrentNote.TextFormat);
                    FormattedTextControl = m_Helper.GetEditor();
                }
                else
                {
                    m_CurrentNote.Text = m_Helper.GetEditorText();
                    FormattedTextControl = m_WebBrowser;
                    RefreshMarkdown();
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorException(string.Format("Failed to switch to {0}", toEditor ? "editor" : "browser"), ex);
            }
        }

        private void TsbtnShowHtmlClick(object sender, EventArgs e)
        {
            using (var dialog = new InternalViewerForm())
            {
                dialog.DisplayText(m_WebBrowser.DocumentText, SyntaxLanguage.HTML);
                dialog.ShowDialog(this);
            }
        }

        private void TimerExternalEditorCheck_Tick(object sender, EventArgs e)
        {
            if (IsExternalEditorFileDirty())
            {
                IsDirty = true;
            }
        }

        private bool IsExternalEditorFileDirty()
        {
            DateTime lastWriteTime = File.GetLastWriteTimeUtc(Properties.Settings.Default.ExternalEditorTempFile);
            return lastWriteTime > m_ExternalEditorLastWriteTime;
        }

        private void MiUndo_Click(object sender, EventArgs e)
        {
            m_Helper.Undo();
        }

        private void CboLinkNoteFilterText_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_RefreshingLinkedNoteFilters)
            {
                DisplayLinkedNotes(false, false);
            }
        }

        private void CboLinkNoteFilterText_TextChanged(object sender, EventArgs e)
        {
            if (!m_RefreshingLinkedNoteFilters)
            {
                DisplayLinkedNotes(false, false);
            }
        }

        private void CboLinkNoteFilterField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_RefreshingLinkedNoteFilters)
            {
                DisplayLinkedNotes(false, true);
            }
        }

        private void MiOpenInNewWindow_Click(object sender, EventArgs e)
        {
            foreach (Note note in m_NoteListModel.GetSelectedNotes())
            {
                m_CommandManager.Execute(new NoteOpenInNewWindowCommand(m_MainForm, note), e);
            }
        }

        private void MiCopyNoteLink_Click(object sender, EventArgs e)
        {
            ExceptionSafeBlock(() =>
                {
                    if (CurrentNote != null)
                    {
                        var reference = new NoteReference { NoteId = CurrentNote.Id };
                        Clipboard.SetData(reference.GetType().FullName, reference);
                    }
                    else
                    {
                        MessageBox.Show("No note to copy");
                    }
                });
        }

        private void CmnuNoteLinks_Opening(object sender, CancelEventArgs e)
        {
            ExceptionSafeBlock(() =>
            {
                MiPasteNoteLink.Enabled = Clipboard.ContainsData(typeof(NoteReference).FullName) || Clipboard.ContainsFileDropList();
            });
        }

        private void ExceptionSafeBlock(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void MiPasteNoteLink_Click(object sender, EventArgs e)
        {
            ExceptionSafeBlock(() =>
            {
                if (Clipboard.ContainsData(typeof(NoteReference).FullName))
                {
                    var noteReference = Clipboard.GetData(typeof(NoteReference).FullName) as NoteReference;
                    if (noteReference != null)
                    {
                        Note note = m_Model.GetNoteById(noteReference.NoteId);
                        if (note != null)
                        {
                            AddNoteLink(note);
                        }
                    }
                }
                else if (Clipboard.ContainsFileDropList())
                {
                    foreach (string file in Clipboard.GetFileDropList())
                    {
                        AddAttachment(file);
                    }
                }
            });
        }

        private void AddAttachments(IEnumerable<string> files)
        {
            foreach (string file in files)
            {
                AddAttachment(file);
            }
        }

        private void AddAttachment(string file)
        {
            CurrentNote.Attachments.Add(new Attachment { Filename = file, Note = CurrentNote });
        }

        private void AddNoteLink(Note noteToLinkToCurrent)
        {
            if (noteToLinkToCurrent != null)
            {
                m_CurrentNote.AddLinkedNote(noteToLinkToCurrent);
            }
        }
    }

    public interface IEditorHelper
    {
        event EventHandler TextChanged;
        Control GetEditor();
        string GetEditorText();
        string GetFormattedText();
        string GetSelectedText();
        void HighlightMatches(MatchCollection matches);
        void InsertLineFormat(string format, params object[] args);
        void InsertLine(string text);
        void InsertTextFormat(string format, params object[] args);
        void InsertText(string text);
        void Paste();
        void PopSelection();
        void PushSelection(int newSelectionStart, int newSelectionLength);
        void PushSelection();
        void ReplaceSelectedText(string newText);
        void SetFont(FontStyle style);
        void SetReadOnly(bool value);
        void SetEditorText(string text, TextFormat format);

        void Undo();
    }

    public abstract class EditorHelperBase
    {
        public event EventHandler TextChanged;

        public void InsertLineFormat(string format, params object[] args) { InsertTextFormat(Environment.NewLine + format, args); }

        public abstract void InsertLine(string text);

        public void InsertTextFormat(string format, params object[] args) { InsertText(string.Format(format, args)); }

        public abstract void InsertText(string text);

        protected virtual void OnTextChanged(EventArgs e)
        {
            if (TextChanged != null) TextChanged(this, e);
        }
    }

    public class CodeEditorHelper : EditorHelperBase, IEditorHelper
    {
        private readonly CodeEditorControl m_Editor;

        public CodeEditorHelper(ContextMenuStrip contextMenu)
        {
            m_Editor = new CodeEditorControl();
            m_Editor.ContextMenuStrip = contextMenu;
            m_Editor.TextChanged += (sender, e) => OnTextChanged(e);
            SetLanguage(SyntaxLanguage.Text);
        }

        public Control GetEditor() { return m_Editor; }

        public string GetEditorText() { return m_Editor.Document.Text; }

        public string GetFormattedText() { return string.Empty; }

        public string GetSelectedText()
        {
            return m_Editor.Selection.Text;
        }

        public void HighlightMatches(MatchCollection matches)
        {
            m_Editor.Document.ClearBookmarks();
            foreach (Match match in matches)
            {                
                var point = m_Editor.Document.IntPosToPoint(match.Index);
                m_Editor.GotoLine(point.Y);
                m_Editor.ToggleBookmark();
            }
        }

        public override void InsertLine(string text) { InsertText(Environment.NewLine + text); }

        public override void InsertText(string text) { m_Editor.Document.InsertText(text, 0, m_Editor.Selection.Bounds.FirstRow, true); }

        public void Paste() { m_Editor.Paste(); }

        public void PopSelection() { }

        public void PushSelection(int newSelectionStart, int newSelectionLength) { }

        public void PushSelection() { }

        public void ReplaceSelectedText(string newText)
        {
            m_Editor.Selection.DeleteSelection();
            InsertText(newText);
        }

        public void SetFont(FontStyle style) { }

        public void SetReadOnly(bool value)
        {
            m_Editor.ReadOnly = value;
        }

        public void SetEditorText(string text, TextFormat format)
        {
            m_Editor.Document.Text = text;
            SetFormat(format);
        }

        private void SetFormat(TextFormat format)
        {
            SyntaxLanguage language;
            switch (format)
            {
                case TextFormat.SourceCodeCPP:
                    language = SyntaxLanguage.CPP;
                    break;
                case TextFormat.SourceCodeCSharp:
                    language = SyntaxLanguage.CSharp;
                    break;
                case TextFormat.SourceCodeCSS:
                    language = SyntaxLanguage.CSS;
                    break;
                case TextFormat.SourceCodeDOSBatch:
                    language = SyntaxLanguage.DOSBatch;
                    break;
                case TextFormat.SourceCodeHTML:
                    language = SyntaxLanguage.HTML;
                    break;
                case TextFormat.SourceCodeJava:
                    language = SyntaxLanguage.Java;
                    break;
                case TextFormat.SourceCodeJavaScript:
                    language = SyntaxLanguage.JavaScript;
                    break;
                case TextFormat.SourceCodePHP:
                    language = SyntaxLanguage.PHP;
                    break;
                case TextFormat.SourceCodePerl:
                    language = SyntaxLanguage.Perl;
                    break;
                case TextFormat.SourceCodePython:
                    language = SyntaxLanguage.Python;
                    break;
                case TextFormat.SourceCodeTSQL:
                    language = SyntaxLanguage.SqlServer2K5;
                    break;
                case TextFormat.SourceCodeXML:
                    language = SyntaxLanguage.XML;
                    break;
                case TextFormat.Txt:
                default:
                    language = SyntaxLanguage.CSharp;
                    break;
            }

            SetLanguage(language);
        }

        private void SetLanguage(SyntaxLanguage language)
        {
            CodeEditorSyntaxLoader.SetSyntax(m_Editor, language);
        }

        public void Undo()
        {
            m_Editor.Undo();
        }
    }

    public class RichTextBoxHelper : EditorHelperBase, IEditorHelper
    {
        private readonly Stack<Tuple<int, int>> m_SelectionStack = new Stack<Tuple<int, int>>();
        private readonly RichTextBox m_RichTextBox;

        public RichTextBoxHelper()
            : this(new RichTextBox())
        {
        }

        public RichTextBoxHelper(RichTextBox richTextBox)
        {
            m_RichTextBox = richTextBox;
            m_RichTextBox.TextChanged += (sender, e) => OnTextChanged(e);
        }

        public Control GetEditor()
        {
            return m_RichTextBox;
        }

        public string GetEditorText()
        {
            return m_RichTextBox.Text;
        }

        public string GetFormattedText()
        {
            return m_RichTextBox.Rtf;
        }

        public string GetSelectedText()
        {
            return m_RichTextBox.SelectedText;
        }

        public void HighlightMatches(MatchCollection matches)
        {
            m_RichTextBox.SuspendLayout();
            try
            {
                int selectionStart = m_RichTextBox.SelectionStart;
                int selectionLength = m_RichTextBox.SelectionLength;
                m_RichTextBox.SelectAll();
                m_RichTextBox.SelectionBackColor = Color.White;
                foreach (Match match in matches)
                {
                    m_RichTextBox.SelectionStart = match.Index;
                    m_RichTextBox.SelectionLength = match.Length;
                    m_RichTextBox.SelectionBackColor = Color.Yellow;
                }
                m_RichTextBox.SelectionStart = selectionStart;
                m_RichTextBox.SelectionLength = selectionLength;
            }
            finally
            {
                m_RichTextBox.ResumeLayout();
            }
        }

        public override void InsertLine(string text)
        {
            InsertText(text);
            m_RichTextBox.SelectedText += Environment.NewLine;
        }

        public override void InsertText(string text)
        {
            m_RichTextBox.SelectedText = text;
        }

        public void Paste()
        {
            m_RichTextBox.Paste(DataFormats.GetFormat(DataFormats.Text));
        }

        public void PopSelection()
        {
            if (m_SelectionStack.Count > 0)
            {
                var selection = m_SelectionStack.Pop();
                m_RichTextBox.SelectionStart = selection.Item1;
                m_RichTextBox.SelectionLength = selection.Item2;
            }
        }

        public void PushSelection(int newSelectionStart, int newSelectionLength)
        {
            PushSelection();
            m_RichTextBox.SelectionStart = newSelectionStart;
            m_RichTextBox.SelectionLength = newSelectionLength;
        }

        public void PushSelection()
        {
            m_SelectionStack.Push(new Tuple<int,int>(m_RichTextBox.SelectionStart, m_RichTextBox.SelectionLength));
        }

        public void ReplaceSelectedText(string newText)
        {
            m_RichTextBox.SelectedText = string.Empty;
            InsertText(newText);
        }

        public void SetFont(FontStyle style)
        {
            m_RichTextBox.SelectionFont = new Font(m_RichTextBox.Font, style);
        }

        public void SetReadOnly(bool value)
        {
            m_RichTextBox.ReadOnly = value;
        }
        
        public void SetEditorText(string text, TextFormat format)
        {
            m_RichTextBox.Text = text;
        }

        public void Undo()
        {
            m_RichTextBox.Undo();
        }
    }
}
