// <copyright file="Step01CreateNoteLinkTable.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer.Updates.Version_0.Version_0_5
{
    using NeoMem2.Automation.Updates;
    using NeoMem2.Automation.Updates.Data;

    /// <summary>
    /// Update to version 0.5 step 1.
    /// </summary>
    [UpdateStep(Constants.NeoMem2DatabaseComponent, 0.5, 1, "Create note link table")]
    public class Step01CreateNoteLinkTable : DatabaseUpdateStep
    {
        /// <summary>
        /// The SQL statement to execute for change 1.
        /// </summary>
        private const string Sql1 = @"
            CREATE TABLE [dbo].[NoteLink] (
                Id bigint NOT NULL IDENTITY (1, 1),
                Note1Id bigint NOT NULL,
                Note2Id bigint NOT NULL,
                CONSTRAINT [PK_NoteLink] PRIMARY KEY CLUSTERED
                (
                    [Id] ASC
                ) WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
            ) ON [PRIMARY]";

        /// <summary>
        /// The SQL statement to execute for change 2.
        /// </summary>
        private const string Sql2 = @"
            ALTER TABLE dbo.NoteLink ADD CONSTRAINT
                FK_NoteLink_Note FOREIGN KEY (Note1Id) REFERENCES dbo.Note (Id)
                ON UPDATE NO ACTION
                ON DELETE NO ACTION";

        /// <summary>
        /// The SQL statement to execute for change 3.
        /// </summary>
        private const string Sql3 = @"
            ALTER TABLE dbo.NoteLink ADD CONSTRAINT
                FK_NoteLink_Note1 FOREIGN KEY (Note2Id) REFERENCES dbo.Note (Id)
                ON UPDATE NO ACTION 
                ON DELETE NO ACTION ";

        /// <summary>
        /// The SQL statement to execute for change 4.
        /// </summary>
        private const string Sql4 = @"
            CREATE UNIQUE NONCLUSTERED INDEX IX_NoteLink ON dbo.NoteLink
            (
                Note1Id,
                Note2Id
            ) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]";
        
        /// <summary>
        /// Executes this update step.
        /// </summary>
        public override void Execute()
        {
            Context.ExecuteDefaultNonQueryText(Sql1);
            Context.ExecuteDefaultNonQueryText(Sql2);
            Context.ExecuteDefaultNonQueryText(Sql3);
            Context.ExecuteDefaultNonQueryText(Sql4);
        }
    }
}
