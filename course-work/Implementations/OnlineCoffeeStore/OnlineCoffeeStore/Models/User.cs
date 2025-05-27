using System.ComponentModel.DataAnnotations;

namespace OnlineCoffeeStore.Models
{
    public enum AvailableRoles
    {
        Administrator = 0,
        Customer = 1,
        Store_Manager = 2,
    }

    public class User
    {
        [Key]  
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string ?FirstName { get; set; }

        [Required]
        [StringLength(20)]
        public string ?LastName { get; set; }

        [EmailAddress]
        [StringLength(30)]
        public string ?Email { get; set; }

        
        public DateTime CreatedOn { get; set; }

        [Required]
        public bool IsActivated { get; set; }

        [Required]
        [StringLength(20)]  
        public string ?Password { get; set; }

        [Required]
        public AvailableRoles ?Role{ get; set; }


        public  virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
