namespace NeoMem2.Data.SqlServer
{
    using NeoMem2.Data.FluentSql;

    public class SqlServerStatementBuilder : IProviderSqlStatementBuilder
    {
        public string DateFunction()
        {
            return "GETDATE()";
        }

        public string IdentityFunction()
        {
            return "SCOPE_IDENTITY()";
        }
    }
}
