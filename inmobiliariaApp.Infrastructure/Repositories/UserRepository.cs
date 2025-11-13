using inmobiliariaApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using inmobiliariaApp.Domain.Entities;
using inmobiliariaApp.Domain.Interfaces;

namespace inmobiliariaApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        // get user by id
        public async Task<User?> GetUserById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        // get all users
        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        // add new user
        public async Task<User> Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // update user
        public async Task Update(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);

            if (existingUser == null)
                throw new KeyNotFoundException("El usuario no existe");

            _context.Entry(existingUser).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();
        }

        // delete user
        public async Task Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
