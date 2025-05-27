using Microsoft.AspNetCore.Mvc;
using OnlineCoffeeStoreClientSite.Models;
using OnlineCoffeeStoreClientSite.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineCoffeeStoreClientSite.Helpers;

namespace OnlineCoffeeStoreClientSite.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApiClient _apiClient;

        public OrderController(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task<IActionResult> Index(int pg = 1)
        {
            var users = await _apiClient.GetAllUsers();

            ViewBag.UserOptions = users.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = $"{u.FirstName} {u.LastName}"
            }).ToList();

            var coffees = await _apiClient.GetAllCoffees();

            ViewBag.Coffees = new SelectList(coffees, "Id", "Name");

            const int pageSize = 6;
            if (pg < 1)
            {
                pg = 1;
            }

            var allOrders = await _apiClient.GetAllOrders();

            int rescCount = users.Count();
            var pager = new Pager(rescCount, pg, pageSize);
            int rescSkip = (pg - 1) * pageSize;
            var data = allOrders.Skip(rescSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var users = await _apiClient.GetAllUsers();
            var coffees = await _apiClient.GetAllCoffees();
            ViewBag.UserOptions = users.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = $"{u.FirstName} {u.LastName}"
            }).ToList();

            ViewBag.Coffees = new SelectList(coffees, "Id", "Name");

            ViewBag.StatusOptions = Enum.GetValues(typeof(OrderStatus))
                      .Cast<OrderStatus>()
                      .Select(s => new SelectListItem
                      {
                          Value = ((int)s).ToString(),
                          Text = s.ToString()
                      }).ToList();

            ViewBag.PaymentOptions = Enum.GetValues(typeof(Payment))
                      .Cast<Payment>()
                      .Select(s => new SelectListItem
                      {
                          Value = ((int)s).ToString(),
                          Text = s.ToString()
                      }).ToList();

            var order = new Order();
            order.OrderItems.Add(new OrderItem());

            return View(order);

        }


        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            if (!ModelState.IsValid)
            {
                // Repopulate dropdowns
                var users = await _apiClient.GetAllUsers();
                var coffees = await _apiClient.GetAllCoffees();

                ViewBag.UserOptions = users.Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = $"{u.FirstName} {u.LastName}"
                }).ToList();

                ViewBag.Coffees = new SelectList(coffees, "Id", "Name");

                ViewBag.StatusOptions = Enum.GetValues(typeof(OrderStatus))
                    .Cast<OrderStatus>()
                    .Select(s => new SelectListItem
                    {
                        Value = ((int)s).ToString(),
                        Text = s.ToString()
                    }).ToList();

                ViewBag.PaymentOptions = Enum.GetValues(typeof(Payment))
                    .Cast<Payment>()
                    .Select(s => new SelectListItem
                    {
                        Value = ((int)s).ToString(),
                        Text = s.ToString()
                    }).ToList();

                return View(order);
            }

            await _apiClient.CreateOrder(order);
            return RedirectToAction("Index");
        }


        [HttpGet("Order/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _apiClient.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            var users = await _apiClient.GetAllUsers();
            var coffees = await _apiClient.GetAllCoffees();
            ViewBag.UserOptions = users.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = $"{u.FirstName} {u.LastName}"
            }).ToList();

            ViewBag.Coffees = new SelectList(coffees, "Id", "Name");

            ViewBag.StatusOptions = Enum.GetValues(typeof(OrderStatus))
                      .Cast<OrderStatus>()
                      .Select(s => new SelectListItem
                      {
                          Value = ((int)s).ToString(),
                          Text = s.ToString()
                      }).ToList();

            ViewBag.PaymentOptions = Enum.GetValues(typeof(Payment))
                      .Cast<Payment>()
                      .Select(s => new SelectListItem
                      {
                          Value = ((int)s).ToString(),
                          Text = s.ToString()
                      }).ToList();


            return View(order);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,Order order)
        {
           

            // И зпращане на заявка за актуализиране на потребителя
            var updatedOrder = await _apiClient.UpdateOrder(id, order);
            if (updatedOrder == null)
            {
                TempData["ErrorMessage"] = "Failed to update the user.";
                return RedirectToAction("Edit", new { id = id });
            }

            return RedirectToAction("Index", "Order");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var orderToBeDeleted = await _apiClient.DeleteOrder(id);
            if (!orderToBeDeleted)
            {
                TempData["ErrorMessage"] = "Failed to delete order .";

            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> SearchByUser(int userId)
        {
            var users = await _apiClient.GetAllUsers();
            ViewBag.UserOptions = users.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = $"{u.FirstName} {u.LastName}"
            }).ToList();

            var ordersByUserId = await _apiClient.GetOrdersByUserId(userId);

            if (ordersByUserId == null || !ordersByUserId.Any())
            {
                TempData["ErrorMessage"] = "Failed to get order by user id .";

            }
            Console.WriteLine($"user{userId}");


            return View("Index", ordersByUserId);
        }

        [HttpGet]
        public async Task<IActionResult> SearchByDateAndStatus(DateTime orderDate, OrderStatus status)
        {
            var search = await _apiClient.SearchByCriteriaOrder(orderDate, status);

            if (search == null || !search.Any())
            {
                TempData["ErrorMessage"] = "Failed to get order by user id .";

            }

            Console.WriteLine($"Received orderDate: {orderDate}, status: {status}");

            return View("Index", search);
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToOrder(int orderId, OrderItem orderItem)
        {

            var coffees = await _apiClient.GetAllCoffees();
            ViewBag.Coffees = new SelectList(coffees, "Id", "Name");
            
            var addItem = await _apiClient.AddItemToOrder(orderId, orderItem);

            if (!addItem)
            {
                TempData["ErrorMessage"] = "Failed to add item to the order.";

            }


            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> RemoveItemFromOrder(int orderId, int orderItemId)
        {
            var removeItem = await _apiClient.RemoveItemFromOrder(orderId, orderItemId);

            if (!removeItem)
            {
                TempData["ErrorMessage"] = "Failed to remove item to the order.";

            }

            return RedirectToAction("Index");
        }




    }
}
