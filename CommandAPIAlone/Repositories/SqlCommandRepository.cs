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

        public async Task CreateCommandAsync(Command cmd)
        {
            if (cmd == null) 
            {
                throw new NullReferenceException(nameof(cmd));
            }
            await _context.Commands.AddAsync(cmd);
        }

        public void DeleteCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new NullReferenceException(nameof(cmd));
            }
            _context.Commands.Remove(cmd);
        }

        public async Task<IEnumerable<Command>> GetAllCommandsAsync()
        {
            return await _context.Commands.ToListAsync();
        }

        public async Task<Command?> GetCommandByIdAsync(int id)
        {
            return await _context.Commands.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task UpdateCommandAsync(Command cmd)
        {
           // entityframework elvégzi nekünk
        }
    }
}
