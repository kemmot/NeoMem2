using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using NeoMem2.Core;

namespace NeoMem2.Gui.Models
{
    public class FolderTagsTreeModel : CategoryTreeModel
    {
        public FolderTagsTreeModel(TreeView categoriesView)
            : base(categoriesView)
        {
        }

        
        protected override IEnumerable<NoteCategory> GetRootCategories(IEnumerable<Note> notes)
        {
            var rootCategories = new List<NoteCategory>();
            var tagNodes = new Dictionary<string, NoteCategory>();

            foreach (Note note in notes)
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

                foreach (var tag in tagNames)
                {
                    var noteFolders = tag.Split(new[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
                    string path = string.Empty;
                    NoteCategory parent = null;
                    foreach (var noteFolder in noteFolders)
                    {
                        if (path.Length > 0) path += @"\";
                        path += noteFolder;

                        NoteCategory category;
                        if (!tagNodes.TryGetValue(path, out category))
                        {
                            category = new NoteCategory(noteFolder);
                            tagNodes[path] = category;

                            if (parent != null) parent.SubCategories.Add(category);
                            else rootCategories.Add(category);
                        }

                        parent = category;
                    }
                    
                    tagNodes[tag].Notes.Add(note);
                }
            }

            return rootCategories;
        }
    }
}
