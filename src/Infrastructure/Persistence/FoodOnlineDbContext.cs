using FoodOnline.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace FoodOnline.Infrastructure.Persistence
{
    public class FoodOnlineDbContext : DbContext
    {
        public DbSet<FoodOnlineUser> Users { get; set; }

        public FoodOnlineDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}