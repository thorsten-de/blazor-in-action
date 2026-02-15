using Microsoft.EntityFrameworkCore;
using BlazingTrails.Persistence.Model;

namespace BlazingTrails.Persistence;

public class BlazingTrailsContext : DbContext
{
    public DbSet<Trail> Trails => Set<Trail>();
    public DbSet<Waypoint> Waypoints => Set<Waypoint>();

    public BlazingTrailsContext(DbContextOptions<BlazingTrailsContext> options) : base(options) { }

    /* This method is called by the Entity Framework when it needs to create the database model.
     * We use it to apply our entity configurations, which are defined in separate classes implementing IEntityTypeConfiguration<T>.
     * By calling ApplyConfigurationsFromAssembly, we tell Entity Framework to scan the assembly for any classes that implement IEntityTypeConfiguration<T> and apply their configurations to the model.
     * This keeps our DbContext clean and allows us to organize our entity configurations in separate classes, which is especially beneficial as our model grows in complexity.
     * 
     * As we don't have any custom configurations yet, we can omit this method for now.
     *
     *  protected override void OnModelCreating(ModelBuilder modelBuilder)
     *   {
     *       base.OnModelCreating(modelBuilder);
     *       modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlazingTrailsContext).Assembly);
     *   } 
     */
}