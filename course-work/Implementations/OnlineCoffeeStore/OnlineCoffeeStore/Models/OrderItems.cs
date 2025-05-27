using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace OnlineCoffeeStore.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int CoffeeId { get; set; }
        [JsonIgnore]
        public virtual Coffee? Coffee { get; set; }

        public int OrderId { get; set; }
        [JsonIgnore]
        public virtual Order? Order { get; set; }

        public int Quantity { get; set; }

        [Precision(10, 2)]
        public decimal UnitPrice { get; set; }

        [Precision(10, 2)]
        public decimal? DiscountPercentage { get; set; }


        [Precision(10, 2)]
        public decimal TotalPrice
        {
            get
            {
                decimal price = Quantity * UnitPrice;
                if (DiscountPercentage.HasValue)
                {
                    price *= (1 - (DiscountPercentage.Value / 100m)); // divide by 100 here
                }
                return price;
            }
        }

    }

}
