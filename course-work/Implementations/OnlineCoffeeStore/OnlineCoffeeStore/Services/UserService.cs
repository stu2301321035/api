using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Nest;
using OnlineCoffeeStore.Data;
using OnlineCoffeeStore.Models;

namespace OnlineCoffeeStore.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context, HttpClient client)
        {
            _context = context;
            _httpClient = client;
        }
        public async Task<User> CreateUser(User user)
        {
            try {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user;

            }
            catch (Exception ex) {
                throw new Exception("Failed to add user to the database.", ex);

            }

        }

        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                var userToBeDeleted = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (userToBeDeleted == null)
                {
                    return false;
                }

                _context.Users.Remove(userToBeDeleted);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) {
                throw new Exception($"Failed to delete user with ID {id}.", ex);
            }
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            try {
                var allUsers =  await _context.Users.Include(x=>x.Orders).ToListAsync();
                return allUsers;
            }
            catch (Exception ex) {
                throw new Exception("Failed to retrieve the users.", ex);

            }
        }

        public async Task<User?> GetUserById(int id)
        {
            try {
                var userById = await _context.Users.FirstOrDefaultAsync(x=>x.Id==id);
                if (userById == null)
                {
                    throw new KeyNotFoundException($"Coffee with ID {id} was not found.");
                }
                return userById;
            }
            catch (Exception ex) {
                throw new Exception($"Failed to retrieve user with ID {id}.", ex);
            }
        }

        public async Task<IEnumerable<User>> SearchByCriteria(string firstName, string lastName)
        {
            try
            {
                IQueryable<User> query = _context.Users;

                if (!string.IsNullOrEmpty(firstName))
                {
                    query = query.Where(x => x.FirstName.Contains(firstName));
                }
                if (!string.IsNullOrEmpty(lastName))
                {
                    query = query.Where(x => x.LastName.Contains(lastName));
                }

                return await query.ToListAsync(); 
            }
            catch (Exception ex) {
                throw new Exception($"There is no user mathcing the criteria.", ex);

            }

        }

        public async Task<User> UpdateUser(int id, User user)
        {
            try
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (existingUser == null)
                {
                    return null;
                }

                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.IsActivated = user.IsActivated;
                existingUser.Password = user.Password;


                await _context.SaveChangesAsync();
                return existingUser;
            }
            catch (Exception ex) {

                throw new Exception($"Failed to update user with ID {id}.", ex);

            }

        
        }
    }
}
