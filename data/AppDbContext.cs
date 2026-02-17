using Microsoft.EntityFrameworkCore;
namespace WebApplication6.data
{
    public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {


        public DbSet<State> State { get; set; }
        public DbSet<City> City { get; set; }
    }
}
