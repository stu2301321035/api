using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using OnlineCoffeeStoreClientSite.Models;
using System.Text.Json.Serialization;

namespace OnlineCoffeeStoreClientSite.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int CoffeeId { get; set; }
      
        public virtual  Coffee? Coffee { get; set; }

        public int OrderId { get; set; }
        
        public  Order? Order { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
        public decimal? DiscountPercentage { get; set; }
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
