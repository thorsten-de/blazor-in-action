using FluentValidation;

namespace BlazingTrails.Shared.Features.ManageTrails;

public class TrailDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Location { get; set; } = "";
    public int TimeInMinutes { get; set; }
    public int Length { get; set; }

    /// <summary>
    /// The waypoints defining our trail
    /// </summary>
    public List<WaypointDto> Waypoints { get; set; } = [];

    /// <summary>
    /// Filename of the optional image
    /// </summary>
    public string? Image { get; set; }

    /// <summary>
    /// Is there any operation performed on the image?
    /// </summary>
    public ImageAction ImageAction { get; set; } = ImageAction.None;

    /// <summary>
    /// A waypoint coordinate
    /// </summary>
    public record WaypointDto(decimal Latitude, decimal Longitude);

    public void ImportDataFrom(TrailDto other)
    {
        this.Name = other.Name;
        this.Description = other.Description;
        this.Location = other.Location;
        this.Length = other.Length;
        this.TimeInMinutes = other.TimeInMinutes;
        this.ImageAction = ImageAction.None;


        this.Waypoints.Clear();
        this.Waypoints.AddRange(other.Waypoints.Select(r => new WaypointDto(r.Latitude, r.Longitude)));
    }
}

public enum ImageAction
{
    None,
    Add,
    Remove
}

/// <summary>
/// Validates Trails
/// </summary>
public class TrailValidator : AbstractValidator<TrailDto>
{
    public TrailValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Please enter a name");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Please enter a description");
        RuleFor(x => x.Location).NotEmpty().WithMessage("Please enter a location");
        RuleFor(x => x.Length).GreaterThan(0).WithMessage("Please enter a length.");
        RuleFor(x => x.TimeInMinutes).GreaterThan(0).WithMessage("Please enter the time for hiking the trail.");

        RuleFor(x => x.Waypoints).NotEmpty().WithMessage("Please add at least one waypoint.");
    }
}
