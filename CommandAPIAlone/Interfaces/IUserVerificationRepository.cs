using CommandAPIAlone.Models;

namespace CommandAPIAlone.Interfaces
{
    public interface IUserVerificationRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByVerificationTokenAsync(string verificationToken);
        Task<User?> GetUserByPasswordResetTokenAsync(string passwordResetToken);
        Task CreateUserAsync(User user);
        Task<string> CreateUniqueRandomTokenAsync();
        Task<bool> SaveChangesAsync();
    }
}
