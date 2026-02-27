using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using BlazingTrails.Shared.Features.Home;
using MediatR;

namespace BlazingTrails.Test.Client.Features.Home;

public class GetTrailsHandler : IRequestHandler<GetTrailsRequest, GetTrailsRequest.Response>
{
    public async Task<GetTrailsRequest.Response> Handle(GetTrailsRequest request, CancellationToken cancellationToken)
    {
        // Use AutoFixture to create dummy data for testing
        var fixture = new Fixture();
        var dummyTrails = fixture.CreateMany<GetTrailsRequest.Trail>();

        return new GetTrailsRequest.Response(dummyTrails);
    }
}
