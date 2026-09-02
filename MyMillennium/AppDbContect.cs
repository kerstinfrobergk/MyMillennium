using Microsoft.EntityFrameworkCore;
using MyMillenniumApi.Data;

namespace MyMillenniumApi
{
    public class AppDbContect : DbContext
    {
        public AppDbContect(DbContextOptions<AppDbContect> options) : base(options)
        { }

        public DbSet<ArtItem> ArtItems => Set<ArtItem>();
    }
}
