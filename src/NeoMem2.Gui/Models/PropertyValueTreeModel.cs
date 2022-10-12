using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using NeoMem2.Core;
using NeoMem2.Utils;

namespace NeoMem2.Gui.Models
{
    public class PropertyValueTreeModel : CategoryTreeModel
    {
        public PropertyValueTreeModel(TreeView view)
            : base(view)
        {
        }


        public bool AllowPaths { get; set; }
        public bool IncludeTemplates { get; set; }


        protected override IEnumerable<NoteCategory> GetRootCategories(IEnumerable<Note> notes)
        {
            var propertyCategories = new List<NoteCategory>();

            var collatedNotes = CollateProperties(notes);

            foreach (string propertyName in (from pn in collatedNotes.Keys orderby pn select pn))
            {
                var propertyCategory = new NoteCategory(propertyName);
                propertyCategories.Add(propertyCategory);

                int uniqueValueCount = 0;
                int allLeafCount = 0;

                var pathCategories = new Dictionary<string, NoteCategory>();

                var propertyEntries = collatedNotes[propertyName];
                var orderedValues = (from pv in propertyEntries.Keys orderby pv select pv);
                foreach (string propertyValue in orderedValues)
                {
                    uniqueValueCount++;

                    string propertyValueString = string.IsNullOrEmpty(propertyValue) ? NoneTag : propertyValue;

                    string[] pathParts;
                    if (AllowPaths && !string.IsNullOrEmpty(propertyValueString) && propertyValueString.Contains(Path.DirectorySeparatorChar))
                    {
                        pathParts = propertyValueString.Split(new char[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    else
                    {
                        pathParts = new[] { propertyValueString };
                    }

                    string path = string.Empty;
                    NoteCategory parentCategory = propertyCategory;
                    foreach (string pathPart in pathParts)
                    {
                        if (path.Length > 0) path += Path.DirectorySeparatorChar;
                        path += pathPart;

                        NoteCategory pathPartCategory;
                        if (!pathCategories.TryGetValue(path, out pathPartCategory))
                        {
                            pathPartCategory = new NoteCategory(pathPart);
                            pathCategories[path] = pathPartCategory;
                            parentCategory.SubCategories.Add(pathPartCategory);
                        }

                        parentCategory = pathPartCategory;
                    }
                }

                foreach (string propertyValue in orderedValues)
                {
                    string propertyValueString = string.IsNullOrEmpty(propertyValue) ? NoneTag : propertyValue;
                    if (AllowPaths)
                    {
                        propertyValueString = propertyValueString.TrimStart(Path.DirectorySeparatorChar);
                    }

                    var parentCategory = pathCategories[propertyValueString];
                    var propertyValueNotes = propertyEntries[propertyValue];
                    foreach (Note note in (from n in propertyValueNotes orderby n.Name select n))
                    {
                        parentCategory.Notes.Add(note);
                        propertyCategory.Notes.Add(note);
                        if (ShowNotes)
                        {
                            parentCategory.Notes.Add(note);
                        }
                    }

                    allLeafCount += propertyValueNotes.Count;
                }

                //foreach (var pathNodePair in pathCategories)
                //{
                //    int immediateNoteCount;
                //    int allNoteCount;
                    //GetNoteCount(pathNodePair.Value, true, out immediateNoteCount, out allNoteCount);
                    //pathNodePair.Value.SetText();
                //}
            }

            return propertyCategories;
        }

        //public override void Refresh(INoteView notes)
        //{
        //    AddCategoryNodes(GetPropertyNodes(notes.GetNotes()));
        //    UpdateNoteCount();
        //}

        private IEnumerable<NoteCategoryTreeNode> GetPropertyNodes(IEnumerable<Note> notes)
        {
            var propertyNodes = new List<NoteCategoryTreeNode>();

            var collatedNotes = CollateProperties(notes);

            foreach (string propertyName in (from pn in collatedNotes.Keys orderby pn select pn))
            {
                var category = new NoteCategory(propertyName);
                var propertyNode = new NoteCategoryTreeNode(category);
                propertyNodes.Add(propertyNode);

                int uniqueValueCount = 0;
                int allLeafCount = 0;

                var pathNodes = new Dictionary<string, NoteCategoryTreeNode>();

                var propertyEntries = collatedNotes[propertyName];
                var orderedValues = (from pv in propertyEntries.Keys orderby pv select pv);
                foreach (string propertyValue in orderedValues)
                {
                    uniqueValueCount++;

                    string propertyValueString = string.IsNullOrEmpty(propertyValue) ? "[None]" : propertyValue;

                    string[] pathParts;
                    if (AllowPaths && !string.IsNullOrEmpty(propertyValueString))
                    {
                        pathParts = propertyValueString.Split(Path.DirectorySeparatorChar);
                    }
                    else
                    {
                        pathParts = new[] { propertyValueString };
                    }

                    string path = string.Empty;
                    NoteCategoryTreeNode parentNode = propertyNode;
                    foreach (string pathPart in pathParts)
                    {
                        if (path.Length > 0) path += Path.DirectorySeparatorChar;
                        path += pathPart;

                        NoteCategoryTreeNode pathPartNode;
                        if (!pathNodes.TryGetValue(path, out pathPartNode))
                        {
                            var pathPartCategory = new NoteCategory(pathPart);
                            pathPartNode = new NoteCategoryTreeNode(pathPartCategory);
                            pathNodes[path] = pathPartNode;
                            parentNode.Nodes.Add(pathPartNode);
                            parentNode.NoteCategory.SubCategories.Add(pathPartCategory);
                        }

                        parentNode = pathPartNode;
                    }
                }

                foreach (string propertyValue in orderedValues)
                {
                    string propertyValueString = string.IsNullOrEmpty(propertyValue) ? "[None]" : propertyValue;
                    var parentNode = pathNodes[propertyValueString];
                    var propertyValueNotes = propertyEntries[propertyValue];
                    foreach (Note note in (from n in propertyValueNotes orderby n.Name select n))
                    {
                        parentNode.NoteCategory.Notes.Add(note);
                        category.Notes.Add(note);
                        if (ShowNotes)
                        {
                            parentNode.Nodes.Add(new NoteTreeNode(note, false));
                        }
                    }

                    allLeafCount += propertyValueNotes.Count;
                }

                foreach (var pathNodePair in pathNodes)
                {
                    int immediateNoteCount;
                    int allNoteCount;
                    GetNoteCount(pathNodePair.Value, true, out immediateNoteCount, out allNoteCount);
                    pathNodePair.Value.SetText();
                }

                propertyNode.SetText();
            }

            return propertyNodes;
        }

        private void GetNoteCount(TreeNode node, bool recurse, out int immediateNoteCount, out int allNoteCount)
        {
            immediateNoteCount = 0;
            allNoteCount = 0;
            foreach (TreeNode childNode in node.Nodes)
            {
                if (childNode is NoteTreeNode)
                {
                    immediateNoteCount++;
                    allNoteCount++;
                }
                else
                {
                    if (recurse)
                    {
                        int immediateChildCount;
                        int allChildCount;
                        GetNoteCount(childNode, true, out immediateChildCount, out allChildCount);
                        allNoteCount += allChildCount;
                    }
                }
            }
        }

        private Dictionary<string, Dictionary<string, List<Note>>> CollateProperties(IEnumerable<Note> notes)
        {
            var propertyValueNotes = new Dictionary<string, Dictionary<string, List<Note>>>();
            foreach (Note note in notes)
            {
                if (note.Namespace != NoteNamespace.NoteTemplate || IncludeTemplates)
                {
                    foreach (Property property in note.Properties)
                    {
                        Dictionary<string, List<Note>> propertyEntries;
                        if (!propertyValueNotes.TryGetValue(property.Name, out propertyEntries))
                        {
                            propertyEntries = new Dictionary<string, List<Note>>();
                            propertyValueNotes[property.Name] = propertyEntries;
                        }

                        List<Note> propertyValueEntries;
                        if (!propertyEntries.TryGetValue(property.Value, out propertyValueEntries))
                        {
                            propertyValueEntries = new List<Note>();
                            propertyEntries[property.Value] = propertyValueEntries;
                        }

                        propertyValueEntries.Add(note);
                    }
                }
            }

            return propertyValueNotes;
        }
        
        public override void RemoveNote(Note note)
        {
            foreach (TreeNode node in View.Nodes)
            {
                var noteNode = node as NoteTreeNode;
                if (noteNode != null && noteNode.Note == note)
                {
                    noteNode.Remove();
                    UpdateNoteCount();
                    break;
                }
            }
        }

        protected void UpdateNoteCount()
        {
            OnDisplayedNoteCountChanged(new ItemEventArgs<int>(View.Nodes.Count));
        }
    }
}
