using System;
using System.Net.Http.Json;
using BlazingTrails.Client.Features.Home;
using BlazingTrails.Shared.Features.ManageTrails.EditTrail;
using MediatR;

namespace BlazingTrails.Client.Features.ManageTrails.EditTrail;

public class GetTrailHandler(HttpClient httpClient) : IRequestHandler<GetTrailRequest, GetTrailRequest.Response>
{
    public async Task<GetTrailRequest.Response> Handle(GetTrailRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return await httpClient
                .GetFromJsonAsync<GetTrailRequest.Response>(
                    GetTrailRequest.RouteTemplate.Replace("{trailId}", request.TrailId.ToString())
                );
        }
        catch (HttpRequestException)
        {
            return default!;
        }
    }
}
