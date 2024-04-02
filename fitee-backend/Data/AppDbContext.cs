using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace fitee_backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Running> Runnings {  get; set; }
    }
}
