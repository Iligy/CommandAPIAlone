using CommandAPIAlone.Data;
using CommandAPIAlone.Interfaces;
using CommandAPIAlone.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace CommandAPIAlone.Repositories
{
    public class UserVerificationRepository : IUserVerificationRepository
    {

        private readonly MainDbContext _context;
        public UserVerificationRepository(MainDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreateUniqueRandomTokenAsync()
        {
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

            var user = await GetUserByPasswordResetTokenAsync(token);

            if (user != null) 
            {
                await CreateUniqueRandomTokenAsync();
            }

            return token;
        }

        public async Task CreateUserAsync(User user)
        {
            if (user == null) 
            {
                throw new ArgumentNullException(nameof(user));
            }

            await _context.Users.AddAsync(user);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByPasswordResetTokenAsync(string passwordResetToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == passwordResetToken);
        }

        public async Task<User?> GetUserByVerificationTokenAsync(string verificationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.VerificationToken == verificationToken);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}
