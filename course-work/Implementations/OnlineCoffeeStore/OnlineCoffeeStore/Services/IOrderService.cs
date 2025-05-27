using OnlineCoffeeStore.Models;

namespace OnlineCoffeeStore.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order?> GetOrderById(int id);
        Task<Order> AddOrder(Order order);

        Task<Order?> UpdateOrder(int id, Order order);
        Task<bool> DeleteOrder(int id);

        Task<IEnumerable<Order>> SearchByCriteria(DateTime orderDate, OrderStatus status);

        Task<IEnumerable<Order>> GetOrdersByUserId(int userId);
        Task<bool> AddItemToOrder(int orderId, OrderItem item);
        Task<bool> RemoveItemFromOrder(int orderId, int orderItemId);


    }
}
