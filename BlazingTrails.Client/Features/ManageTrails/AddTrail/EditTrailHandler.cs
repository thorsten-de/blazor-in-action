using System.Net.Http.Json;
using BlazingTrails.Shared.Features.ManageTrails.EditTrail;
using MediatR;

namespace BlazingTrails.Client.Features.ManageTrails.AddTrail;

public class EditTrailhandler([FromKeyedServices(Constants.SecureAPIClient)] HttpClient httpClient) :
    IRequestHandler<EditTrailRequest, EditTrailRequest.Response>
{
    public async Task<EditTrailRequest.Response> Handle(EditTrailRequest request, CancellationToken cancellationToken)
    {
        var response = await httpClient
            .PutAsJsonAsync(EditTrailRequest.RouteTemplate, request, cancellationToken);

        if (response.IsSuccessStatusCode)
            return new(true);
        else
            return new(false);
    }
}
