using Microsoft.EntityFrameworkCore;
using OnlineCoffeeStore.Data;
using OnlineCoffeeStore.Models;

namespace OnlineCoffeeStore.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context, HttpClient client)
        {
            _context = context;
            _httpClient = client;
        }
        public async Task<Category> AddCategory(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add coffee category to the database.", ex);
            }
        }

        public async Task<bool> DeleteCategory(int id)
        {

            try
            {
                var categoryToBeDeleted = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (categoryToBeDeleted == null)
                {
                    return false;
                }
                _context.Categories.Remove(categoryToBeDeleted);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete coffee category with ID {id}.", ex);
            }
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            try
            {
                return await _context.Categories.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve the coffee categories.", ex);
            }
        }

        public async Task<Category?> GetCategoryById(int id)
        {
            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (category == null)
                {
                    throw new KeyNotFoundException($"Coffee category with ID {id} was not found.");
                }
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve coffee category with ID {id}.", ex);
            }
        }

        public async Task<Category?> UpdateCategory(int id, Category category)
        {
            try
            {
                var existingCoffeeCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (existingCoffeeCategory == null)
                {
                    return null;
                }

                // Update coffee properties
                existingCoffeeCategory.CategoryName = category.CategoryName;

                await _context.SaveChangesAsync();
                return existingCoffeeCategory;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update coffee with ID {id}.", ex);
            }
        }
    }
}
