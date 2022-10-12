using System.Collections.Generic;
using System.Windows.Forms;

using NeoMem2.Core;
using NeoMem2.Utils;

namespace NeoMem2.Gui.Models
{
    public class ClassNoteTreeModel : CategoryTreeModel
    {
        public ClassNoteTreeModel(TreeView view)
            : base(view)
        {
        }

        protected override IEnumerable<NoteCategory> GetRootCategories(IEnumerable<Note> notes)
        {
            var categories = new Dictionary<string, NoteCategory>();

            foreach (var note in notes)
            {
                string classToUse;

                if (string.IsNullOrEmpty(note.Class))
                {
                    if (note.Namespace == NoteNamespace.Note)
                    {
                        classToUse = NoneTag;
                    }
                    else
                    {
                        classToUse = "[" + note.Namespace.ToLower() + "]";
                    }
                }
                else
                {
                    classToUse = note.Class;
                }

                NoteCategory category;
                if (!categories.TryGetValue(classToUse, out category))
                {
                    category = new NoteCategory(classToUse);
                    categories[classToUse] = category;
                }

                category.Notes.Add(note);
            }

            return categories.Values;
        }

        public override void RemoveNote(Note note)
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

        protected void UpdateNoteCount()
        {
            OnDisplayedNoteCountChanged(new ItemEventArgs<int>(View.Nodes.Count));
        }
    }
}
