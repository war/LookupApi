using MediatR;
using Dapper;
using System.Data;
using LookupApi.Application.TypeAhead.Queries;
using LookupApi.Infrastructure.Common.Interfaces;
using LookupApi.Application.TypeAhead.Models;

namespace LookupApi.Application.Lookups.Handlers;

public class GetTypeAheadItemsHandler : IRequestHandler<GetTypeAheadItemsQuery, IEnumerable<TypeAheadItem>>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public GetTypeAheadItemsHandler(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<TypeAheadItem>> Handle(GetTypeAheadItemsQuery request, CancellationToken cancellationToken)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        // The stored procedure name is derived from the lookup type
        // For example, "Users" would call "sp_GetTypeAhead_Users"
        var procName = $"sp_GetTypeAhead_{request.LookupType}";

        var parameters = new DynamicParameters();
        parameters.Add("SearchTerm", request.SearchTerm);
        parameters.Add("MaxResults", request.MaxResults);

        if (request.Parameters != null)
        {
            foreach (var param in request.Parameters)
            {
                parameters.Add(param.Key, param.Value);
            }
        }

        // Get results with AdditionalData as string
        var results = await connection.QueryAsync<TypeAheadItem>(
            procName,
            parameters,
            commandType: CommandType.StoredProcedure);

        return results;
    }
}
