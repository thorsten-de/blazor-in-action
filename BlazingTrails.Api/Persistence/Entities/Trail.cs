using BlazingTrails.Shared.Features.ManageTrails;

namespace BlazingTrails.Persistence.Model;

/// <summary>
/// The persistence model representing a trail in the database
/// </summary>
public class Trail
{
    /// <summary>
    /// The unique identifier of the trail
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the trail.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The description of the trail.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// The filename of the optional image for the trail. 
    /// </summary>
    /// <remarks>
    /// This is just the filename, not the full path. The actual image file is stored in the <c>wwwroot/images</c> folder.
    /// </remarks>
    public string? Image { get; set; }

    /// <summary>
    /// The location of the trail, described as a human readable string, e.g. "Bavarian Forest, Germany".
    /// </summary>
    public required string Location { get; set; }

    /// <summary>
    /// The time in minutes it takes to hike the trail.
    /// </summary>
    public required int TimeInMinutes { get; set; }

    /// <summary>
    /// The length of the trail in kilometers.
    /// </summary>
    public required int Length { get; set; }

    /// <summary>
    /// The waypoints defining our trail
    /// </summary>
    public ICollection<Waypoint> Waypoints { get; set; } = default!;

    /// <summary>
    /// Imports the data from a TrailDto into this Trail entity. This is used when 
    /// we want to update an existing trail with the data from a TrailDto.
    /// </summary>
    /// <param name="other">Trail data to import</param>
    public void ImportDataFrom(TrailDto other)
    {
        this.Name = other.Name;
        this.Description = other.Description;
        this.Location = other.Location;
        this.Length = other.Length;
        this.TimeInMinutes = other.TimeInMinutes;

        this.Waypoints = other.Waypoints.Select(w => new Waypoint
        {
            Latitude = w.Latitude,
            Longitude = w.Longitude,
            Trail = this
        }).ToList();
    }
}