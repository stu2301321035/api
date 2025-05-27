using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Nest;
using OnlineCoffeeStore.Data;
using OnlineCoffeeStore.Models;
using System.Linq;

namespace OnlineCoffeeStore.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context, HttpClient client)
        {
            _context = context;
            _httpClient = client;
        }

        public async Task<bool> AddItemToOrder(int orderId, OrderItem item)
        {
            try
            {

                var order = await _context.Orders
                    .Include(x=>x.OrderItems)
                    .FirstOrDefaultAsync(x=>x.Id == orderId);

                if (order == null) {
                    return false;
                }

                order.OrderItems.Add(item);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex) {
                throw new Exception("Failed to add orderItem to the order", ex);
            }
        }

        public async Task<Order> AddOrder(Order order)
        {
            try {
                _context.Orders
                    .Add(order);
                await _context.SaveChangesAsync();
                return order;
            }
            catch (Exception ex) {
                throw new Exception("Failed to add order to the database", ex);
            }
        }

        public async Task<bool> DeleteOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return false;

            // Remove related order items first
            _context.OrderItems.RemoveRange(order.OrderItems);

            // Now remove the order
            _context.Orders.Remove(order);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            try
            {
                return  await _context.Orders.Include(x=>x.OrderItems).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception("Failed to retrieve the orders.", ex);

            }
        }

        public async Task<Order?> GetOrderById(int id)
        {
            try
            {
                var order = await _context.Orders
                       .Include(o => o.OrderItems)
                       .FirstOrDefaultAsync(o => o.Id == id); 
                if (order == null)
                {
                    throw new KeyNotFoundException($"Order with ID {id} was not found.");
                }
                return order;
            }
            catch (Exception ex) {
                throw new Exception($"Failed to retrieve order with ID {id}.", ex);
            }

        }

        public async Task<IEnumerable<Order>> GetOrdersByUserId(int userId)
        {
            try
            {
                var orders = await _context.Orders
                    .Include(x => x.OrderItems)
                    .Include(x=>x.Users)
                    .Where(x=>x.UsersId == userId)
                    .ToListAsync();

                return orders;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve orders with ID {userId}.", ex);
            }
        }

        public async Task<bool> RemoveItemFromOrder(int orderId, int orderItemId)
        {
            try {

                //Retrieve the order with its items
                var order = await _context.Orders
                    .Include(x => x.OrderItems)
                    .FirstOrDefaultAsync(x => x.Id == orderId);

                if (order == null)
                {
                    return false; // Order not found
                }

                // Find the specific order item by its Id
                var orderItem = order.OrderItems.FirstOrDefault(x => x.Id==orderItemId);

                if (orderItem == null)
                {
                    return false; // OrderItem not found
                }

                // remove the item from the OrderItems collection
                order.OrderItems.Remove(orderItem);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception e) {
                throw new Exception($"Failed to delete orderItem from the specific order with ID {orderItemId}.", e);

            }
        }

        public async Task<IEnumerable<Order>> SearchByCriteria(DateTime orderDate, OrderStatus status)
        {
            try
            {
                return await _context.Orders
                    .Where(x => x.OrderDate.Date == orderDate.Date && x.Status == status)
                    .ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Failed to retrieve the cruteria.", ex);

            }

        }

        public async Task<Order?> UpdateOrder(int id, Order order)
        {
            try
            {
                var existingOrder = await _context.Orders
                    .Include(x => x.OrderItems)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (existingOrder == null)
                {
                    return null;
                }

                // Основни полета на поръчката
                existingOrder.OrderDate = order.OrderDate;
                existingOrder.Status = order.Status;
                existingOrder.Notes = order.Notes;
                existingOrder.PaymentMethod = order.PaymentMethod;
                existingOrder.UsersId = order.UsersId;
                existingOrder.OrderItems = order.OrderItems;

                await _context.SaveChangesAsync();
                return existingOrder;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update order with ID {id}.", ex);
            }
        }
    }
}
