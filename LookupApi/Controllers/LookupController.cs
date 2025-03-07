using MediatR;
using Microsoft.AspNetCore.Mvc;
using LookupApi.Application.Lookups.Queries;
using LookupApi.Application.Lookups.Models;
using LookupApi.Application.TypeAhead.Queries;

namespace LookupApi.Controllers;

[ApiController]
[Route("api/v2/[controller]")]
[Produces("application/json")]
public class LookupsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LookupsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets lookup items of specified type
    /// </summary>
    /// <param name="lookupType">Type of lookup data to retrieve
    /// <param name="parameters">Optional parameters for filtering the lookup data</param>
    /// <returns>A list of lookup items</returns>
    /// <response code="200">Returns the lookup items</response>
    /// <response code="400">If the request parameters are invalid</response>
    /// <response code="404">If the lookup type is not found</response>
    [HttpGet("lookup/{lookupType}")]
    [ProducesResponseType(typeof(IEnumerable<LookupItem>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<LookupItem>>> GetLookupItems(
        string lookupType,
        [FromQuery] Dictionary<string, string>? parameters)
    {
        var query = new GetLookupItemsQuery
        {
            LookupType = lookupType,
            Parameters = parameters
        };

        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Gets type-ahead suggestions for a specific lookup type
    /// </summary>
    /// <param name="lookupType">Type of lookup data to search</param>
    /// <param name="searchTerm">Text to search for</param>
    /// <param name="maxResults">Maximum number of results to return (default: 100)</param>
    /// <param name="parameters">Optional additional parameters</param>
    /// <returns>A list of lookup items</returns>
    /// <response code="200">Returns the lookup items</response>
    /// <response code="400">If the request parameters are invalid</response>
    /// <response code="404">If the lookup type is not found</response>
    [HttpGet("typeahead/{lookupType}")]
    [ProducesResponseType(typeof(IEnumerable<LookupItem>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<LookupItem>>> GetTypeAheadItems(
        string lookupType,
        [FromQuery] string searchTerm,
        [FromQuery] int maxResults = 100,
        [FromQuery] Dictionary<string, string>? parameters = null)
    {
        var query = new GetTypeAheadItemsQuery
        {
            LookupType = lookupType,
            SearchTerm = searchTerm,
            MaxResults = maxResults,
            Parameters = parameters
        };

        var result = await _mediator.Send(query);
        return Ok(result);
    }
}
