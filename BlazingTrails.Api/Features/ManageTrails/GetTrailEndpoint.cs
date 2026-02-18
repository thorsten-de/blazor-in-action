using System;
using Ardalis.ApiEndpoints;
using BlazingTrails.Persistence;
using BlazingTrails.Shared.Features.ManageTrails.EditTrail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingTrails.Api.Features.ManageTrails;

public class GetTrailEndpoint(BlazingTrailsContext dbContext) :
    EndpointBaseAsync.WithRequest<int>.WithActionResult<GetTrailRequest.Response>
{
    [Authorize]
    [HttpGet(GetTrailRequest.RouteTemplate)]
    public override async Task<ActionResult<GetTrailRequest.Response>> HandleAsync(int trailId, CancellationToken cancellationToken = default)
    {
        var trail = await dbContext.Trails
            .Include(x => x.Waypoints)
            .SingleOrDefaultAsync(x => x.Id == trailId, cancellationToken);

        if (trail is null)
            return NotFound("Trail could not be found.");

        if (!trail.Owner.Equals(HttpContext.User.Identity?.Name, StringComparison.OrdinalIgnoreCase))
            return Unauthorized();

        var response = new GetTrailRequest.Response(
            new GetTrailRequest.Trail(trail.Id,
            trail.Name,
            trail.Location,
            trail.Image,
            trail.TimeInMinutes,
            trail.Length,
            trail.Description,
            trail.Waypoints.Select(wp => new GetTrailRequest.Waypoint(wp.Latitude, wp.Longitude))
        ));

        return Ok(response);
    }
}
