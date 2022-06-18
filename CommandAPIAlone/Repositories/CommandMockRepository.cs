using CommandAPIAlone.Interfaces;
using CommandAPIAlone.Models;

namespace CommandAPIAlone.Repositories
{
    public class CommandMockRepository : ICommandRepository
    {
        public Task CreateCommandAsync(Command cmd)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCommandAsync(Command cmd)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Command>> GetAllCommandsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Command> GetCommandByIdAsync(int id)
        {
            throw new NotImplementedException();
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
