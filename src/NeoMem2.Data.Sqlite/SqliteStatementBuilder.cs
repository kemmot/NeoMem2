namespace NeoMem2.Data.Sqlite
{
    using NeoMem2.Data.FluentSql;

    using System;

    public class SqliteStatementBuilder : IProviderSqlStatementBuilder
    {
        public string DateFunction()
        {
            return "\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\"";
        }

        public string IdentityFunction()
        {
            return "last_insert_rowid()";
        }
    }
}
