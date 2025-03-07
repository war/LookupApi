using System.Data;

namespace LookupApi.Infrastructure.Common.Interfaces;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync();
}
