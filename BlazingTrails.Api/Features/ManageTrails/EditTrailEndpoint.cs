using Ardalis.ApiEndpoints;
using BlazingTrails.Persistence;
using BlazingTrails.Shared.Features.ManageTrails.EditTrail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingTrails.Api.Features.ManageTrails;

public class EditTrailEndpoint(BlazingTrailsContext dbContext) :
    EndpointBaseAsync.WithRequest<EditTrailRequest>.WithActionResult<bool>
{
    [Authorize]
    [HttpPut(EditTrailRequest.RouteTemplate)]
    public override async Task<ActionResult<bool>> HandleAsync(EditTrailRequest request, CancellationToken cancellationToken = default)
    {
        var trail = await dbContext.Trails
            .Include(x => x.Waypoints)
            .SingleOrDefaultAsync(x => x.Id == request.Trail.Id, cancellationToken);

        if (trail is null)
            return NotFound("Trail could not be found");

        if (!HttpContext.User.IsEditorOf(trail))
            return Unauthorized();

        trail.ImportDataFrom(request.Trail);

        // Remove the physical image file when it is removed
        if (request.Trail.ImageAction == Shared.Features.ManageTrails.ImageAction.Remove && trail.Image is not null)
        {
            System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "images", trail.Image!));
            trail.Image = null;
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return Ok(true);
    }
}
