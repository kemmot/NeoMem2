namespace NeoMem2.Gui.Models
{
    using NeoMem2.Core;
    using NeoMem2.Utils;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;

    public class StructuredNoteTreeModel : NoteTreeModelBase
    {
        public StructuredNoteTreeModel(TreeView view, string delimiter)
            : base(view)
        {
            Delimiter = delimiter;
        }

        public string Delimiter { get; private set; }

        public INoteView NoteView { get; private set; }

        public override void Refresh(INoteView view)
        {
            NoteView = view;

            var rootNodes = ConvertCategoriesToNodes(GetRootCategories(view.GetNotes()));
            base.AddNodes(rootNodes);
        }

        private IEnumerable<TreeNode> ConvertCategoriesToNodes(IEnumerable<NoteCategory> categories)
        {
            foreach (var category in categories)
            {
                NoteCategoryTreeNode node = new NoteCategoryTreeNode(category, recurse:true);
                yield return node;
            }
        }

        public override void RemoveNote(Note note)
        {
            void RemoveNoteFunction()
            {
                foreach (TreeNode node in View.Nodes)
                {
                    var noteNode = node as NoteTreeNode;
                    if (noteNode != null && noteNode.Note == note)
                    {
                        noteNode.Remove();
                        UpdateNoteCount();
                        break;
                    }
                }
            }

            if (View.InvokeRequired) View.Invoke((ThreadStart)RemoveNoteFunction);
            else RemoveNoteFunction();
        }

        private void UpdateNoteCount()
        {
            OnDisplayedNoteCountChanged(new ItemEventArgs<int>(View.Nodes.Count));
        }

        public override void SelectFirstNote()
        {
            List<Note> notes = NoteView.GetNotes().ToList();
            if (Properties.Settings.Default.SelectTopSearchMatch && notes.Count > 0)
            {
                OnCurrentNoteChanged(new ItemCompleteEventArgs<Note>(notes[0]));
            }
        }

        protected IEnumerable<NoteCategory> GetRootCategories(IEnumerable<Note> notes)
        {
            const string uncategorizedCategoryName = "[uncategorized]";

            var categories = new Dictionary<string, NoteCategory>();
            var rootCategories = new List<NoteCategory>();

            foreach (var note in notes)
            {
                if (note.Namespace == NoteNamespace.Note)
                {
                    NoteCategory previousCategory = null;
                    string[] nameParts = note.Name.Split(new string[] { Delimiter }, StringSplitOptions.RemoveEmptyEntries);
                    string notePath = string.Empty;
                    for (int namePartIndex = 0; namePartIndex < nameParts.Length; namePartIndex++)
                    {
                        if (namePartIndex < nameParts.Length - 1)
                        {
                            string namePart = nameParts[namePartIndex];
                            if (!string.IsNullOrEmpty(notePath)) notePath += Delimiter;
                            notePath += namePart;
                            NoteCategory category;
                            if (!categories.TryGetValue(notePath, out category))
                            {
                                category = new NoteCategory(namePart);
                                categories[notePath] = category;
                            }

                            if (previousCategory == null)
                            {
                                if (!rootCategories.Contains(category))
                                {
                                    rootCategories.Add(category);
                                }
                            }
                            else if (!previousCategory.SubCategories.Contains(category))
                            {
                                previousCategory.SubCategories.Add(category);
                            }

                            previousCategory = category;
                        }
                    }

                    if (previousCategory == null)
                    {
                        if (!categories.TryGetValue(uncategorizedCategoryName, out previousCategory))
                        {
                            previousCategory = new NoteCategory(uncategorizedCategoryName);
                            categories[previousCategory.Name] = previousCategory;
                            rootCategories.Add(previousCategory);
                        }
                    }

                    previousCategory.Notes.Add(note);
                }
            }

            foreach (var category in categories.Values)
            {
                for (int noteIndex = category.Notes.Count - 1; noteIndex >= 0; noteIndex--)
                {
                    Note note = category.Notes[noteIndex];
                    if (categories.TryGetValue(note.Name, out NoteCategory newCategory))
                    {
                        newCategory.Notes.Add(note);
                        category.Notes.Remove(note);
                    }
                }

                category.Notes.SortByName();
            }

            return rootCategories;
        }
    }
}
