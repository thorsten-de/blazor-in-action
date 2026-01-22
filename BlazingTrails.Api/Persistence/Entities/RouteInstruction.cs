namespace BlazingTrails.Persistence.Model;

public class RouteInstruction
{
    public int Id { get; set; }
    public int TrailId { get; set; }
    public required int Stage { get; set; }
    public required string Description { get; set; }

    public Trail Trail { get; set; } = default!;
}