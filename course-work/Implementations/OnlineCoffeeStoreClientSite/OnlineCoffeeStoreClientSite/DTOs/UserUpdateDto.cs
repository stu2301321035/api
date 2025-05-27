namespace OnlineCoffeeStoreClientSite.DTOs
{
    public class UserUpdateDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActivated { get; set; }
        public string Role { get; set; }
    }

}
