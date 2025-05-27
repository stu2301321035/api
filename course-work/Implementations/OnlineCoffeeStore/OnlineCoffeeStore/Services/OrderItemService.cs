using Microsoft.EntityFrameworkCore;
using Nest;
using OnlineCoffeeStore.Data;
using OnlineCoffeeStore.Migrations;
using OnlineCoffeeStore.Models;
using System.Threading.Tasks;

namespace OnlineCoffeeStore.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _context;

        public OrderItemService(ApplicationDbContext context, HttpClient client)
        {
            _context = context;
            _httpClient = client;
        }

        public async Task<IEnumerable<OrderItem>> GetCoffeeIdsFromItems(int coffeeId)
        {
            try
            {
                var orderItems = await _context.OrderItems
                    .Include(x => x.Coffee)
                    .Where(x => x.CoffeeId == coffeeId)
                    .ToListAsync();

                return orderItems;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve order items with orderId {coffeeId}.", ex);

            }
        }

        public async Task<IEnumerable<OrderItem>> GetAllOrderItems()
        {
            try
            {
                return await _context.OrderItems.Include(x=>x.Coffee).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve all order items ", ex);

            }
        }

        public async Task<IEnumerable<OrderItem>> GetOrderIdsFromItems(int orderId)
        {
            try
            {
                var orderItems = await _context.OrderItems
                    .Include(x=>x.Order)
                    .Where(x=>x.OrderId == orderId)
                    .ToListAsync();

                return orderItems;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve order items with orderId {orderId}.", ex);

            }
        }

        public async Task<decimal> CalculateTotalPrice(int orderId)
        {
            try
            {
                var orderItems = await _context.OrderItems
                    .Include(x=>x.Coffee)
                    .Include(x => x.Order)
                    .Where(x => x.OrderId == orderId)
                    .ToListAsync();
                
                decimal total = orderItems.Sum(x => x.TotalPrice);

                return total;


            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
