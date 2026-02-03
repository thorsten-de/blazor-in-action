using System.Net.Http.Json;
using BlazingTrails.Shared.Features.ManageTrails.AddTrail;
using MediatR;

namespace BlazingTrails.Shared.Features.ManageTrails;

public class AddTrailHandler(HttpClient httpClient) : IRequestHandler<AddTrailRequest, AddTrailRequest.Response>
{
    public async Task<AddTrailRequest.Response> Handle(AddTrailRequest request, CancellationToken cancellationToken)
    {
        var response = await httpClient.PostAsJsonAsync(AddTrailRequest.RouteTemplate, request, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var trailId = await response.Content.ReadFromJsonAsync<int>(cancellationToken);
            return new(trailId);
        }
        else
        {
            return new(-1);
        }
    }
}
