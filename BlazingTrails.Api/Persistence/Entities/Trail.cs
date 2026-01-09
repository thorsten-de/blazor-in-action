using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
}