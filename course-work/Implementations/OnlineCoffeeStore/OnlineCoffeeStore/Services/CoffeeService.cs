using Microsoft.EntityFrameworkCore;
using OnlineCoffeeStore.Data;
using OnlineCoffeeStore.Models;

namespace OnlineCoffeeStore.Services
{
    public class CoffeeService : ICoffeeService
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _context;

        public CoffeeService(ApplicationDbContext context, HttpClient client)
        {
            _context = context;
            _httpClient = client;
        }

      
        public async Task<Coffee> AddCoffee(Coffee coffee)
        {
            try
            {
                _context.Coffees.Add(coffee);
                await _context.SaveChangesAsync();
                return coffee;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add coffee to the database.", ex);
            }
        }

        public async Task<bool> DeleteCoffee(int id)
        {
            try
            {
                var coffeeToBeDeleted = await _context.Coffees.FirstOrDefaultAsync(c => c.Id == id);
                if (coffeeToBeDeleted == null)
                {
                    return false;
                }
                _context.Coffees.Remove(coffeeToBeDeleted);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete coffee with ID {id}.", ex);
            }
        }

        public async Task<IEnumerable<Coffee>> FindByCategory(int id)
        {
            try
            {
                var coffees = await _context.Coffees
                    .Include(c => c.Category)
                    .Where(c => c.CategoryId == id)
                    .ToListAsync();

                return coffees;
            }
            catch (Exception ex)
            {
                throw new Exception("There is no coffee with this category.", ex);
            }
        }


        public async Task<IEnumerable<Coffee>> GetAllCoffees()
        {
            try
            {
                return await _context.Coffees
                       .Include(c => c.Category) 
                       .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve the coffees.", ex);
            }
        }

        public async Task<Coffee?> GetCoffeeById(int id)
        {
            try
            {
                var coffee = await _context.Coffees.FirstOrDefaultAsync(c => c.Id == id);
                if (coffee == null)
                {
                    throw new KeyNotFoundException($"Coffee with ID {id} was not found.");
                }
                return coffee;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve coffee with ID {id}.", ex);
            }
        }

        public async Task<IEnumerable<Coffee>> SearchByCriteria(string name, double price)
        {
            IQueryable<Coffee> query = _context.Coffees;

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(u => u.Name.Contains(name));
            }

            if (price<0)
            {
                query = query.Where(u => u.Price.Equals(price));
            }

            return await query.ToListAsync();
        }


        public async Task<Coffee?> UpdateCoffee(int id, Coffee coffee)
        {
            try
            {
                var existingCoffee = await _context.Coffees.FirstOrDefaultAsync(c => c.Id == id);
                if (existingCoffee == null)
                {
                    return null;
                }

                // Update coffee properties
                existingCoffee.Name = coffee.Name;
                existingCoffee.CategoryId = coffee.CategoryId;
                existingCoffee.Category = coffee.Category;
                existingCoffee.Ingredients = coffee.Ingredients;
                existingCoffee.Price = coffee.Price;
                existingCoffee.Status = coffee.Status;

                await _context.SaveChangesAsync();
                return existingCoffee;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update coffee with ID {id}.", ex);
            }
        }

    }
}
