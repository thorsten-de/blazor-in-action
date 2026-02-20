using System;
using System.Net.Http.Json;
using BlazingTrails.Client.Features.Home;
using BlazingTrails.Shared.Features.ManageTrails.EditTrail;
using MediatR;

namespace BlazingTrails.Client.Features.ManageTrails.EditTrail;

public class GetTrailHandler(IHttpClientFactory httpClientFactory) : IRequestHandler<GetTrailRequest, GetTrailRequest.Response>
{
    public async Task<GetTrailRequest.Response> Handle(GetTrailRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var httpClient = httpClientFactory.CreateClient(Constants.SecureAPIClient);
            return await httpClient
                .GetFromJsonAsync<GetTrailRequest.Response>(
                    GetTrailRequest.RouteTemplate.Replace("{trailId}", request.TrailId.ToString()),
                    cancellationToken
                );
        }
        catch (HttpRequestException)
        {
            return default!;
        }
    }
}
