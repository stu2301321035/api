using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineCoffeeStoreClientSite.DTOs
{
    public class LoginRequest
    {
        [DisplayName("Email: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string Email { get; set; }

        [DisplayName("Passsword: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string Password { get; set; }
    }
    
}
