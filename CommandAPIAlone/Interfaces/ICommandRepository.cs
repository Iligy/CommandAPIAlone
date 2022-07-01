using CommandAPIAlone.Models;

namespace CommandAPIAlone.Interfaces
{
    public interface ICommandRepository
    {
        Task<IEnumerable<Command>> GetAllCommandsAsync();
        Task<Command?> GetCommandByIdAsync(int id);
        Task CreateCommandAsync(Command cmd);
        Task UpdateCommandAsync(Command cmd);
        void DeleteCommand(Command cmd);
        Task<bool> SaveChangesAsync();
    }
}
