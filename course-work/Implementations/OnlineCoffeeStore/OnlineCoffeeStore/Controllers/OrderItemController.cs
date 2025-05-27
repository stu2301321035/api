using Microsoft.AspNetCore.Mvc;
using OnlineCoffeeStore.Models;
using OnlineCoffeeStore.Services;

namespace OnlineCoffeeStore.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemController : Controller
    {
        public readonly IOrderItemService _service;

        public OrderItemController(IOrderItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetAllOrderItemsAsync()
        {
            var allOrderItems = await _service.GetAllOrderItems();
            return Ok(allOrderItems);
        }


        [HttpGet("ByOrder/{orderId}")]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            var orderItems = await _service.GetOrderIdsFromItems(orderId);
            return Ok(orderItems);
        }

        [HttpGet("ByCoffee/{coffeeId}")]

        public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItemsByCoffeeIdAsync(int coffeeId)
        {
            var orderItems = await _service.GetCoffeeIdsFromItems(coffeeId);
            return Ok(orderItems);
        }

        [HttpGet("calculation")]
        public async Task<ActionResult<decimal>> CalculateTotalPrice(int orderId)
        {
            var calculation = await _service.CalculateTotalPrice(orderId);
            return Ok(calculation);
        }

    }
}
