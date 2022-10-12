// <copyright file="SelectSqlStatementBuilder.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Data.FluentSql
{
    public interface IProviderSqlStatementBuilder
    {
        string DateFunction();

        string IdentityFunction();
    }
}