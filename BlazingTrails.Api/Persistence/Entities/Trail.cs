using BlazingTrails.Shared.Features.ManageTrails;

namespace BlazingTrails.Persistence.Model;

public class Trail
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public string? Image { get; set; }
    public required string Location { get; set; }
    public required int TimeInMinutes { get; set; }
    public required int Length { get; set; }

    public ICollection<RouteInstruction> Route { get; set; } = default!;

    public void ImportDataFrom(TrailDto other)
    {
        this.Name = other.Name;
        this.Description = other.Description;
        this.Location = other.Location;
        this.Length = other.Length;
        this.TimeInMinutes = other.TimeInMinutes;

        this.Route = other.Route.Select(r => new RouteInstruction
        {
            Stage = r.Stage,
            Description = r.Description,
            Trail = this
        }).ToList();
    }
}