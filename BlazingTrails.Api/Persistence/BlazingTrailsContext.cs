using Microsoft.EntityFrameworkCore;
using BlazingTrails.Persistence.Model;

namespace BlazingTrails.Persistence;

public class BlazingTrailsContext : DbContext
{
    public DbSet<Trail> Trails => Set<Trail>();
    public DbSet<RouteInstruction> RouteInstructions => Set<RouteInstruction>();

    public BlazingTrailsContext(DbContextOptions<BlazingTrailsContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlazingTrailsContext).Assembly);
    }
}