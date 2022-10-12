// <copyright file="Step01CreateNoteHistoryTable.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer.Updates.Version_1.Version_1_3
{
    using NeoMem2.Automation.Updates;
    using NeoMem2.Automation.Updates.Data;

    /// <summary>
    /// Update to version 1.3 step 1.
    /// </summary>
    [UpdateStep(Constants.NeoMem2DatabaseComponent, 1.3, 1, "Create NoteHistory table")]
    public class Step01CreateNoteHistoryTable : DatabaseUpdateStep
    {
        /// <summary>
        /// The SQL statement to execute for change 1.
        /// </summary>
        private const string Sql1 = @"
            CREATE TABLE [dbo].[NoteHistory]
            (
                Id bigint IDENTITY(1,1) NOT NULL,
                NoteId bigint NOT NULL,
                FieldName nvarchar(100) NOT NULL,
                ChangeDate datetime NOT NULL,
                ChangeType nvarchar(50) NOT NULL,
                Value nvarchar(MAX) NULL,
                ValueString nvarchar(MAX) NULL
                CONSTRAINT [PK_NoteHistory] PRIMARY KEY CLUSTERED 
                (
                    [Id]
                ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
            ) ON [PRIMARY]";

        /// <summary>
        /// The SQL statement to execute for change 2.
        /// </summary>
        private const string Sql2 = @"
            ALTER TABLE [dbo].[NoteHistory] ADD CONSTRAINT
                FK_NoteHistory_Note FOREIGN KEY (NoteId) REFERENCES dbo.Note (Id)
                ON UPDATE NO ACTION ON DELETE NO ACTION";

        /// <summary>
        /// Executes this update step.
        /// </summary>
        public override void Execute()
        {
            Context.ExecuteDefaultNonQueryText(Sql1);
            Context.ExecuteDefaultNonQueryText(Sql2);
        }
    }
}
