// <copyright file="Step01CreateAttachmentTable.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer.Updates.Version_0.Version_0_6
{
    using NeoMem2.Automation.Updates;
    using NeoMem2.Automation.Updates.Data;

    /// <summary>
    /// Update to version 0.6 step 1.
    /// </summary>
    [UpdateStep(Constants.NeoMem2DatabaseComponent, 0.6, 1, "Create attachment table")]
    public class Step01CreateAttachmentTable : DatabaseUpdateStep
    {
        /// <summary>
        /// The SQL statement to execute for change 1.
        /// </summary>
        private const string Sql1 = @"
            CREATE TABLE dbo.Attachment
	        (
	            Id bigint IDENTITY(1,1) NOT NULL,
	            NoteId bigint NOT NULL,
	            Filename nvarchar(MAX) NOT NULL,
	            Data varbinary(MAX) NULL,
	            DataLength bigint CONSTRAINT DF_Attachment_DataLength DEFAULT 0
	        )";

        /// <summary>
        /// The SQL statement to execute for change 2.
        /// </summary>
        private const string Sql2 = @"
            ALTER TABLE dbo.Attachment ADD CONSTRAINT
	            PK_Attachment PRIMARY KEY CLUSTERED (Id)";

        /// <summary>
        /// The SQL statement to execute for change 3.
        /// </summary>
        private const string Sql3 = @"
            ALTER TABLE dbo.Attachment ADD CONSTRAINT
	            FK_Attachment_Note FOREIGN KEY (NoteId) REFERENCES dbo.Note (Id)
                ON UPDATE CASCADE
                ON DELETE CASCADE";

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
