using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Cors;

namespace OnlineCoffeeStoreClientSite.Models
{
    public enum OrderStatus
    {
        Pending = 0,
        Completed = 1,
        Cancelled = 2,
    }

    public enum Payment
    {
        Cash = 0,
        Card = 1,
        Online = 2
    }

    public class Order
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
        public int Id { get; set; }

        [DisplayName("Order Date")]
        public DateTime OrderDate { get; set; }

        public OrderStatus Status { get; set; }

        public string? Notes { get; set; }

        [DisplayName("Payment Method")]
        public Payment PaymentMethod { get; set; }

        [DisplayName("User's Names")]
        public int UsersId { get; set; }

        public  User? Users { get; set; }

        public List<OrderItem> ?OrderItems { get; set; }
    }
}
