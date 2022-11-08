using System.Data;

namespace TestApp.BuildingBlocks.Infrastructure.Database
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();

        IDbConnection CreateNewConnection();

        string GetConnectionString();
    }
}