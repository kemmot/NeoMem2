using System;
using System.Collections.Generic;
using System.Windows.Forms;

using NeoMem2.Core;
using NeoMem2.Utils;

using System.Linq;

namespace NeoMem2.Gui.Models
{
    public class MultiCategoryTreeModel : CategoryTreeModel
    {
        private readonly List<CategoryDetails> m_CategoryDetails = new List<CategoryDetails>();
        private readonly Dictionary<string, ICategoryModel> m_Models = new Dictionary<string, ICategoryModel>();
        private List<Note> m_SelectedNotes = new List<Note>();

        public MultiCategoryTreeModel(TreeView view)
            : base(view)
        {
        }

        public override MultiSelectActionType MultiSelectAction { get { return MultiSelectActionType.Intersect; } }

        public void AddAllModels()
        {
            var factory = new CategoryModelFactory();
            foreach (CategoryModelType modelType in Enum.GetValues(typeof(CategoryModelType)))
            {
                if (modelType != CategoryModelType.Multi)
                {
                    var model = factory.Get(modelType, View);
                    model.Unregister();

                    var categoryTreeModel = model as CategoryTreeModel;
                    if (categoryTreeModel != null)
                    {
                        categoryTreeModel.ExcludeDefaultCategories = true;
                    }

                    AddModel(new EnumWrapper<CategoryModelType>(modelType).Description, model);
                }
            }
        }

        public void AddModel(string name, ICategoryModel model)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (name.Length == 0) throw new ArgumentException("Argument cannot be zero length", "name");
            if (model == null) throw new ArgumentNullException("model");

            m_Models[name] = model;
        }

        protected override IEnumerable<NoteCategory> GetRootCategories(IEnumerable<Note> notes)
        {
            var modelNames = new List<string>(m_Models.Keys);
            modelNames.Sort();

            var categories = new List<NoteCategory>();
            foreach (var modelName in modelNames)
            {
                var category = new NoteCategory(modelName);
                foreach (NoteCategory subCategory in m_Models[modelName].GetCategories(notes))
                {
                    category.SubCategories.Add(subCategory);
                    m_CategoryDetails.Add(new CategoryDetails { SubCategory = subCategory, IsAdded = true });
                }

                categories.Add(category);
            }

            return categories;
        }

        public override void Unregister()
        {
            base.Unregister();
            foreach (var model in m_Models.Values)
            {
                var treeModel = model as TreeModelBase;
                if (treeModel != null)
                {
                    treeModel.Unregister();
                }
            }
        }

        protected override void OnNotesSelected(ItemEventArgs<List<Note>> e)
        {
            m_SelectedNotes = e.Item;
            base.OnNotesSelected(e);
        }

        protected override void AfterCheck(object sender, TreeViewEventArgs e)
        {
            base.AfterCheck(sender, e);

            if (m_SelectedNotes.Count == 0) return;

            foreach (TreeNode nodeToRemove in CheckNodes(View.Nodes))
            {
                nodeToRemove.Remove();
            }
        }

        private IEnumerable<TreeNode> CheckNodes(TreeNodeCollection nodes)
        {
            if (nodes == null) yield break;

            foreach (TreeNode node in nodes)
            {
                foreach (TreeNode nodeToRemove in CheckNode(node))
                {
                    yield return nodeToRemove;
                }
            }
        }

        private IEnumerable<TreeNode> CheckNode(TreeNode node)
        {
            if (node == null) yield break;
            if (m_SelectedNotes.Count == 0) yield break;

            foreach (TreeNode nodeToRemove in CheckNodes(node.Nodes))
            {
                yield return nodeToRemove;
            }
            
            NoteCategoryTreeNode categoryNode = node as NoteCategoryTreeNode;
            if (categoryNode != null)
            {
                var categoryDetails = m_CategoryDetails.FirstOrDefault(c => c.SubCategory == categoryNode.NoteCategory);
                if (categoryDetails != null)
                {
                    bool anyMatch = false;
                    foreach (Note note in m_SelectedNotes)
                    {
                        if (categoryNode.NoteCategory.Notes.Contains(note))
                        {
                            anyMatch = true;
                            break;
                        }
                    }

                    if (!anyMatch)
                    {
                        categoryDetails.IsAdded = false;
                        yield return node;
                    }
                }
            }
        }
    }

    public class CategoryDetails
    {
        public NoteCategory SubCategory { get; set; }
        public bool IsAdded { get; set; }
    }
}
