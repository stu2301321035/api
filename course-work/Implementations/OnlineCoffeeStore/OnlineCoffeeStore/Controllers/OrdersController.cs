using Microsoft.AspNetCore.Mvc;
using Nest;
using OnlineCoffeeStore.DTO;
using OnlineCoffeeStore.Models;
using OnlineCoffeeStore.Services;
using System.Threading.Tasks;

namespace OnlineCoffeeStore.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        public readonly IOrderService _service;
        public readonly IUserService _serviceUser;
        public OrdersController(IOrderService service, IUserService serviceUser)
        {
            _service = service;
            _serviceUser = serviceUser;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrdersAsync()
        {
            var allOrders = await _service.GetAllOrders();
            return Ok(allOrders);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderByIdAsync(int id)
        {
            var order = await _service.GetOrderById(id);
            return Ok(order);
        }

        [HttpGet("ByUser/{userId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByUserIdAsync(int userId) 
        {
            var orders = await _service.GetOrdersByUserId(userId);
            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _serviceUser.GetUserById(order.UsersId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            await _service.AddOrder(order);

            var locationUri = new Uri($"/api/Orders/{order.Id}", UriKind.Relative);
            return Created(locationUri, order);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteOrder(int id)
        {
            var success = await _service.DeleteOrder(id);
            if (!success)
            {
                return NotFound($"Order with ID {id} not found.");
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> UpdateOrder(int id, Order order) {

            var existingOrder = await _service.UpdateOrder(id, order);
            return Ok(existingOrder);
        }

        [HttpGet("searchByCriteria")]
        public async Task<ActionResult<IEnumerable<Order>>> SearchByCriteria([FromQuery] DateTime orderDate, [FromQuery] OrderStatus status)
        {
            var searchStrings = await _service.SearchByCriteria(orderDate, status);
            return Ok(searchStrings);
        }

        [HttpDelete("RemoveItem")]
        public async Task<ActionResult<bool>> RemoveItemFromOrder(int orderId, int orderItemId)
        {
            var removeItem = await _service.RemoveItemFromOrder(orderId, orderItemId);
            return Ok(removeItem);

        }

        [HttpPost("AddItem")]
        public async Task<ActionResult<bool>> AddItemToOrder(int orderId, OrderItem item)
        {
            var addItem = await _service.AddItemToOrder(orderId, item);
            return Ok(addItem);
        }

       
    }
}
