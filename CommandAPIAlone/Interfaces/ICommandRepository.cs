using CommandAPIAlone.Models;

namespace CommandAPIAlone.Interfaces
{
    public interface ICommandRepository
    {
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<Command>> GetAllCommandsAsync();
        Task<Command> GetCommandByIdAsync(int id);
        Task CreateCommandAsync(Command cmd);
        Task UpdateCommandAsync(Command cmd);
        Task DeleteCommandAsync(Command cmd);
    }
}
