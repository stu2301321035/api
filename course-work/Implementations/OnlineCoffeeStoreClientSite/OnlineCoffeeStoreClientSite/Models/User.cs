using System.ComponentModel;

namespace OnlineCoffeeStoreClientSite.Models
{
    public enum AvailableRoles
    {
        Administrator = 0,
        Customer = 1,
        Store_Manager = 2
    }
    public class User
    {
        public int Id { get; set; }

        [DisplayName("First Name ")]
        public string ?FirstName { get; set; }

        [DisplayName("Last Name ")]
        public string ?LastName { get; set; }

        public string ?Email { get; set; }

        [DisplayName("Created On ")]

        public DateTime CreatedOn { get; set; }

        [DisplayName("Activation Status")]
        public bool IsActivated { get; set; }
        public string ?Password { get; set; }
        public int Role {  get; set; }
        public string RolesString => ((AvailableRoles)Role).ToString();

        public ICollection<Order> ?Orders { get; set; } // Може да се добави в клиентския модел


    }
}
