using Microsoft.EntityFrameworkCore;
using log_and_reg.Models;

namespace log_and_reg.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
    }
}
