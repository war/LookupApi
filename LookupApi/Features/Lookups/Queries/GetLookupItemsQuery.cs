using LookupApi.Application.Lookups.Models;
using MediatR;

namespace LookupApi.Application.Lookups.Queries;

public class GetLookupItemsQuery : IRequest<IEnumerable<LookupItem>>
{
    public string LookupType { get; set; } = string.Empty;
    public Dictionary<string, string>? Parameters { get; set; }
}
