using Microsoft.EntityFrameworkCore;
using hwapp.Models;

namespace hwapp.Persistence
{
    public class HelloDbContext : DbContext
    {
        public HelloDbContext(DbContextOptions<HelloDbContext> options) : base(options)
        {
            
        }

        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Feature> Features { get; set; }
    }
}