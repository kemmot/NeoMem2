using System;
using System.Collections.Generic;
using System.Windows.Forms;

using NeoMem2.Core;
using NeoMem2.Utils;

namespace NeoMem2.Gui.Models
{
    public class LastModifiedTreeModel : CategoryTreeModel
    {
        private const string TagNameLastHour = "Last Hour";
        private const string TagNameEarlierToday = "Earlier Today";
        private const string TagNameYesterday = "Yesterday";
        private const string TagNameThisWeek = "This Week";
        private const string TagNameLastWeek = "Last Week";
        private const string TagNameThisMonth = "This Month";
        private const string TagNameLastMonth = "Last Month";
        private const string TagNameThisYear = "This Year";
        private const string TagNameLastYear = "Last Year";
        private const string TagNameEarlier = "Earlier";


        public LastModifiedTreeModel(TreeView categoriesView)
            : base(categoriesView)
        {
        }


        public override bool SortCategories { get { return false; } }
        public override string NoteSortPropertyName { get { return Note.PropertyNameLastModifiedDate; } }
        public override bool NoteReverseSort { get { return true; } }


        protected override IEnumerable<NoteCategory> GetRootCategories(IEnumerable<Note> notes)
        {
            var sortedNotes = new List<Note>(notes);
            sortedNotes.Sort(new NoteComparer(Note.PropertyNameLastModifiedDate, true));
            
            var categories = new Dictionary<string, NoteCategory>();
            foreach (var note in sortedNotes)
            {
                string categoryName;
                if (note.LastModifiedDate > DateTime.Now.AddHours(-1))
                {
                    categoryName = TagNameLastHour;
                }
                else if (note.LastModifiedDate.Date == DateTime.Today)
                {
                    categoryName = TagNameEarlierToday;
                }
                else if (note.LastModifiedDate.Date == DateTime.Today.AddDays(-1))
                {
                    categoryName = TagNameYesterday;
                }
                else if (note.LastModifiedDate > DateTime.Today.GetStartOfWeek())
                {
                    categoryName = TagNameThisWeek;
                }
                else if (note.LastModifiedDate > DateTime.Today.GetStartOfWeek().AddDays(-7))
                {
                    categoryName = TagNameLastWeek;
                }
                else if (note.LastModifiedDate > DateTime.Today.GetStartOfMonth())
                {
                    categoryName = TagNameThisMonth;
                }
                else if (note.LastModifiedDate > DateTime.Today.GetStartOfMonth().AddMonths(-1))
                {
                    categoryName = TagNameLastMonth;
                }
                else if (note.LastModifiedDate > DateTime.Today.GetStartOfYear())
                {
                    categoryName = TagNameThisYear;
                }
                else if (note.LastModifiedDate > DateTime.Today.GetStartOfYear().AddYears(-1))
                {
                    categoryName = TagNameLastYear;
                }
                else
                {
                    categoryName = TagNameEarlier;
                }


                NoteCategory category;
                if (!categories.TryGetValue(categoryName, out category))
                {
                    category = new NoteCategory { Name = categoryName };
                    categories[categoryName] = category;
                }

                category.Notes.Add(note);
            }

            var categoryViewOrder = new List<string>
            { 
                TagNameLastHour, 
                TagNameEarlierToday, 
                TagNameYesterday, 
                TagNameThisWeek, 
                TagNameLastWeek, 
                TagNameThisMonth, 
                TagNameLastMonth, 
                TagNameThisYear, 
                TagNameLastYear, 
                TagNameEarlier 
            };

            var sortedCategories = new List<NoteCategory>();
            foreach (string tagName in categoryViewOrder)
            {
                NoteCategory category;
                if (categories.TryGetValue(tagName, out category))
                {
                    if (category.Notes.Count > 0)
                    {
                        sortedCategories.Add(category);
                    }
                }
            }

            return sortedCategories;
        }
    }
}
