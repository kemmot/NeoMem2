using System;
using System.Windows.Forms;

namespace NeoMem2.Gui.Models
{
    public class CategoryModelFactory
    {
        public ICategoryModel Get(CategoryModelType treeModelType, TreeView view)
        {
            ICategoryModel model;
            switch (treeModelType)
            {
                case CategoryModelType.Classes:
                    model = new ClassNoteTreeModel(view) { ShowNotes = false };
                    break;
                case CategoryModelType.LastModified:
                    model = new LastModifiedTreeModel(view);
                    break;
                case CategoryModelType.Multi:
                    var multiModel = new MultiCategoryTreeModel(view);
                    multiModel.AddAllModels();
                    model = multiModel;
                    break;
                case CategoryModelType.PropertyValuesFlat:
                    model = new PropertyValueTreeModel(view) { AllowPaths = false, ShowNotes = false };
                    break;
                case CategoryModelType.PropertyValuesStructured:
                    model = new PropertyValueTreeModel(view) { AllowPaths = true, ShowNotes = false };
                    break;
                case CategoryModelType.TagsFlat:
                    model = new FlatTagsTreeModel(view);
                    break;
                case CategoryModelType.Initials:
                    model = new InitialsTreeModel(view);
                    break;
                case CategoryModelType.TagsStructured:
                    model = new FolderTagsTreeModel(view);
                    break;
                default:
                    string message = string.Format(
                        "Category model type not supported: {0}",
                        treeModelType);
                    throw new NotSupportedException(message);
            }

            return model;
        }
    }
}
