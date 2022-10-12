using System;
using System.Collections.Generic;
using System.Windows.Forms;

using NeoMem2.Core;

namespace NeoMem2.Gui
{
    public partial class BatchChangeForm : Form
    {
        private List<Note> m_Notes;
        private readonly Dictionary<string, List<string>> m_PropertiesValues = new Dictionary<string, List<string>>();


        public BatchChangeForm()
        {
            InitializeComponent();
        }


        public List<Note> Notes
        {
            get { return m_Notes; }
            set
            {
                m_Notes = value;
                RefreshNotes();
                GetAvailableProperties();
                RefreshProperties();
            }
        }


        #region Tags

        private void BtnBatchChangeTagsClick(object sender, EventArgs e)
        {
            //GetBatchChanger(false).MakeChanges(Notes);
        }

        //private TagBatchChanger GetBatchChanger(bool simulate)
        //{
        //    TagBatchChanger batchChanger;
        //    if (OptAddTag.Checked)
        //    {
        //        batchChanger = new AddTagBatchChanger();
        //    }
        //    else if (OptRemoveTag.Checked)
        //    {
        //        batchChanger = new RemoveTagBatchChanger();
        //    }
        //    else if (OptSetTag.Checked)
        //    {
        //        batchChanger = new SetTagBatchChanger();
        //    }
        //    else
        //    {
        //        throw new NotSupportedException("Operation not supported");
        //    }

        //    batchChanger.Simulate = simulate;
        //    batchChanger.Tag = CboTag.Text;
        //    return batchChanger;
        //}

        private void BatchChangeCheckedChanged(object sender, EventArgs e)
        {
            RefreshNotes();
        }

        private void RefreshNotes()
        {
            //if (Notes == null)
            //{
            //    throw new Exception("Notes property has not been set");
            //}

            //var tags = new List<string>();

            //LvTagsPreview.BeginUpdate();
            //try
            //{
            //    CboTag.Items.Clear();
            //    LvTagsPreview.Items.Clear();
            //    foreach (Note note in GetBatchChanger(true).MakeChanges(Notes))
            //    {
            //        var item = new ListViewItem { Text = note.Name };
            //        item.SubItems.Add("Old tag temporarily unavailable");
            //        item.SubItems.Add(note.Tags);
            //        LvTagsPreview.Items.Add(item);

            //        var noteTags = note.Tags.Split(';');
            //        foreach (var noteTag in noteTags)
            //        {
            //            if (!tags.Contains(noteTag.ToUpper()))
            //            {
            //                tags.Add(noteTag.ToUpper());
            //                CboTag.Items.Add(noteTag);
            //            }
            //        }
            //    }

            //    if (LvTagsPreview.Items.Count > 0)
            //    {
            //        foreach (ColumnHeader header in LvTagsPreview.Columns)
            //        {
            //            header.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            //        }
            //    }
            //}
            //finally
            //{
            //    LvTagsPreview.EndUpdate();
            //}
        }

        #endregion

        #region Properties

        private void BtnBatchChangePropertiesClick(object sender, EventArgs e)
        {
            if (Notes == null)
            {
                throw new Exception("Notes property has not been set");
            }

            foreach (Note note in Notes)
            {
                GetNewPropertyValue(note, true);
            }
        }

        private void CboPropertyNameSelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshPropertyValues();
            RefreshProperties();
        }

        private void CboPropertyNameTextChanged(object sender, EventArgs e)
        {
            RefreshPropertyValues();
            RefreshProperties();
        }

        private void RefreshPropertyValues()
        {
            CboPropertyValue.Items.Clear();

            List<string> propertyValues;
            if (m_PropertiesValues.TryGetValue(CboPropertyName.Text, out propertyValues))
            {
                foreach (string propertyValue in propertyValues)
                {
                    CboPropertyValue.Items.Add(propertyValue);
                }
            }
        }

        private void CboPropertyValueSelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshProperties();
        }

        private void CboPropertyValueTextChanged(object sender, EventArgs e)
        {
            RefreshProperties();
        }

        private void BatchPropertyChanged(object sender, EventArgs e)
        {
            RefreshProperties();
        }

        private void GetAvailableProperties()
        {
            if (Notes == null)
            {
                throw new Exception("Notes property has not been set");
            }

            m_PropertiesValues.Clear();
            foreach (Note note in Notes)
            {
                foreach (var property in note.Properties)
                {
                    List<string> propertyValues;
                    if (!m_PropertiesValues.TryGetValue(property.Name, out propertyValues))
                    {
                        propertyValues = new List<string>();
                        m_PropertiesValues.Add(property.Name, propertyValues);
                        CboPropertyName.Items.Add(property.Name);
                    }

                    if (!propertyValues.Contains(property.Value))
                    {
                        propertyValues.Add(property.Value);
                    }
                }
            }
        }

        private void RefreshProperties()
        {
            if (Notes == null)
            {
                throw new Exception("Notes property has not been set");
            }
            
            LvPropertiesPreview.BeginUpdate();
            try
            {
                LvPropertiesPreview.Items.Clear();
                foreach (Note note in Notes)
                {
                    var item = new ListViewItem { Text = note.Name };

                    string selectedPropertyName = CboPropertyName.Text;

                    Property selectedProperty;
                    string propertyValue;
                    if (note.TryGetPropertyByName(selectedPropertyName, out selectedProperty))
                    {
                        propertyValue = selectedProperty.ValueString;
                    }
                    else
                    {
                        propertyValue = string.Empty;
                    }

                    item.SubItems.Add(propertyValue);
                    item.SubItems.Add(GetNewPropertyValue(note, true));

                    LvPropertiesPreview.Items.Add(item);
                }

                if (LvPropertiesPreview.Items.Count > 0)
                {
                    foreach (ColumnHeader header in LvPropertiesPreview.Columns)
                    {
                        header.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                    }
                }
            }
            finally
            {
                LvPropertiesPreview.EndUpdate();
            }
        }

        private string GetNewPropertyValue(Note note, bool whatIf)
        {
            string propertyName = CboPropertyName.Text;

            string newPropertyValue;
            if (OptAddProperty.Checked)
            {
                Property property;
                if (note.TryGetPropertyByName(propertyName, out property))
                {
                    newPropertyValue = property.Value;
                }
                else
                {
                    newPropertyValue = CboPropertyValue.Text;

                    if (!whatIf)
                    {
                        note.AddProperty(new Property(propertyName, newPropertyValue));
                    }
                }
            }
            else if (OptRemoveProperty.Checked)
            {
                newPropertyValue = string.Empty;
                if (!whatIf)
                {
                    Property property;
                    if (!note.TryGetPropertyByName(propertyName, out property))
                    {
                        note.RemoveProperty(propertyName);
                    }
                }
            }
            else if (OptSetProperty.Checked)
            {
                newPropertyValue = CboPropertyValue.Text;
                if (!whatIf)
                {
                    Property property;
                    if (note.TryGetPropertyByName(propertyName, out property))
                    {
                        property.Value = newPropertyValue;
                    }
                    else
                    {
                        note.AddProperty(new Property(propertyName, newPropertyValue));
                    }
                }
            }
            else
            {
                Property property;
                newPropertyValue = note.TryGetPropertyByName(propertyName, out property) ? property.Value : string.Empty;
            }

            return newPropertyValue;
        }

        #endregion
    }
}
