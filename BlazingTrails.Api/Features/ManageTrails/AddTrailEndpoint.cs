using System;
using Ardalis.ApiEndpoints;
using BlazingTrails.Persistence;
using BlazingTrails.Persistence.Model;
using BlazingTrails.Shared.Features.ManageTrails.AddTrail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazingTrails.Api.Features.ManageTrails;

public class AddTrailEndpoint(BlazingTrailsContext dbContext) : EndpointBaseAsync.WithRequest<AddTrailRequest>.WithResult<int>
{

    [Authorize]
    [HttpPost(AddTrailRequest.RouteTemplate)]
    public override async Task<int> HandleAsync(AddTrailRequest req, CancellationToken cancellationToken = default)
    {
        // TODO: Get the proper owner
        string owner = "";
        var trail = new Trail
        {
            Name = req.Trail.Name,
            Description = req.Trail.Description,
            Location = req.Trail.Location,
            TimeInMinutes = req.Trail.TimeInMinutes,
            Length = req.Trail.Length,
            Owner = owner,
            Waypoints = req.Trail.Waypoints.Select(Waypoint.FromDto).ToList()
        };

        await dbContext.Trails.AddAsync(trail, cancellationToken);
        await dbContext.SaveChangesAsync();

        return trail.Id;
    }
}
