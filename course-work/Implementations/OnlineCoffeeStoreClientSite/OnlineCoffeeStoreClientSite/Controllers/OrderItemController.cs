using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineCoffeeStoreClientSite.Models;
using OnlineCoffeeStoreClientSite.Services;

namespace OnlineCoffeeStoreClientSite.Controllers
{
    public class OrderItemController : Controller
    {
        private readonly ApiClient _apiClient;

        public OrderItemController(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IActionResult> Index(int pg = 1)
        {
            var orders = await _apiClient.GetAllOrders();
            var coffees = await _apiClient.GetAllCoffees();

            ViewBag.CoffeeNames = coffees.Select(u=> new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Name
            }).ToList();

            ViewBag.OrderId = orders.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = $"{u.Id}"
            }).ToList();

            var orderItems = await _apiClient.GetAllOrderItems();
            const int pageSize = 6;
            if (pg < 1)
            {
                pg = 1;
            }

            int rescCount = orderItems.Count();
            var pager = new Pager(rescCount, pg, pageSize);
            int rescSkip = (pg - 1) * pageSize;
            var data = orderItems.Skip(rescSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;

            return View(data);
        }


        [HttpGet]
        public async Task<IActionResult> SearchByOrderId(int orderId)
        {
            var itemsByOrderId = await _apiClient.GetOrderIdsFromItems(orderId);

            if (itemsByOrderId == null || !itemsByOrderId.Any())
            {
                TempData["ErrorMessage"] = "Failed to get order by order id .";

            }
            Console.WriteLine($"orderId {orderId}");


            return View("Index", itemsByOrderId);
        }
        [HttpGet]
        public async Task<IActionResult> SearchByCoffeeId(int coffeeId)
        {
            var coffees = await _apiClient.GetAllCoffees();

            ViewBag.CoffeeNames = coffees.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Name
            }).ToList();


            var itemsByCoffeeId = await _apiClient.GetCoffeeIdsFromItems(coffeeId);

            if (itemsByCoffeeId == null || !itemsByCoffeeId.Any())
            {
                TempData["ErrorMessage"] = "Failed to get order by coffee id .";

            }
            Console.WriteLine($"coffeeId {coffeeId}");


            return View("Index", itemsByCoffeeId);
        }
        [HttpGet]
        public async Task<IActionResult> CalculateTotalPrice(int orderId)
        {
            var calculation = await _apiClient.CalculateTotalPrice(orderId);

            if (calculation == null)
            {
                TempData["ErrorMessage"] = "Failed to calculate by order id.";
            }
            else
            {
                ViewBag.CalculatedTotal = calculation;
            }

            var coffees = await _apiClient.GetAllCoffees();

            ViewBag.CoffeeNames = coffees.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Name
            }).ToList();

            var orderItems = await _apiClient.GetOrderIdsFromItems(orderId); 
           

            return View("Index", orderItems);
        }


    }
}
