using Microsoft.EntityFrameworkCore;
using hwapp.Core.Models;

namespace hwapp.Persistence
{
    public class HelloDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Feature> Features { get; set; }
        public HelloDbContext(DbContextOptions<HelloDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<VehicleFeature>().HasKey(vf =>
             new { vf.VehicleId, vf.FeatureId });
        }

    }
}