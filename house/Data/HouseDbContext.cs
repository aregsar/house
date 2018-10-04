using Microsoft.EntityFrameworkCore;

namespace house.Data
{
    public class HouseDbContext : DbContext
    {
        public HouseDbContext(DbContextOptions<HouseDbContext> options)
            : base(options)
        {
        }

        public DbSet<house.Data.House> House { get; set; }
    }
}
