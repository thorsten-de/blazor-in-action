using BlazingTrails.Shared.Features.ManageTrails;

namespace BlazingTrails.Persistence.Model;

/// <summary>
/// The persistence model representing a waypoint
/// </summary>
public class Waypoint
{
    /// <summary>
    /// The unique identifier of the waypoint
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The unique identifier of the trail this waypoint belongs to.
    /// </summary>
    public int TrailId { get; set; }

    /// <summary>
    /// The latitude of the waypoint
    /// </summary>
    public required decimal Latitude { get; set; }

    /// <summary>
    /// The longitude of the waypoint
    /// </summary>
    public required decimal Longitude { get; set; }

    /// <summary>
    /// The trail this waypoint belongs to
    /// </summary>
    /// <remarks>
    /// This is the navigation property to the trail. It allows us to access the trail from the waypoint.
    /// </remarks>
    public Trail Trail { get; set; } = default!;

    public static Waypoint FromDto(TrailDto.WaypointDto dto)
    {
        return new Waypoint
        {
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
        };
    }
}