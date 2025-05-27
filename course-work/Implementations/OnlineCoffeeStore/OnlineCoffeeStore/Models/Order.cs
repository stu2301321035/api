using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace OnlineCoffeeStore.Models
{
   
    public enum OrderStatus
    {
        Pending = 0,
        Completed = 1,
        Cancelled = 2,
    }

   
    public enum Payment
    {
        Cash = 0 ,
        Card=1,
        Online = 2
    }

    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [StringLength(500)] // Optional validation for length of Notes
        public string ?Notes { get; set; }

        [Required]
        public Payment PaymentMethod { get; set; }

        [Required]
        public int UsersId { get; set; }

        [ForeignKey("UsersId")]

        [JsonIgnore]
        public virtual User? Users { get; set; }

        
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }

}
