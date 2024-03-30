using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace fitee_backend.Data
{
    public class AppDbContext : DbContext
    {
        //protected readonly IConfiguration Configuration;

        //public AppDbContext(IConfiguration configuration)
        //{
           // Configuration = configuration;
        //}

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Running> Runnings {  get; set; }
    }
}
