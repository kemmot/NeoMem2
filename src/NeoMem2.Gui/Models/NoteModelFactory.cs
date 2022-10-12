using System;
using System.Windows.Forms;

using NeoMem2.Core;

namespace NeoMem2.Gui.Models
{
    public class NoteModelFactory
    {
        public INoteViewModel Get(NoteModelType treeModelType, TreeView view)
        {
            INoteViewModel model;
            switch (treeModelType)
            {
                case NoteModelType.ClassNotes:
                    model = new ClassNoteTreeModel(view) { ExcludeDefaultCategories = true, ShowNotes = true, TemplateVisibility = Properties.Settings.Default.TemplateVisibility };
                    break;
                case NoteModelType.AlphabeticalFlatNotes:
                    model = new FlatNoteTreeModel(view, Note.PropertyNameName);
                    break;
                case NoteModelType.FlatPropertyValueNotes:
                    model = new PropertyValueTreeModel(view) { AllowPaths = false, ExcludeDefaultCategories = true, ShowNotes = true };
                    break;
                case NoteModelType.PropertyValueNotes:
                    model = new PropertyValueTreeModel(view) { AllowPaths = true, ExcludeDefaultCategories = true, ShowNotes = true };
                    break;
                case NoteModelType.ScoredFlatNotes:
                    model = new FlatNoteTreeModel(view, Note.PropertyNameScore);
                    break;
                case NoteModelType.StructuredNotes:
                    model = new StructuredNoteTreeModel(view, Properties.Settings.Default.StructuredNoteNameDelimiter);
                    break;
                default:
                    string message = string.Format(
                        "Note model type not supported: {0}",
                        treeModelType);
                    throw new NotSupportedException(message);
            }

            return model;
        }
    }
}
