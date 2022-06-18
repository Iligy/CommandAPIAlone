using CommandAPIAlone.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandAPIAlone.Data
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {

        }

        public DbSet<Command> Commands { get; set; }
    }
}
