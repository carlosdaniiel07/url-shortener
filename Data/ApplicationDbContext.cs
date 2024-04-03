using Microsoft.EntityFrameworkCore;
using UrlShortener.Data.Mappings;
using UrlShortener.Entities;

namespace UrlShortener.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Url> Urls { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UrlMapping());
        }
    }
}
