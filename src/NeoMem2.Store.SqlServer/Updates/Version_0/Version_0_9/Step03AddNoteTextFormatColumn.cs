// <copyright file="Step03AddNoteTextFormatColumn.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer.Updates.Version_0.Version_0_9
{
    using NeoMem2.Automation.Updates;
    using NeoMem2.Automation.Updates.Data;
    using NeoMem2.Core;

    /// <summary>
    /// Update to version 0.9 step 3.
    /// </summary>
    [UpdateStep(Constants.NeoMem2DatabaseComponent, 0.9, 3, "Add Note.TextFormat column")]
    public class Step03AddNoteTextFormatColumn : DatabaseUpdateStep
    {
        /// <summary>
        /// The SQL statement to execute for change 1.
        /// </summary>
        private const string Sql1 = "ALTER TABLE dbo.Note ADD TextFormat INT NOT NULL CONSTRAINT DF_Note_IsTemplate DEFAULT {0}";

        /// <summary>
        /// The SQL statement to execute for change 2.
        /// </summary>
        private const string Sql2 = @"ALTER TABLE dbo.Note ADD CONSTRAINT
            FK_Note_TextFormat FOREIGN KEY
            (
                TextFormat
            ) REFERENCES dbo.TextFormat
            (
                Id
            ) ON UPDATE NO ACTION 
            ON DELETE NO ACTION";

        /// <summary>
        /// Executes this update step.
        /// </summary>
        public override void Execute()
        {
            Context.ExecuteDefaultNonQueryText(string.Format(Sql1, (int)TextFormat.Txt));
            Context.ExecuteDefaultNonQueryText(Sql2);
        }
    }
}
