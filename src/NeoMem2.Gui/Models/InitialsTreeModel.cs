using System.Collections.Generic;
using System.Windows.Forms;

using NeoMem2.Core;

namespace NeoMem2.Gui.Models
{
    public class InitialsTreeModel : CategoryTreeModel
    {
        public InitialsTreeModel(TreeView categoriesView)
            : base(categoriesView)
        {
        }
        

        protected override IEnumerable<NoteCategory> GetRootCategories(IEnumerable<Note> notes)
        {
            var categories = new Dictionary<char, NoteCategory>();
            foreach (var note in notes)
            {
                char initial = GetInitial(note);

                NoteCategory tag;
                if (!categories.TryGetValue(initial, out tag))
                {
                    tag = new NoteCategory { Name = new string(new[] { initial }) };
                    categories[initial] = tag;
                }

                tag.Notes.Add(note);
            }

            return categories.Values;
        }

        private char GetInitial(Note note)
        {
            string name = note.Name;
            return string.IsNullOrEmpty(name) ? '-' : name.ToUpper()[0];
        }
    }
}
