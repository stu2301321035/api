using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using OnlineCoffeeStoreClientSite.Helpers;
using OnlineCoffeeStoreClientSite.Models;
using OnlineCoffeeStoreClientSite.Services;
using System.Threading.Tasks;

namespace OnlineCoffeeStoreClientSite.Controllers
{
        public class CoffeeController : Controller
        {
            private readonly ApiClient _apiClient;

            public CoffeeController(ApiClient apiClient)
            {
                _apiClient = apiClient;
            }

            public async Task<IActionResult> Index(int pg = 1)
            {
                var token = HttpContext.Session.GetString("JwtToken");
                var role = JwtHelper.GetRoleFromToken(token);

                Console.WriteLine("Role from token" + role);

                ViewBag.Role = role;
                var categories = await _apiClient.GetAllCategories();

                ViewBag.CategoryId = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.CategoryName
                }).ToList();

                var coffees = await _apiClient.GetAllCoffees();
                const int pageSize = 6;
                if (pg < 1)
                {
                    pg = 1;
                }

                int rescCount = coffees.Count();
                var pager = new Pager(rescCount, pg, pageSize);
                int rescSkip = (pg - 1) * pageSize;
                var data = coffees.Skip(rescSkip).Take(pager.PageSize).ToList();
                this.ViewBag.Pager = pager;

                return View(data);
            }

            [HttpGet]
            public async Task<IActionResult> Create()
            {
                var categories = await _apiClient.GetAllCategories();

                ViewBag.CategoryId = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.CategoryName
                }).ToList();
                ViewBag.StatusOptions = Enum.GetValues(typeof(CoffeeStatus))
                        .Cast<CoffeeStatus>()
                        .Select(s => new SelectListItem
                        {
                            Value = ((int)s).ToString(),
                            Text = s.ToString()
                        }).ToList();

                  return View();
            }

            [HttpPost]
            public async Task<IActionResult> Create(Coffee coffee)
            {
                if (!ModelState.IsValid)
                {
                    foreach (var key in ModelState.Keys)
                    {
                        var state = ModelState[key];
                        foreach (var error in state.Errors)
                        {
                            Console.WriteLine($"Model error for {key}: {error.ErrorMessage}");
                        }
                    }
                    return View(coffee);
                }

                await _apiClient.CreateCoffee(coffee);
                return RedirectToAction("Index");
            }

            [HttpGet("Coffee/Edit/{id}")]
            public async Task<IActionResult> Edit(int id)
            {
                var coffee = await _apiClient.GetCoffeeById(id);
                if (coffee ==null)
                {
                    return NotFound();
                }
                var categories = await _apiClient.GetAllCategories();

                ViewBag.CategoryId = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.CategoryName
                }).ToList();
                ViewBag.StatusOptions = Enum.GetValues(typeof(CoffeeStatus))
                        .Cast<CoffeeStatus>()
                        .Select(s => new SelectListItem
                        {
                            Value = ((int)s).ToString(),
                            Text = s.ToString()
                        }).ToList();

            return View(coffee);

            }

            [HttpPost]
            public async Task<IActionResult> Edit(int id, Coffee coffee)
            {
                if (id != coffee.Id)
                    return BadRequest();

                var updatedCoffee = await _apiClient.UpdateCoffee(id, coffee);

                if (updatedCoffee == null)
                {
                    TempData["ErrorMessage"] = "Failed to update the coffee.";
                    return RedirectToAction("Edit", new { id = id });
                }

                return RedirectToAction("Index", "Coffee");
            }

            [HttpGet]
            public async Task<IActionResult> Delete(int id) { 
                var coffeeToBeDeleted = await _apiClient.DeleteCoffee(id);
                if (!coffeeToBeDeleted)
                {
                    TempData["ErrorMessage"] = "Failed to delete coffee item.";

                }

                return RedirectToAction("Index");
            }

            [HttpGet]
            public async Task<IActionResult> Search(string name, double price)
            {
                var coffees = await _apiClient.SearchByCriteriaCoffee(name, price);

                if (coffees == null || !coffees.Any())
                {
                    TempData["ErrorMessage"] = "No coffee items found matching the criteria.";
                    return RedirectToAction("Index"); // Redirect back to Index if no users found
                }

                // Return the filtered users back to the Index view
                return View("Index", coffees); // Ensure you are using the same Index view
            }

            [HttpGet]
            public async Task<IActionResult> FindByCategory(int id)
            {
           

                var coffees = await _apiClient.FindCoffeeByCategory(id);


                if (coffees == null || !coffees.Any())
                {
                    TempData["ErrorMessage"] = "No coffee items found matching the criteria.";
                    return RedirectToAction("Index"); // Redirect back to Index if no users found
                }

            Console.WriteLine($"coffee{coffees}");

                return View("Index", coffees); // Ensure you are using the same Index view

            }




         }


}


