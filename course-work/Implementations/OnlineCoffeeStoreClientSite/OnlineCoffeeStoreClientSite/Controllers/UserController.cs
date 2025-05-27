using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineCoffeeStoreClientSite.Helpers;
using OnlineCoffeeStoreClientSite.Models;
using OnlineCoffeeStoreClientSite.Services;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;

namespace OnlineCoffeeStoreClientSite.Controllers
{
    public class UserController : Controller
    {
        private readonly ApiClient _apiClient;

        public UserController(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IActionResult> Index(int pg = 1)
        {
            var users = await _apiClient.GetAllUsers();

            const int pageSize = 6;
            if (pg < 1) 
            { 
                pg = 1;
            }

            int rescCount = users.Count();
            var pager = new Pager(rescCount,pg,pageSize);
            int rescSkip = (pg - 1) * pageSize;
            var data = users.Skip(rescSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
           

            return View(data);
        }

        public IActionResult Create()
        {
            ViewBag.RoleList = new SelectList( Enum.GetValues(typeof(AvailableRoles))
                .Cast<AvailableRoles>()
                .Select(r => new { Id = (int)r, Name = r.ToString() }), "Id", "Name" );
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var createdUser = await _apiClient.CreateUser(user);
            if (createdUser == null)
            {

                ModelState.AddModelError("", "Error creating user.");
                return View(user);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Home");
            }

            int? loggedUserId = JwtHelper.GetUserIdFromToken(token);
            if (loggedUserId == null)
            {
                TempData["ErrorMessage"] = "You are not authorized to edit this user's information.";

            }

            var user = await _apiClient.GetUserById(id);
            if (user == null)
                return NotFound();

            if (user.Id != loggedUserId)
            {

                TempData["ErrorMessage"] = "You are not authorized to edit this user's information.";
                return RedirectToAction("Index", "User");
            }


            var userToBeDeleted = await _apiClient.DeleteUser(id);
            if (!userToBeDeleted)
            {
                TempData["ErrorMessage"] = "Failed to delete user.";
            }
            return RedirectToAction("Index");
        }

        [HttpGet("User/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token)) 
            {
                return RedirectToAction("Login", "Home");
            }

            int? loggedUserId = JwtHelper.GetUserIdFromToken(token);
            if (loggedUserId == null) 
            {
                TempData["ErrorMessage"] = "You are not authorized to edit this user's information.";

            }

            var user = await _apiClient.GetUserById(id);
            if (user == null)
                return NotFound();

            if (user.Id != loggedUserId)
            {

                TempData["ErrorMessage"] = "You are not authorized to edit this user's information.";
                return RedirectToAction("Index", "User");
            }
            ViewBag.RoleList = Enum.GetValues(typeof(AvailableRoles))
                        .Cast<AvailableRoles>()
                        .Select(s => new SelectListItem
                        {
                            Value = ((int)s).ToString(),
                            Text = s.ToString()
                        }).ToList();
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, User user)
        {
            var token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Home");
            }

            int? loggedUserId = JwtHelper.GetUserIdFromToken(token);
            if (loggedUserId == null || user.Id != loggedUserId)
            {
                TempData["ErrorMessage"] = "You are not authorized to edit this user's information.";
                return RedirectToAction("Index", "User");
            }

            // Проверка на задължителни полета
            if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName) || string.IsNullOrEmpty(user.Email))
            {
                TempData["ErrorMessage"] = "All fields are required.";
                return RedirectToAction("Edit", new { id = id });
            }

            // Изпращане на заявка за актуализиране на потребителя
            var updatedUser = await _apiClient.UpdateUser(id, user);
            if (updatedUser == null)
            {
                TempData["ErrorMessage"] = "Failed to update the user.";
                return RedirectToAction("Edit", new { id = id });
            }

            return RedirectToAction("Index", "User");
        }


        [HttpGet]
        public async Task<IActionResult> Search(string firstName, string lastName)
        {
            // Make the API call to search by criteria
            var users = await _apiClient.SearchByCriteria(firstName, lastName);

            if (users == null || !users.Any())
            {
                TempData["ErrorMessage"] = "No users found matching the criteria.";
                return RedirectToAction("Index"); // Redirect back to Index if no users found
            }

            // Return the filtered users back to the Index view
            return View("Index", users); // Ensure you are using the same Index view
        }





    }
}
