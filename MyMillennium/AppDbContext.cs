using Microsoft.EntityFrameworkCore;
using MyMillenniumApi.Data;

namespace MyMillenniumApi
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<ArtItem> ArtItems => Set<ArtItem>();
    }
}
