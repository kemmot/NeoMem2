// <copyright file="Step03CreateNoteTagTable.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer.Updates.Version_1.Version_1_2
{
    using NeoMem2.Automation.Updates;
    using NeoMem2.Automation.Updates.Data;

    /// <summary>
    /// Update to version 1.2 step 3.
    /// </summary>
    [UpdateStep(Constants.NeoMem2DatabaseComponent, 1.2, 3, "Create NoteTag table")]
    public class Step03CreateNoteTagTable : DatabaseUpdateStep
    {
        /// <summary>
        /// The SQL statement to execute for change 1.
        /// </summary>
        private const string Sql1 = @"
            CREATE TABLE [dbo].[NoteTag](
                NoteId bigint NOT NULL,
                TagId int NOT NULL
                CONSTRAINT [PK_NoteTag] PRIMARY KEY CLUSTERED 
                (
                    [NoteId], [TagId]
                ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
            ) ON [PRIMARY]";

        /// <summary>
        /// The SQL statement to execute for change 2.
        /// </summary>
        private const string Sql2 = @"
            ALTER TABLE [dbo].[NoteTag] ADD CONSTRAINT
                FK_NoteTag_Note FOREIGN KEY (NoteId) REFERENCES dbo.Note (Id)
                ON UPDATE NO ACTION ON DELETE NO ACTION";

        /// <summary>
        /// The SQL statement to execute for change 3.
        /// </summary>
        private const string Sql3 = @"
            ALTER TABLE [dbo].[NoteTag] ADD CONSTRAINT
                FK_NoteTag_Tag FOREIGN KEY (TagId) REFERENCES dbo.Tag (Id)
                ON UPDATE NO ACTION ON DELETE NO ACTION";

        /// <summary>
        /// Executes this update step.
        /// </summary>
        public override void Execute()
        {
            Context.ExecuteDefaultNonQueryText(Sql1);
            Context.ExecuteDefaultNonQueryText(Sql2);
            Context.ExecuteDefaultNonQueryText(Sql3);
        }
    }
}
