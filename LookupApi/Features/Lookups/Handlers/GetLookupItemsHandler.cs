using MediatR;
using Dapper;
using System.Data;
using LookupApi.Application.Lookups.Queries;
using LookupApi.Application.Lookups.Models;
using LookupApi.Infrastructure.Common.Interfaces;

namespace LookupApi.Application.Lookups.Handlers;

public class GetLookupItemsHandler : IRequestHandler<GetLookupItemsQuery, IEnumerable<LookupItem>>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public GetLookupItemsHandler(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<LookupItem>> Handle(GetLookupItemsQuery request, CancellationToken cancellationToken)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        // The stored procedure name is derived from the lookup type
        // For example, "Countries" would call "sp_GetLookup_Countries"
        var procName = $"sp_GetLookup_{request.LookupType}";

        // Convert parameters dictionary to DynamicParameters for Dapper
        var parameters = new DynamicParameters();
        if (request.Parameters != null)
        {
            foreach (var param in request.Parameters)
            {
                parameters.Add(param.Key, param.Value);
            }
        }

        return await connection.QueryAsync<LookupItem>(
            procName,
            parameters,
            commandType: CommandType.StoredProcedure);
    }
}
