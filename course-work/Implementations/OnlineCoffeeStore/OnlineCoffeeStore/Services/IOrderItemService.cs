using OnlineCoffeeStore.Models;

namespace OnlineCoffeeStore.Services
{
    public interface IOrderItemService
    {
        Task <IEnumerable<OrderItem>> GetAllOrderItems();
        Task<IEnumerable<OrderItem>> GetOrderIdsFromItems(int orderId);

        Task<IEnumerable<OrderItem>> GetCoffeeIdsFromItems(int coffeeId);
        Task<decimal> CalculateTotalPrice(int orderId);
    } 
    

}
