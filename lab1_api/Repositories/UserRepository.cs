using lab1_api.Models.Domain;
using lab1_api.Data;
using lab1_api.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab1_api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject the DbContext
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all users
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // Get user by ID
        public async Task<User?> GetUserByIdAsync(int id)
        {
            // Using the FindAsync method which returns null if not found
            return await _context.Users.FindAsync(id);
        }

        // Create a new user
        public async Task<User?> CreateUserAsync(User user)
        {
            if (user == null)
            {
                return null; // Ensure the user is not null before saving
            }

            // Add the user to the database
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // Update an existing user
        public async Task<User?> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;  // Trả về đối tượng User sau khi cập nhật
        }

        // Delete a user by ID
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false; // Nếu không tìm thấy người dùng, trả về false
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true; // Nếu xóa thành công, trả về true
        }
        // Find a user by email
        public async Task<User?> FindUserByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email != null && u.Email.Equals(email));
        }

    }
}
