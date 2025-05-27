namespace OnlineCoffeeStore.DTO.Order
{
    public class OrderItemDto
    {
        public int CoffeeId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
