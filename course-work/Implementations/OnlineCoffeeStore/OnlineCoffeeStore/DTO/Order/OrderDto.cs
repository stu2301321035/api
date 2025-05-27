using OnlineCoffeeStore.Models;

namespace OnlineCoffeeStore.DTO.Order
{
    public class OrderDto
    {
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public string? Notes { get; set; }
        public Payment PaymentMethod { get; set; }
        public int UsersId { get; set; }
        public List<OrderItemDto>? OrderItems { get; set; } = new List<OrderItemDto>();
    }
}
