// <copyright file="Step01CreateNoteHistoryTypeTable.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer.Updates.Version_1.Version_1_4
{
    using NeoMem2.Automation.Updates;
    using NeoMem2.Automation.Updates.Data;

    /// <summary>
    /// Update to version 1.4 step 1.
    /// </summary>
    [UpdateStep(Constants.NeoMem2DatabaseComponent, 1.4, 1, "Create NoteHistoryType table")]
    public class Step01CreateNoteHistoryTypeTable : DatabaseUpdateStep
    {
        /// <summary>
        /// The SQL statement to execute for change 1.
        /// </summary>
        private const string Sql = @"
            CREATE TABLE [dbo].[NoteHistoryType]
            (
                Id int NOT NULL,
                Name nvarchar(50) NULL,
                CONSTRAINT [PK_NoteHistoryType] PRIMARY KEY CLUSTERED 
                (
                    [Id]
                ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
            ) ON [PRIMARY]";

        /// <summary>
        /// Executes this update step.
        /// </summary>
        public override void Execute()
        {
            Context.ExecuteDefaultNonQueryText(Sql);
        }
    }
}
