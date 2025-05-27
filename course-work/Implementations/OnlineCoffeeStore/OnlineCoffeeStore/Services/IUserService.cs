using OnlineCoffeeStore.Models;

namespace OnlineCoffeeStore.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User?> GetUserById(int id);
        Task<IEnumerable<User>> SearchByCriteria(string firstName, string lastName);
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(int id, User user);
        Task<bool> DeleteUser(int id);
    }
}
