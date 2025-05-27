using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineCoffeeStore.Models
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

        [Required]
        public string ?Name { get; set; }

        [MaxLength(255)]
        public String ?Ingredients { get; set; } 

        [Required]
        [Range(0.01, double.MaxValue)]  
        public double Price { get; set; }

        [Required]
        public CoffeeStatus Status { get; set; }  
        [Required]

        [ForeignKey("Category")]  

        public int? CategoryId { get; set; }
        public virtual Category? Category { get; set; }  

    }

    public class Category
    { 
        public int Id { get; set; }

        [Required]
        [StringLength(100)]  
        public string ?CategoryName { get; set; }

    }



}
