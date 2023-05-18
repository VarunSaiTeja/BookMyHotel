using BookMyHotel.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookMyHotel
{
    public class AppDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source=Demo.db", x => x.UseNetTopologySuite());
    }
}
