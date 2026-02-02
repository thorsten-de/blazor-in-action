using System;
using Ardalis.ApiEndpoints;
using BlazingTrails.Persistence;
using BlazingTrails.Persistence.Model;
using BlazingTrails.Shared.Features.ManageTrails;
using Microsoft.AspNetCore.Mvc;

namespace BlazingTrails.Api.Features.ManageTrails;

public class AddTrailEndpoint(BlazingTrailsContext dbContext) : EndpointBaseAsync.WithRequest<AddTrailRequest>.WithResult<int>
{

    [HttpPost(AddTrailRequest.RouteTemplate)]
    public override async Task<int> HandleAsync(AddTrailRequest req, CancellationToken cancellationToken = default)
    {
        var trail = new Trail
        {
            Name = req.Trail.Name,
            Description = req.Trail.Description,
            Location = req.Trail.Location,
            TimeInMinutes = req.Trail.TimeInMinutes,
            Length = req.Trail.Length,
            Route = req.Trail.Route.Select(x => new RouteInstruction
            {
                Stage = x.Stage,
                Description = x.Description,
            }).ToArray()
        };

        await dbContext.Trails.AddAsync(trail, cancellationToken);
        await dbContext.SaveChangesAsync();

        return trail.Id;
    }
}
