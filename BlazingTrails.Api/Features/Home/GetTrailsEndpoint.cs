using Ardalis.ApiEndpoints;
using BlazingTrails.Persistence;
using BlazingTrails.Shared.Features.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingTrails.Api.Features.Home;

public class GetTrailsEndpoint(BlazingTrailsContext dbContext) : EndpointBaseAsync.WithRequest<int>.WithResult<GetTrailsRequest.Response>
{
    [HttpGet(GetTrailsRequest.RouteTemplate)]
    public override async Task<GetTrailsRequest.Response> HandleAsync(int request, CancellationToken cancellationToken = default)
    {
        var trails = await dbContext.Trails
            .Include(x => x.Waypoints)
            .ToListAsync(cancellationToken);

        var response = new GetTrailsRequest
            .Response(trails.Select(trail => new GetTrailsRequest.Trail(
                trail.Id,
                trail.Name,
                trail.Image,
                trail.Location,
                trail.TimeInMinutes,
                trail.Length,
                trail.Description,
                trail.Owner,
                trail.Waypoints.Select(wp => new GetTrailsRequest.Waypoint(wp.Latitude, wp.Longitude)).ToList()
            )));

        return response;
    }
}
