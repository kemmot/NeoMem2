using System;
using System.Windows.Forms;

using Fireball.CodeEditor.SyntaxFiles;

using NeoMem2.Core;
using NeoMem2.Gui.Models;

namespace NeoMem2.Gui
{
    public partial class NoteHistoryForm : Form
    {
        private readonly Model m_Model;
        private readonly Note m_Note;

        public NoteHistoryForm(Model model, Note note)
            : this()
        {
            if (model == null) throw new ArgumentNullException("model");
            if (note == null) throw new ArgumentNullException("note");

            m_Model = model;
            m_Note = note;
        }

        public NoteHistoryForm()
        {
            InitializeComponent();
        }

        private void NoteHistoryFormLoad(object sender, EventArgs e)
        {
            Text = Text + " - " + m_Note.Name;

            foreach (NoteChange noteChange in m_Model.GetNoteHistory(m_Note))
            {
                LvChanges.Items.Add(new NoteChangeListViewItem(noteChange));
            }

            ColumnHeaderAutoResizeStyle resizeStyle = LvChanges.Items.Count > 0
                ? ColumnHeaderAutoResizeStyle.ColumnContent
                : ColumnHeaderAutoResizeStyle.HeaderSize;
            foreach (ColumnHeader header in LvChanges.Columns)
            {
                header.AutoResize(resizeStyle);
            }
        }


        public class NoteChangeListViewItem : ListViewItem
        {
            private readonly NoteChange m_NoteChange;

            public NoteChangeListViewItem(NoteChange noteChange)
            {
                if (noteChange == null) throw new ArgumentNullException("noteChange");

                m_NoteChange = noteChange;

                Text = m_NoteChange.ChangeDate.ToString("yyyy-MM-dd HH:mm:ss");
                SubItems.Add(m_NoteChange.ChangeType.ToString());
                SubItems.Add(m_NoteChange.FieldName);
                SubItems.Add(m_NoteChange.ValueString);
            }

            public NoteChange NoteChange { get { return m_NoteChange; } }
        }

        private void LvChangesDoubleClick(object sender, EventArgs e)
        {
            if (LvChanges.SelectedItems.Count > 0)
            {
                NoteChangeListViewItem item = LvChanges.SelectedItems[0] as NoteChangeListViewItem;
                if (item != null)
                {
                    using (InternalViewerForm form = new InternalViewerForm())
                    {
                        form.DisplayText(item.NoteChange.ValueString, SyntaxLanguage.Text);
                        form.ShowDialog(this);
                    }
                }
            }
        }
    }
}
