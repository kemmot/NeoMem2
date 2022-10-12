// <copyright file="Step01CreatePropertyTable.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.SqlServer.Updates.Version_0.Version_0_3
{
    using NeoMem2.Automation.Updates;
    using NeoMem2.Automation.Updates.Data;

    /// <summary>
    /// Update to version 0.3 step 1.
    /// </summary>
    [UpdateStep(Constants.NeoMem2DatabaseComponent, 0.3, 1, "Create property table")]
    public class Step01CreatePropertyTable : DatabaseUpdateStep
    {
        /// <summary>
        /// The SQL statement to execute for change 1.
        /// </summary>
        private const string Sql1 = @"
            CREATE TABLE dbo.Property
            (
                Id bigint NOT NULL IDENTITY (1, 1),
                NoteId bigint NOT NULL,
                Name nvarchar(100) NOT NULL,
                ClrDataType nvarchar(1000) NOT NULL,
                IsSystemProperty bit NOT NULL,
                Value nvarchar(MAX) NOT NULL,
                ValueString nvarchar(MAX) NOT NULL
            )  ON [PRIMARY]
            TEXTIMAGE_ON [PRIMARY]";

        /// <summary>
        /// The SQL statement to execute for change 2.
        /// </summary>
        private const string Sql2 = @"
            ALTER TABLE dbo.Property ADD CONSTRAINT
            PK_Property PRIMARY KEY CLUSTERED 
            (
                Id
            ) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]";

        /// <summary>
        /// The SQL statement to execute for change 3.
        /// </summary>
        private const string Sql3 = @"
            ALTER TABLE dbo.Property ADD CONSTRAINT
            FK_Property_Note FOREIGN KEY
            (
                NoteId
            ) REFERENCES dbo.Note
            (
                Id
            ) ON UPDATE  NO ACTION 
            ON DELETE  NO ACTION";

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
