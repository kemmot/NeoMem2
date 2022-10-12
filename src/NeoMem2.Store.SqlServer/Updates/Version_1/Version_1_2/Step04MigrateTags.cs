// <copyright file="Step04MigrateTags.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer.Updates.Version_1.Version_1_2
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using NeoMem2.Automation.Updates;
    using NeoMem2.Automation.Updates.Data;
    using NeoMem2.Core;

    /// <summary>
    /// Update to version 1.2 step 4.
    /// </summary>
    [UpdateStep(Constants.NeoMem2DatabaseComponent, 1.2, 4, "Migrate tags")]
    public class Step04MigrateTags : DatabaseUpdateStep
    {
        /// <summary>
        /// The SQL statement to execute for change 1.
        /// </summary>
        private const string GetNoteTagsSql = "SELECT Id, Tags FROM Note WHERE Tags <> ''";

        /// <summary>
        /// The SQL statement to execute for change 2.
        /// </summary>
        private const string InsertTagSql = "INSERT INTO Tag (Name) VALUES ('{0}');SELECT SCOPE_IDENTITY()";

        /// <summary>
        /// The SQL statement to execute for change 3.
        /// </summary>
        private const string InsertNoteTagSql = "INSERT INTO NoteTag (NoteId, TagId) VALUES ({0}, {1})";

        /// <summary>
        /// Executes this update step.
        /// </summary>
        public override void Execute()
        {
            var tagIds = new Dictionary<string, int>();

            var noteTags = Context.ExecuteDefaultDataSetText(GetNoteTagsSql);
            foreach (DataRow noteTagRow in noteTags.Rows)
            {
                var noteId = Convert.ToInt64(noteTagRow["Id"]);
                var tagText = Convert.ToString(noteTagRow["Tags"]);

                tagText = tagText.ToLower();

                var tags = tagText.Split(new[] { Note.TagDelimiter }, StringSplitOptions.RemoveEmptyEntries).Distinct();
                foreach (string tag in tags)
                {
                    int tagId;
                    if (!tagIds.TryGetValue(tag, out tagId))
                    {
                        tagId = Convert.ToInt32(Context.ExecuteDefaultScalarText(InsertTagSql, tag));
                        tagIds[tag] = tagId;
                    }

                    Context.ExecuteDefaultNonQueryText(InsertNoteTagSql, noteId, tagId);
                }
            }
        }
    }
}
