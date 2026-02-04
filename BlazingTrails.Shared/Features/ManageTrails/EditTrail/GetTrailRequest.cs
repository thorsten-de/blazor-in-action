using System;
using MediatR;

namespace BlazingTrails.Shared.Features.ManageTrails.EditTrail;

/// <summary>
/// Takes an Id that must be retrieved from the API.
/// </summary>
/// <param name="TrailId">Id for the Trail</param>
public record GetTrailRequest(int TrailId) : IRequest<GetTrailRequest.Response>
{
    public const string RouteTemplate = "/api/trails/{trailId}";

    public record Trail(int Id, string Name, string Location,
        string? Image, int TimeInMinutes, int Length, string Description,
        IEnumerable<RouteInstruction> Route);

    public record RouteInstruction(int Id, int Stage, string Description);

    /// <summary>
    /// Response returns structured Trail data
    /// </summary>
    /// <param name="Trail">Trail data to be returned</param>
    public record Response(Trail Trail);

}
