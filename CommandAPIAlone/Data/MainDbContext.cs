using CommandAPIAlone.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandAPIAlone.Data
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {

        }

        public DbSet<Command> Commands => Set<Command>();
        public DbSet<PracticeModel> PracticeModels => Set<PracticeModel>();
        public DbSet<User> Users => Set<User>();
    }
}
