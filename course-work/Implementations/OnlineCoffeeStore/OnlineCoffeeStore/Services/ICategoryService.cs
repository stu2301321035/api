using OnlineCoffeeStore.Models;

namespace OnlineCoffeeStore.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category?> GetCategoryById(int id);
        Task<Category> AddCategory(Category category);

        Task<Category?> UpdateCategory(int id, Category category);
        Task<bool> DeleteCategory(int id);
    }
}
