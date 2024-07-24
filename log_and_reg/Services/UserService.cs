using log_and_reg.Data;
using log_and_reg.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace log_and_reg.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Register(User user, string password)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.password = Convert.ToBase64String(passwordHash);
            user.salt = Convert.ToBase64String(passwordSalt);

            _context.User.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _context.User.SingleOrDefaultAsync(u => u.user_name == username);
            if (user == null || !VerifyPasswordHash(password, Convert.FromBase64String(user.password), Convert.FromBase64String(user.salt)))
                return null;

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }
    }
}
