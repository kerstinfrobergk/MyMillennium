using Microsoft.EntityFrameworkCore;
using MyMillennium.Data.Entities;

namespace MyMillennium.Data.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<ArtItem> ArtItems => Set<ArtItem>();
    }
}
