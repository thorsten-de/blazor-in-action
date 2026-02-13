using System.Net.Http.Json;
using BlazingTrails.Shared.Features.Home;
using MediatR;

namespace BlazingTrails.Client.Features.Home.Shared;

public class GetTrailsHandler(HttpClient httpClient) : IRequestHandler<GetTrailsRequest, GetTrailsRequest.Response>
{
    public async Task<GetTrailsRequest.Response> Handle(GetTrailsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return await httpClient.GetFromJsonAsync<GetTrailsRequest.Response>(GetTrailsRequest.RouteTemplate);
        }
        catch (HttpRequestException)
        {
            return default!;
        }
    }
}
