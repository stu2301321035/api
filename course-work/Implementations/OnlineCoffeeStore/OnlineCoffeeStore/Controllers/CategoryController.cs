using Microsoft.AspNetCore.Mvc;
using OnlineCoffeeStore.Models;
using OnlineCoffeeStore.Services;

namespace OnlineCoffeeStore.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        public readonly ICategoryService _service;
        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategoryAsync()
        {
            var allCategories = await _service.GetAllCategories();
            return Ok(allCategories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Coffee>> GetCoffeeById(int id)
        {
            var category = await _service.GetCategoryById(id);
            return Ok(category);
        }


        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategoryAsync([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest();
            }

            var addedCategory = await _service.AddCategory(category); // Await the service call

            // Check if the service actually added the coffee.  The service could return null
            if (addedCategory == null)
            {
                return StatusCode(500, "Failed to create coffee"); // Or another appropriate error code
            }

            return CreatedAtAction(nameof(GetCoffeeById), new { id = addedCategory.Id }, addedCategory);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteCategoryAsync(int id)
        {
            var removedCategory = await _service.DeleteCategory(id);
            return Ok(removedCategory);
        }

        [HttpPut("{id}")] // PUT api/Coffee/5
        public async Task<ActionResult<Category>> UpdateCategory(int id, Category category)
        {
            var existingCategory = await _service.UpdateCategory(id, category);
            return Ok(existingCategory);
        }

    }
}
