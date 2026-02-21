using System.Diagnostics.CodeAnalysis;
using BlazingTrails.ComponentLibrary.Map;
using BlazingTrails.Shared.Features.Home;

namespace BlazingTrails.Client.Features.Home;

/// <summary>
/// Represents a trail as shown on the home page. This is the client-side representation of the trail, which is different 
/// from the TrailDto that we receive from the API, because it contains some additional logic for presentation.
/// </summary>
public class Trail
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Image { get; set; } = "";
    public string Location { get; set; } = "";
    public int TimeInMinutes { get; set; }
    public string TimeFormatted => $"{TimeInMinutes / 60}h {TimeInMinutes % 60}m";
    public int Length { get; set; }
    public required string Owner { get; set; }
    public List<LatLong> Waypoints { get; set; } = [];

    public static Trail FromRequest(GetTrailsRequest.Trail x) => new Trail
    {
        Id = x.Id,
        Name = x.Name,
        Image = x.Image ?? "",
        Description = x.Description,
        Owner = x.Owner,
        Location = x.Location,
        Length = x.Length,
        TimeInMinutes = x.TimeInMinutes,
        Waypoints = x.Waypoints
            .Select(wp => new LatLong(wp.Latitude, wp.Longitude))
            .ToList()
    };

    public static EqualityComparer<Trail> EqualityComparer { get; } = new TrailEqualityComparer();

    /// <summary>
    /// We want to use a HashSet to store the user's favorite trails, so we need to define an equality comparer for the Trail class.
    ///  We consider two trails to be equal if they have the same Id, because the Id uniquely identifies each trail.
    /// </summary>
    private class TrailEqualityComparer : EqualityComparer<Trail>
    {
        public override bool Equals(Trail? x, Trail? y)
        {
            if (x is null || y is null) return false;
            return x.Id == y.Id;
        }

        public override int GetHashCode([DisallowNull] Trail trail) => trail.Id.GetHashCode();
    }

    public class RouteInstruction
    {
        public int Stage { get; set; }
        public string Description { get; set; }
    }
}
