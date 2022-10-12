using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using NeoMem2.Automation.Updates;
using NeoMem2.Core;
using NeoMem2.Core.Queries;
using NeoMem2.Core.Scripting;
using NeoMem2.Core.Stores;
using NeoMem2.Gui.Properties;
using NeoMem2.Utils;

namespace NeoMem2.Gui.Models
{
    public class Model
    {
        public event EventHandler<ItemEventArgs<Note>> CurrentNoteChanged;
        public event EventHandler<ItemEventArgs<Exception>> ExceptionOccurred;
        public event EventHandler<ItemEventArgs<List<Note>>> NotesSelected;
        public event EventHandler<ItemEventArgs<string>> StatusChanged;
        public event EventHandler<ItemEventArgs<List<Tuple<ComponentInfo, UpdateContext>>>> UpdatesFound;

        protected virtual void OnCurrentNoteChanged(ItemEventArgs<Note> e)
        {
            if (CurrentNoteChanged != null) CurrentNoteChanged(this, e);
        }

        protected virtual void OnExceptionOccurred(ItemEventArgs<Exception> e)
        {
            ExceptionOccurred?.Invoke(this, e);
        }

        protected virtual void OnNotesSelected(ItemEventArgs<List<Note>> e)
        {
            if (NotesSelected != null) NotesSelected(this, e);
        }

        protected virtual void OnUpdatesFound(ItemEventArgs<List<Tuple<ComponentInfo, UpdateContext>>> e)
        {
            if (UpdatesFound != null) UpdatesFound(this, e);
        }

        protected virtual void OnStatusChanged(ItemEventArgs<string> e)
        {
            if (StatusChanged != null) StatusChanged(this, e);
        }


        private readonly NoteView m_AllNotes = new NoteView();
        private readonly INoteView m_ActiveNotes;
        private ICategoryModel m_CategoryModel;
        private readonly TreeView m_CategoriesView;
        private Note m_CurrentNote;
        private NoteQueryOptions m_CurrentQueryOptions;
        private List<Note> m_FilteredNotes = new List<Note>();
        private readonly MainForm m_MainForm;
        private INoteViewModel m_NoteModel;
        private readonly FlatNoteTreeModel m_PinnedNoteModel;
        private readonly IScriptHost m_ScriptHost = new PowerShellScriptHost();
        private NoteView m_SelectedNotes;
        private INeoMemStore m_Source;


        public Model(MainForm mainForm)
        {
            m_MainForm = mainForm;

            m_ActiveNotes = new FilteredNoteView(m_AllNotes, note => !note.IsDeleted);

            m_NoteModel = new FlatNoteTreeModel(m_MainForm.TvNotes, Note.PropertyNameName);
            m_NoteModel.CurrentNoteChanged += TreeModelCurrentNoteChanged;
            m_NoteModel.DisplayedNoteCountChanged += NoteModelDisplayedNoteCountChanged;
            m_NoteModel.ExceptionOccurred += NoteModelOnExceptionOccurred;

            m_MainForm.TvPinnedNotes.Sorted = true;
            m_PinnedNoteModel = new FlatNoteTreeModel(m_MainForm.TvPinnedNotes, Note.PropertyNameName);
            m_PinnedNoteModel.CurrentNoteChanged += TreeModelCurrentNoteChanged;

            m_CategoriesView = m_MainForm.TvCategories;
            SetModel(CategoryModelType.TagsFlat);
        }

        private void NoteModelOnExceptionOccurred(object sender, ItemEventArgs<Exception> e)
        {
            OnExceptionOccurred(e);
        }


        public List<Note> CurrentNotes { get; private set; }

        public INeoMemStore Source { get { return m_Source; } }


        private void NoteModelDisplayedNoteCountChanged(object sender, ItemEventArgs<int> e)
        {
            SetStatus(e.Item);
        }

        private void CategoryModelNotesSelected(object sender, ItemEventArgs<List<Note>> e)
        {
            CurrentNotes = e.Item;
            m_SelectedNotes = new NoteView(e.Item);
            m_NoteModel.Refresh(m_SelectedNotes);
            OnStatusChanged(new ItemEventArgs<string>(string.Format("Displaying {0}/{1} notes", e.Item.Count, m_ActiveNotes.Count)));
            OnNotesSelected(e);
        }

        private void TreeModelCurrentNoteChanged(object sender, ItemEventArgs<Note> e)
        {
            CurrentNote = e.Item;
        }


        public Note CurrentNote
        {
            get { return m_CurrentNote; }
            set
            {
                if (m_CurrentNote != value)
                {
                    m_CurrentNote = value;
                    OnCurrentNoteChanged(new ItemEventArgs<Note>(m_CurrentNote));
                    HighlightCurrentQuery();
                }
            }
        }

        public IScriptHost ScriptHost { get { return m_ScriptHost; } }


        public void Backup(string backupFile)
        {
            m_Source.Backup(backupFile);
        }

        public void Delete(Note note)
        {
            note.DeletedDate = DateTime.Now;
            m_Source.Save(note);

            m_AllNotes.Remove(note);
            m_FilteredNotes.Remove(note);
            m_NoteModel.RemoveNote(note);
            m_CategoryModel.RemoveNote(note);
        }

        public List<string> GetExistingPropertyValues(string propertyName)
        {
            List<string> existingValues = new List<string>();
            foreach (Note note in m_ActiveNotes.GetNotes())
            {
                Property property;
                if (note.TryGetPropertyByName(propertyName, out property))
                {
                    if (!existingValues.Contains(property.Value))
                    {
                        existingValues.Add(property.Value);
                    }
                }
            }

            existingValues.Sort();

            return existingValues;
        }

        public IEnumerable<Note> GetActiveNotes()
        {
            return m_ActiveNotes.GetNotes();
        }

        public IEnumerable<Note> GetFiles()
        {
            return GetNotesByNamespace(NoteNamespace.File);
        }

        public Note GetTemplate(string name)
        {
            return GetNotesByNamespace(NoteNamespace.NoteTemplate).FirstOrDefault(n => n.Name == name);
        }

        public IEnumerable<Note> GetTemplates()
        {
            return GetNotesByNamespace(NoteNamespace.NoteTemplate);
        }

        public Note GetNoteById(long id)
        {
            return m_ActiveNotes.GetNotes().FirstOrDefault(note => note.Id == id);
        }

        public Note GetNoteByName(string name)
        {
            return m_ActiveNotes.GetNotes().FirstOrDefault(note => note.Name == name);
        }

        private IEnumerable<Note> GetNotesByNamespace(string namespaceName)
        {
            return m_ActiveNotes.GetNotes().Where(note => note.Namespace == namespaceName);
        }

        public List<Tag> GetTags()
        {
            return m_Source.GetTags();
        }

        public void SetModel(CategoryModelType treeModelType)
        {
            if (m_CategoryModel != null)
            {
                m_CategoryModel.NotesSelected -= CategoryModelNotesSelected;
                m_CategoryModel.Unregister();
            }

            m_CategoryModel = new CategoryModelFactory().Get(treeModelType, m_CategoriesView);
            m_CategoryModel.NotesSelected += CategoryModelNotesSelected;
            m_CategoryModel.Refresh(new NoteView(m_FilteredNotes));
        }

        public void SetModel(NoteModelType noteModelType)
        {
            if (m_NoteModel != null)
            {
                m_NoteModel.CurrentNoteChanged -= TreeModelCurrentNoteChanged;
                m_NoteModel.DisplayedNoteCountChanged -= NoteModelDisplayedNoteCountChanged;
            }

            m_NoteModel = new NoteModelFactory().Get(noteModelType, m_MainForm.TvNotes);
            m_NoteModel.CurrentNoteChanged += TreeModelCurrentNoteChanged;
            m_NoteModel.DisplayedNoteCountChanged += NoteModelDisplayedNoteCountChanged;
            m_NoteModel.ExceptionOccurred += NoteModelOnExceptionOccurred; 
            m_NoteModel.Refresh(m_SelectedNotes);
        }

        public void NewStore(string storeType, string connectionString)
        {
            try
            {
                var store = StoreFactory.Instance.GetStore(storeType, connectionString);
                store.CreateNewStore();
            }
            catch (Exception ex)
            {
                string message = string.Format(
                    "Failed to create new store '{0}' with connection string '{1}'",
                    storeType,
                    connectionString);
                throw new Exception(message, ex);
            }
        }

        public void Open(string storeType, string connectionString)
        {
            try
            {
                m_Source = StoreFactory.Instance.GetStore(storeType, connectionString);
                var updates = m_Source.GetUpdates();
                if (updates.Count > 0)
                {
                    OnUpdatesFound(new ItemEventArgs<List<Tuple<ComponentInfo, UpdateContext>>>(updates));
                }

                m_AllNotes.Clear();
                foreach (Note note in m_Source.GetNotes().GetNotes())
                {
                    m_AllNotes.Add(note);
                }

                Refresh();
            }
            catch (Exception ex)
            {
                string message = string.Format(
                    "Failed to open store '{0}' with connection string '{1}'",
                    storeType,
                    connectionString);
                throw new Exception(message ,ex);
            }
        }

        public Note CreateNewNote(bool attach = true)
        {
            var newNote = m_Source.CreateNewNote();
            if (attach)
            {
                m_AllNotes.Add(newNote);

                if (Settings.Default.RefreshOnNewNote)
                {
                    Refresh();
                }
            }

            return newNote;
        }

        public void AttachNotes(IEnumerable<Note> notes)
        {
            m_AllNotes.AddRange(notes);
            Refresh();
        }

        public void AttachNote(Note note)
        {
            m_AllNotes.Add(note);
            Refresh();
        }

        public void Save(Note note)
        {
            m_Source.Save(note);
        }

        public void Refresh(NoteQueryOptions options = null)
        {
            if (options == null)
            {
                options = new NoteQueryOptions
                {
                    IsCaseSensitive = false,
                    MatcherType = NoteMatcherType.AllMatch,
                    QueryText = string.Empty
                };
                m_CurrentQueryOptions = null;
            }
            else
            {
                m_CurrentQueryOptions = options;
            }

            m_PinnedNoteModel.Refresh(new FilteredNoteView(m_ActiveNotes, note => note.IsPinned));
            m_FilteredNotes = new NoteQuery().Search(m_ActiveNotes.GetNotes(), options);

            FillNodes();
            HighlightCurrentQuery();
        }

        private void HighlightCurrentQuery()
        {
            if (m_CurrentQueryOptions != null && m_CurrentQueryOptions.HighlightMatches && m_MainForm.CurrentNoteForm != null)
            {
                m_MainForm.CurrentNoteForm.HighlightQuery(m_CurrentQueryOptions);
            }
        }

        private void FillNodes()
        {
            m_CategoryModel.Refresh(new NoteView(m_FilteredNotes));
        }

        public void SetCategoryFilter(string filter)
        {
            m_CategoryModel.SetFilter(filter);
        }

        private void SetStatus(int displayedCount)
        {
            OnStatusChanged(new ItemEventArgs<string>(string.Format("Displaying {0}/{1} notes", displayedCount, m_ActiveNotes.Count)));
        }
        
        public Dictionary<string, string> GetPropertyTypes()
        {
            var propertyTypes = new Dictionary<string, string>();
            foreach (var note in m_ActiveNotes.GetNotes())
            {
                foreach (var property in note.Properties)
                {
                    if (!property.IsSystemProperty && !propertyTypes.ContainsKey(property.Name))
                    {
                        propertyTypes[property.Name] = property.ClrDataType;
                    }
                }
            }

            return propertyTypes;
        }

        public bool SupportsImmediateSearch(NoteMatcherType matcherType)
        {
            return matcherType != NoteMatcherType.PowerShell;
        }

        public List<NoteChange> GetNoteHistory(Note note)
        {
            return m_Source.GetNoteHistory(note);
        }

        public void MakeRoot(Note note)
        {
            m_FilteredNotes = new List<Note> { note };
            FillNodes();
        }

        public void SelectFirstNote()
        {
            m_NoteModel.SelectFirstNote();
        }
    }
}
