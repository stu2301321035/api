using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace OnlineCoffeeStoreClientSite.Models
{
    public enum CoffeeStatus
    {
        Available = 0,
        Pending = 1,
        Unavailable = 2
    }

    public class Coffee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public double Price { get; set; }
        public CoffeeStatus Status { get; set; }
        public string StatusText => ((CoffeeStatus)Status).ToString();  // Автоматично превръща в string

        public int  CategoryId { get; set; }

        public Category? Category { get; set; }

    }

    public class Category
    {
        public int Id { get; set; }
        [DisplayName("Category ")]
        public string CategoryName { get; set; }
    }

}
