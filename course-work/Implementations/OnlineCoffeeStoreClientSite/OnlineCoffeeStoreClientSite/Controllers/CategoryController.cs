using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OnlineCoffeeStoreClientSite.Helpers;
using OnlineCoffeeStoreClientSite.Models;
using OnlineCoffeeStoreClientSite.Services;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace OnlineCoffeeStoreClientSite.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApiClient _apiClient;

        public CategoryController(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task<IActionResult> Index()
        {

            var allCategories = await _apiClient.GetAllCategories();
            return View(allCategories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            var createdCategory = await _apiClient.CreateCategory(category);
            if (createdCategory == null)
            {
                ModelState.AddModelError("", "Error creating user.");
                return View(category);
            }

            return RedirectToAction("Index");
        }

        [HttpGet("Category/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _apiClient.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id!= category.Id)
                return BadRequest();

            var updatedCategory = await _apiClient.UpdateCategory(id, category);

            if (updatedCategory == null)
            {
                TempData["ErrorMessage"] = "Failed to update the category.";
                return RedirectToAction("Edit", new { id = id });
            }

            return RedirectToAction("Index", "Category");
        }

        [HttpGet]

        public async Task<IActionResult> Delete(int id) 
        {
            var categoryToBeDeleted = await _apiClient.DeleteCategory(id);
            if (!categoryToBeDeleted)
            {
                TempData["ErrorMessage"] = "Failed to delete coffee item.";

            }

            return RedirectToAction("Index");
        }


    }
}
