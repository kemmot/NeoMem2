using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using NeoMem2.Core;

namespace NeoMem2.Gui.Models
{
    public class FlatTagsTreeModel : CategoryTreeModel
    {
        public FlatTagsTreeModel(TreeView categoriesView)
            : base(categoriesView)
        {
        }


        public bool IncludeUnsplitTags { get; set; }


        protected override IEnumerable<NoteCategory> GetRootCategories(IEnumerable<Note> notes)
        {
            var tags = new Dictionary<string, NoteCategory>();

            foreach (var note in notes)
            {
                var tagNames = new List<string>();
                if (note.Tags.Count == 0)
                {
                    tagNames.Add(NoneTag);
                }
                else
                {
                    tagNames.AddRange(note.Tags.Select(tag => tag.Tag.Name));
                }

                foreach (string noteTag in tagNames)
                {
                    NoteCategory category;
                    if (!tags.TryGetValue(noteTag, out category))
                    {
                        category = new NoteCategory { Name = noteTag };
                        tags[noteTag] = category;
                    }

                    category.Notes.Add(note);
                }
            }

            return tags.Values;
        }
    }
}
