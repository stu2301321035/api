using OnlineCoffeeStore.Models;
using System.Diagnostics.Metrics;

namespace OnlineCoffeeStore.Services
{
    public interface ICoffeeService
    {
        Task<IEnumerable<Coffee>> GetAllCoffees(); 
        Task<Coffee?> GetCoffeeById(int id);
        Task<Coffee> AddCoffee(Coffee coffee); 

        Task<Coffee?> UpdateCoffee(int id, Coffee coffee);
        Task<bool> DeleteCoffee(int id);
        Task<IEnumerable<Coffee>> FindByCategory(int id);

        Task<IEnumerable<Coffee>> SearchByCriteria(string name, double price);



    }
}
