using BookMyHotel.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookMyHotel
{
    public class LiteDbContext : AppDbContext
    {
        public LiteDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().Ignore(c => c.Area);
            modelBuilder.Entity<Hotel>().Ignore(c => c.Location);
            modelBuilder.Entity<Order>().Ignore(c => c.DeliveryAddress);
            base.OnModelCreating(modelBuilder);
        }
    }
}
