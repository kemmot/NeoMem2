using NeoMem2.Core;
using NeoMem2.Utils;

using NLog;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using NeoMem2.Gui.Commands;

namespace NeoMem2.Gui.Models
{
    public class NoteListModel
    {
        private const string PropertyNameAllCustom = "[all custom]";

        public event EventHandler<ItemEventArgs<Note>> CurrentNoteChanged;
        public event EventHandler<ItemEventArgs<Exception>> ExceptionOccurred;

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly MultiCommandHandler m_DoubleClickHandler = new MultiCommandHandler();
        private readonly List<string> m_NotesListColumns = new List<string>();
        private readonly NoteComparer m_NotesSorter = new NoteComparer();
        private readonly List<string> m_PropertyNames = new List<string>();
        private readonly List<Note> m_SelectedNotes = new List<Note>();
        private readonly ListView m_View;

        public NoteListModel(ListView view)
        {
            if (view == null) throw new ArgumentNullException("view");

            m_View = view;
            m_View.VirtualMode = true;
            m_View.ColumnClick += LvNotesColumnClick;
            m_View.DoubleClick += LvNotesDoubleClick;
            m_View.ItemDrag += LvNotesOnItemDrag;
            m_View.RetrieveVirtualItem += LvNotesRetrieveVirtualItem;
            m_View.SelectedIndexChanged += LvNotesSelectedIndexChanged;

            m_DoubleClickHandler.ExceptionOccurred += CommandOnExceptionOccurred;
            RegisterDoubleClickHandler(new OpenFileNoteCommand());
        }

        public void RegisterDoubleClickHandler(INotesCommand command)
        {
            m_DoubleClickHandler.RegisterCommand(command);
        }

        private void CommandOnExceptionOccurred(object sender, ItemEventArgs<Exception> e)
        {
            OnExceptionOccurred(e);
        }

        protected virtual void OnExceptionOccurred(ItemEventArgs<Exception> e)
        {
            ExceptionOccurred?.Invoke(this, e);
        }

        public ListView View { get { return m_View; } }

        private void LvNotesColumnClick(object sender, ColumnClickEventArgs e)
        {
            string columnName = View.Columns[e.Column].Text;
            m_NotesSorter.SetSortProperty(columnName);
            m_SelectedNotes.Sort(m_NotesSorter);
            View.VirtualListSize = 0;
            View.VirtualListSize = m_SelectedNotes.Count;
        }

        private void LvNotesDoubleClick(object sender, EventArgs e)
        {
            m_DoubleClickHandler.Execute(GetSelectedNotes());
        }

        private void LvNotesOnItemDrag(object sender, ItemDragEventArgs e)
        {
            View.DoDragDrop(e.Item, DragDropEffects.Copy | DragDropEffects.Link | DragDropEffects.Move);
        }

        private void LvNotesRetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            Note note = m_SelectedNotes[e.ItemIndex];

            var item = new NoteListViewItem(note);

            int columnIndex = 0;
            foreach (string columnName in m_NotesListColumns)
            {
                if (columnName == PropertyNameAllCustom)
                {
                    foreach (string propertyName in m_PropertyNames)
                    {
                        Property property;
                        string columnValue = note.TryGetPropertyByName(propertyName, out property) ? property.Value : string.Empty;

                        if (columnIndex == 0)
                        {
                            item.Text = columnValue;
                        }
                        else
                        {
                            item.SubItems.Add(columnValue);
                        }
                        columnIndex++;
                    }
                }
                else
                {
                    string columnValue;
                    switch (columnName)
                    {
                        case Note.PropertyNameClass:
                            columnValue = note.Class;
                            break;
                        case Note.PropertyNameName:
                            columnValue = note.Name;
                            break;
                        case Note.PropertyNameScore:
                            columnValue = note.Score.ToString();
                            break;
                        case Note.PropertyNameTags:
                            columnValue = note.TagText;
                            break;
                        case Note.PropertyNameLastAccessedDate:
                            columnValue = note.LastAccessedDate.ToString("yyyy-MM-dd HH:mm:ss");
                            break;
                        case Note.PropertyNameLastModifiedDate:
                            columnValue = note.LastModifiedDate.ToString("yyyy-MM-dd HH:mm:ss");
                            break;
                        case Note.PropertyNameCreatedDate:
                            columnValue = note.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss");
                            break;
                        case Note.PropertyNameTextFormat:
                            columnValue = note.TextFormat.ToString();
                            break;
                        default:
                            Property property;
                            columnValue = note.TryGetPropertyByName(columnName, out property) ? property.Value : string.Empty;
                            break;
                    }

                    if (columnIndex == 0)
                    {
                        item.Text = columnValue;
                    }
                    else
                    {
                        item.SubItems.Add(columnValue);
                    }
                    columnIndex++;
                }

            }

            e.Item = item;
        }

        private void LvNotesSelectedIndexChanged(object sender, EventArgs e)
        {
            if (View.SelectedIndices.Count > 0)
            {
                OnCurrentNoteChanged(new ItemEventArgs<Note>(m_SelectedNotes[View.SelectedIndices[0]]));
            }
        }

        protected virtual void OnCurrentNoteChanged(ItemEventArgs<Note> e)
        {
            if (CurrentNoteChanged != null) CurrentNoteChanged(this, e);
        }

        public List<Note> GetSelectedNotes()
        {
            var selectedNotes = new List<Note>();
            foreach (int selectedIndex in View.SelectedIndices)
            {
                selectedNotes.Add(m_SelectedNotes[selectedIndex]);
            }

            return selectedNotes;
        }

        public void SetColumns(IEnumerable<string> columnNames)
        {
            m_NotesListColumns.Clear();
            m_NotesListColumns.AddRange(columnNames);
            SetNotes(m_SelectedNotes);
        }

        public void SetNotes(List<Note> notes)
        {
            Stopwatch columnAddStopwatch = new Stopwatch();
            Stopwatch columnClearStopwatch = new Stopwatch();
            Stopwatch itemAddStopwatch = new Stopwatch();
            Stopwatch itemClearStopwatch = new Stopwatch();
            Stopwatch propertyFindStopwatch = new Stopwatch();
            Stopwatch stopwatch = Stopwatch.StartNew();

            m_PropertyNames.Clear();

            propertyFindStopwatch.Start();
            foreach (var note in notes)
            {
                foreach (Property property in note.Properties)
                {
                    if (!m_PropertyNames.Contains(property.Name))
                    {
                        m_PropertyNames.Add(property.Name);
                    }
                }
            }
            propertyFindStopwatch.Stop();

            m_SelectedNotes.Clear();
            m_SelectedNotes.AddRange(notes);

            View.BeginUpdate();
            try
            {
                itemClearStopwatch.Start();
                View.VirtualListSize = 0;
                itemClearStopwatch.Stop();

                if (m_SelectedNotes.Count > 0)
                {
                    columnClearStopwatch.Start();
                    View.Columns.Clear();
                    columnClearStopwatch.Stop();

                    columnAddStopwatch.Start();
                    foreach (string columnName in m_NotesListColumns)
                    {
                        if (columnName == PropertyNameAllCustom)
                        {
                            foreach (string customColumnName in m_PropertyNames)
                            {
                                View.Columns.Add(customColumnName);
                            }
                        }
                        else
                        {
                            View.Columns.Add(columnName);
                        }

                        //LvNotes.Columns.Add(Note.PropertyNameName);
                        //LvNotes.Columns.Add(Note.PropertyNameTags);
                        //LvNotes.Columns.Add("Created Date");
                        //LvNotes.Columns.Add("Last Modified Date");
                        //LvNotes.Columns.Add("Last Accessed Date");
                    }

                    columnAddStopwatch.Stop();

                    itemAddStopwatch.Start();
                    View.VirtualListSize = m_SelectedNotes.Count;
                    itemAddStopwatch.Stop();

                    foreach (ColumnHeader header in View.Columns)
                    {
                        header.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                    }
                }
            }
            finally
            {
                View.EndUpdate();
            }
            

            stopwatch.Stop();

            Logger.Debug(
                "ModelNotesSelected took: {0}, property find: {1}, column clear: {2}, column add: {3}, item clear: {4}, item add: {5}",
                stopwatch.Elapsed,
                propertyFindStopwatch.Elapsed,
                columnClearStopwatch.Elapsed,
                columnAddStopwatch.Elapsed,
                itemClearStopwatch.Elapsed,
                itemAddStopwatch.Elapsed);
        }
    }

    public class NoteListViewItem : ListViewItem
    {
        private readonly Note m_Note;

        public NoteListViewItem(Note note)
        {
            if (note == null) throw new ArgumentNullException("note");

            m_Note = note;
        }

        public Note Note { get { return m_Note; } }
    }
}
