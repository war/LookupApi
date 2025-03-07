using LookupApi.Application.TypeAhead.Models;
using MediatR;

namespace LookupApi.Application.TypeAhead.Queries;

public class GetTypeAheadItemsQuery : IRequest<IEnumerable<TypeAheadItem>>
{
    public string LookupType { get; set; } = string.Empty;
    public string SearchTerm { get; set; } = string.Empty;
    public int MaxResults { get; set; } = 100;
    public Dictionary<string, string>? Parameters { get; set; }
}
