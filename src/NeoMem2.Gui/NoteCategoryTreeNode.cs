using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using NeoMem2.Core;

namespace NeoMem2.Gui
{
    public class NoteCategoryTreeNode : TreeNode
    {
        public readonly static Color AlternatingNodeColour = Color.LightYellow;

        public NoteCategoryTreeNode(NoteCategory tag, bool recurse = false)
        {
            NoteCategory = tag;
            SetText(addLoadingNote:!recurse);

            if (recurse)
            {
                int nodeCount = Nodes.Count;
                foreach (var subCategory in tag.SubCategories)
                {
                    var subCategoryNode = new NoteCategoryTreeNode(subCategory, true);
                    if (nodeCount % 2 == 1)
                    {
                        subCategoryNode.BackColor = AlternatingNodeColour;
                    }
                    this.Nodes.Add(subCategoryNode);
                    nodeCount++;
                }

                foreach (var note in tag.Notes)
                {
                    var noteNode = new NoteTreeNode(note, hasChildren:false);
                    if (nodeCount % 2 == 1)
                    {
                        noteNode.BackColor = AlternatingNodeColour;
                    }
                    this.Nodes.Add(noteNode);
                    nodeCount++;
                }
            }
        }

        public NoteCategory NoteCategory { get; private set; }

        public void Remove(Note note)
        {
            if (NoteCategory.Notes.Contains(note))
            {
                NoteCategory.Notes.Remove(note);
                SetText();
            }
        }

        public void SetText(bool addLoadingNote = true)
        {
            var text = new StringBuilder();
            text.Append(NoteCategory.Name);
            text.Append(" (");
            text.AppendFormat("{0} notes", NoteCategory.Notes.Count);
            if (NoteCategory.SubCategories.Count > 0)
            {
                text.AppendFormat(", {0} subcategories", NoteCategory.SubCategories.Count);
            }
            text.Append(")");

            Action guiUpdate = delegate
            {
                Text = text.ToString();

                if (addLoadingNote && Nodes.Count == 0 && NoteCategory.SubCategories.Count > 0)
                {
                    Nodes.Add("Loading...");
                }
            };

            if (TreeView != null)
            {
                TreeView.Invoke(guiUpdate);
            }
            else
            {
                guiUpdate();
            }
        }
    }
}
