using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using NeoMem2.Core;
using NeoMem2.Gui.Core;
using NeoMem2.Utils;

namespace NeoMem2.Gui.Models
{
    public abstract class CategoryTreeModel : TreeModelBase, ICategoryModel, INoteViewModel
    {
        private const string AllTag = "[all]";
        private const string FilesTag = "[files]";
        protected const string NoneTag = "[none]";
        private const string NotesTag = "[notes]";
        private const string PinnedTag = "[pinned]";
        private const string TemplatesTag = "[templates]";


        public event EventHandler<ItemEventArgs<Note>> CurrentNoteChanged;
        public event EventHandler<ItemEventArgs<int>> DisplayedNoteCountChanged;
        public event EventHandler<ItemEventArgs<Exception>> ExceptionOccurred;
        public event EventHandler<ItemEventArgs<List<Note>>> NotesSelected;


        private readonly List<TreeNode> m_CategoryNodes = new List<TreeNode>();


        protected CategoryTreeModel(TreeView categoriesView)
            : base(categoriesView)
        {
        }


        public bool ExcludeDefaultCategories { get; set; }
        public virtual MultiSelectActionType MultiSelectAction { get { return MultiSelectActionType.Union; } }
        public virtual bool NoteReverseSort { get { return false; } }
        public virtual string NoteSortPropertyName { get { return Note.PropertyNameName; } }
        public bool ShowNotes { get; set; }
        public virtual bool SortCategories { get { return true; } }
        public TemplateVisibilityType TemplateVisibility { get; set; } = TemplateVisibilityType.WhenOnlyTemplates;


        public override void Refresh(INoteView noteView)
        {
            var notes = new List<Note>(noteView.GetNotes());
            AddCategories(GetCategories(notes));
            notes.Sort(new NoteComparer(NoteSortPropertyName, NoteReverseSort));
            OnNotesSelected(new ItemEventArgs<List<Note>>(notes));
        }

        public IEnumerable<NoteCategory> GetCategories(IEnumerable<Note> notes)
        {
            var notesToUse = notes.ToList();

            var categories = new List<NoteCategory>();
            if (!ExcludeDefaultCategories)
            {
                categories.AddRange(GetDefaultCategories(notesToUse));
            }

            IEnumerable<Note> notesToGetCategoriesFor;
            switch (TemplateVisibility)
            {
                case TemplateVisibilityType.Always:
                    notesToGetCategoriesFor = notesToUse;
                    break;
                case TemplateVisibilityType.Never:
                    notesToGetCategoriesFor = notesToUse.Where(n => n.Namespace != NoteNamespace.NoteTemplate);
                    break;
                case TemplateVisibilityType.WhenOnlyTemplates:
                    if (notesToUse.All(n => n.Namespace == NoteNamespace.NoteTemplate))
                    {
                        notesToGetCategoriesFor = notesToUse;
                    }
                    else
                    {
                        notesToGetCategoriesFor = notesToUse.Where(n => n.Namespace != NoteNamespace.NoteTemplate);
                    }
                    break;
                default:
                    throw new NotSupportedException("Template visibility type not supported: " + TemplateVisibility);
            }
            
            categories.AddRange(GetRootCategories(notesToGetCategoriesFor));
            return categories;
        }

        protected virtual IEnumerable<NoteCategory> GetDefaultCategories(IEnumerable<Note> notes)
        {
            var allNotesCategory = new NoteCategory(AllTag);
            var filesCategory = new NoteCategory(FilesTag);
            var notesCategory = new NoteCategory(NotesTag);
            var pinnedNotesCategory = new NoteCategory(PinnedTag);
            var templatesCategory = new NoteCategory(TemplatesTag);

            foreach (Note note in notes)
            {
                allNotesCategory.Notes.Add(note);
                if (note.IsPinned) pinnedNotesCategory.Notes.Add(note);

                switch (note.Namespace)
                {
                    case NoteNamespace.File:
                        filesCategory.Notes.Add(note);
                        break;
                    case NoteNamespace.Note:
                        notesCategory.Notes.Add(note);
                        break;
                    case NoteNamespace.NoteTemplate:
                        templatesCategory.Notes.Add(note);
                        break;
                }
            }

            return new List<NoteCategory>
                       {
                allNotesCategory,
                filesCategory,
                notesCategory,
                pinnedNotesCategory,
                templatesCategory
                       };
        }

        protected virtual IEnumerable<NoteCategory> GetRootCategories(IEnumerable<Note> notes)
        {
            return new List<NoteCategory>();
        }

        public virtual void RemoveNote(Note note)
        {
            foreach (var node in View.Nodes)
            {
                var tagNode = node as NoteCategoryTreeNode;
                if (tagNode != null)
                {
                    tagNode.Remove(note);
                }
            }
        }

        public void SetFilter(string filter)
        {
            Filter = filter ?? string.Empty;
            AddNodes(m_CategoryNodes);
        }

        protected virtual void OnExceptionOccurred(ItemEventArgs<Exception> e)
        {
            if (ExceptionOccurred != null) ExceptionOccurred(this, e);
        }

        protected virtual void OnNotesSelected(ItemEventArgs<List<Note>> e)
        {
            if (NotesSelected != null) NotesSelected(this, e);
        }

        protected override void AfterCheck(object sender, TreeViewEventArgs e)
        {
            base.AfterCheck(sender, e);
            RefreshNotes();
        }

        protected override void AfterSelect(object sender, TreeViewEventArgs e)
        {
            base.AfterSelect(sender, e);

            var node = e.Node as NoteTreeNode;
            if (node != null)
            {
                OnCurrentNoteChanged(new ItemEventArgs<Note>(node.Note));
            }

            RefreshNotes();
        }

        private void AddCategories(IEnumerable<NoteCategory> categories, TreeNode root = null)
        {
            IEnumerable<NoteCategory> sortedCategories = SortCategories ? (from c in categories orderby c.Name select c) : categories;

            int categoryCount = sortedCategories.Count();

            m_CategoryNodes.Clear();
            foreach (NoteCategory category in sortedCategories)
            {
                var node = new NoteCategoryTreeNode(category);
                m_CategoryNodes.Add(node);

                if (node.Nodes.Count == 0 && ShowNotes && category.Notes.Count > 0)
                {
                    node.Nodes.Add("Loading...");
                }

                if (category.Notes.Count < Properties.Settings.Default.MaxChildNodesForAutoExpand
                    || categoryCount == 1)
                {
                    node.Expand();
                }
            }

            AddNodes(m_CategoryNodes);
        }

        private void RefreshNotes()
        {
            var notes = new List<Note>();
            var checkedNodes = GetCheckNodes(View.Nodes);
            bool categoriesChecked = checkedNodes.Count > 0;
            
            if (categoriesChecked)
            {
                var sets = checkedNodes.OfType<NoteCategoryTreeNode>().Select(categoryNode => categoryNode.NoteCategory.Notes).ToList();

                IEnumerable<Note> temp = null;
                switch (MultiSelectAction)
                {
                    case MultiSelectActionType.Intersect:
                        temp = sets.Aggregate(temp, (current, set) => current == null ? set : current.Intersect(set));
                        break;
                    case MultiSelectActionType.Union:
                        temp = sets.Aggregate<List<Note>, IEnumerable<Note>>(notes, (current, set) => current.Union(set));
                        break;
                    default:
                        throw new NotSupportedException("Unsupported MultiSelectActionType: " + MultiSelectAction);
                }

                notes = new List<Note>(temp);
            }
            else
            {
                var categoryNode = View.SelectedNode as NoteCategoryTreeNode;
                if (categoryNode != null)
                {
                    foreach (var note in categoryNode.NoteCategory.Notes)
                    {
                        if (!notes.Contains(note))
                        {
                            notes.Add(note);
                        }
                    }
                }
            }

            notes.Sort(new NoteComparer(NoteSortPropertyName, NoteReverseSort));

            OnNotesSelected(new ItemEventArgs<List<Note>>(notes));
        }

        private List<TreeNode> GetCheckNodes(TreeNodeCollection nodes)
        {
            var checkedNodes = new List<TreeNode>();
            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                {
                    checkedNodes.Add(node);
                }

                checkedNodes.AddRange(GetCheckNodes(node.Nodes));
            }

            return checkedNodes;
        }

        protected virtual void OnCurrentNoteChanged(ItemEventArgs<Note> e)
        {
            if (CurrentNoteChanged != null) CurrentNoteChanged(this, e);
        }

        protected virtual void OnDisplayedNoteCountChanged(ItemEventArgs<int> e)
        {
            if (DisplayedNoteCountChanged != null) DisplayedNoteCountChanged(this, e);
        }

        protected override void AfterExpand(object sender, TreeViewEventArgs e)
        {
            base.AfterExpand(sender, e);

            var categoryNode = e.Node as NoteCategoryTreeNode;
            if (categoryNode != null)
            {
                View.BeginUpdate();
                try
                {
                    categoryNode.Nodes.Clear();
                    foreach (var category in categoryNode.NoteCategory.SubCategories)
                    {
                        categoryNode.Nodes.Add(new NoteCategoryTreeNode(category));
                    }

                    if (ShowNotes)
                    {
                        foreach (var note in categoryNode.NoteCategory.Notes)
                        {
                            categoryNode.Nodes.Add(new NoteTreeNode(note, note.LinkedNotes.Count > 0));
                        }
                    }
                }
                finally
                {
                    View.EndUpdate();
                }
            }
            else if (ShowNotes && e.Node is NoteTreeNode)
            {
                var noteNode = (NoteTreeNode)e.Node;
                ExpandLinkedNotes(noteNode);
            }
        }

        private void ExpandLinkedNotes(NoteTreeNode node)
        {
            var childNodes = new List<TreeNode>();

            List<Note> parentNotes = GetParentNotes(node);

            List<Note> childNotes = new List<Note>();
            foreach (var link in node.Note.LinkedNotes)
            {
                var linkedNote = node.Note == link.Note1 ? link.Note2 : link.Note1;
                if (!parentNotes.Contains(linkedNote))
                {
                    childNotes.Add(linkedNote);
                }
            }

            if (Properties.Settings.Default.ClassNoteTreeModelGroupChildren)
            {
                childNodes.AddRange(CollateNodesByClass(childNotes));
            }
            else
            {
                foreach (Note childNote in childNotes)
                {
                    childNodes.Add(new NoteTreeNode(childNote, childNote.LinkedNotes.Count > 0));
                }
            }

            node.Nodes.Clear();
            foreach (var childNode in childNodes)
            {
                node.Nodes.Add(childNode);
            }
            
        }

        private List<Note> GetParentNotes(TreeNode node)
        {
            var parentNotes = new List<Note>();
            var parentNode = node.Parent;
            while (parentNode != null)
            {
                var parentNoteNode = parentNode as NoteTreeNode;
                if (parentNoteNode != null)
                {
                    parentNotes.Add(parentNoteNode.Note);
                    parentNode = parentNode.Parent;
                }
                else
                {
                    parentNode = null;
                }
            }

            return parentNotes;
        }

        private IEnumerable<NoteCategoryTreeNode> CollateNodesByClass(IEnumerable<Note> notes)
        {
            var classNodes = new List<NoteCategoryTreeNode>();

            var notesByClass = CollateNotesByClass(notes);
            foreach (string className in notesByClass.Keys)
            {
                var classNotes = notesByClass[className];
                classNotes.Sort(new NoteComparer());

                var category = new NoteCategory(className, classNotes);
                var classNode = new NoteCategoryTreeNode(category);
                if (ShowNotes)
                {
                    classNode.Nodes.Add("Loading...");
                }

                classNodes.Add(classNode);
            }

            if (Properties.Settings.Default.ClassNoteTreeModelSorted)
            {
                classNodes.Sort((left, right) => left.Text.CompareTo(right.Text));
            }

            return classNodes;
        }

        private Dictionary<string, List<Note>> CollateNotesByClass(IEnumerable<Note> notes)
        {
            var notesByClass = new Dictionary<string, List<Note>>();
            foreach (Note note in notes)
            {
                if (note.Namespace != NoteNamespace.NoteTemplate)
                {
                    string classToUse = string.IsNullOrEmpty(note.Class) ? "[none]" : note.Class;

                    List<Note> classNotes;
                    if (!notesByClass.TryGetValue(classToUse, out classNotes))
                    {
                        classNotes = new List<Note>();
                        notesByClass[classToUse] = classNotes;
                    }

                    classNotes.Add(note);
                }
            }

            return notesByClass;
        }

        public void SelectFirstNote()
        {
            if (Properties.Settings.Default.SelectTopSearchMatch && m_CategoryNodes.Count > 0)
            {
                NoteCategory category = ((NoteCategoryTreeNode)m_CategoryNodes[0]).NoteCategory;
                if (category.Notes.Count > 0)
                {
                    Note topNote = category.Notes[0];
                    OnCurrentNoteChanged(new ItemCompleteEventArgs<Note>(topNote));
                }
            }
        }
    }
}
