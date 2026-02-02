using System;
using Ardalis.ApiEndpoints;
using BlazingTrails.Client.Features.Home;
using BlazingTrails.Persistence;
using BlazingTrails.Shared.Features.ManageTrails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace BlazingTrails.Api.Features.ManageTrails;

public class UpdateTrailEndpoint(BlazingTrailsContext dbContext) : EndpointBaseAsync
    .WithRequest<int>.WithActionResult<string>
{


    [HttpPost(UploadTrailImageRequest.RouteTemplate)]
    public override async Task<ActionResult<string>> HandleAsync([FromRoute] int trailId, CancellationToken cancellationToken = default)
    {
        var trail = await dbContext.Trails.SingleOrDefaultAsync(x => x.Id == trailId, cancellationToken);
        if (trail is null)
            return BadRequest("Trail does not exist");

        var file = Request.Form.Files.First();
        if (file.Length == 0)
            return BadRequest("No image found.");

        var filename = $"{Guid.NewGuid()}.jpg";
        var saveLocation = Path.Combine(Directory.GetCurrentDirectory(), "images", filename);

        using var image = Image.Load(file.OpenReadStream());
        image.Mutate(img => img.Resize(new ResizeOptions
        {
            Mode = ResizeMode.Pad,
            Size = new(640, 426)
        }));
        await image.SaveAsJpegAsync(saveLocation, cancellationToken);

        trail.Image = filename;
        await dbContext.SaveChangesAsync(cancellationToken);

        return Ok(trail.Image);
    }
}
