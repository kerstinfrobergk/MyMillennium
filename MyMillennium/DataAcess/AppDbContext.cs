using Microsoft.EntityFrameworkCore;
using MyMillenniumApi.Entities;

namespace MyMillenniumApi.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<ArtItem> ArtItems => Set<ArtItem>();
    }
}
