using Microsoft.EntityFrameworkCore;
using PlanetoR.Models;

namespace PlanetoR.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<CelestialBody> CelestialBodies { get; set; }

}