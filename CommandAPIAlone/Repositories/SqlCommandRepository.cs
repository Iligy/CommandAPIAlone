using CommandAPIAlone.Data;
using CommandAPIAlone.Interfaces;
using CommandAPIAlone.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandAPIAlone.Repositories
{
    public class SqlCommandRepository : ICommandRepository
    {
        private readonly MainDbContext _context;

        public SqlCommandRepository(MainDbContext context)
        {
            _context = context;
        }

        public Task CreateCommandAsync(Command cmd)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCommandAsync(Command cmd)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Command>> GetAllCommandsAsync()
        {
            return await _context.Commands.ToListAsync();
        }

        public async Task<Command> GetCommandByIdAsync(int id)
        {
            return await _context.Commands.FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateCommandAsync(Command cmd)
        {
            throw new NotImplementedException();
        }
    }
}
