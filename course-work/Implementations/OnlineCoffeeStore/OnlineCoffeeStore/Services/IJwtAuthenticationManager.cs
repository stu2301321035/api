namespace OnlineCoffeeStore.Services
{
    public interface IJwtAuthenticationManager
    {
        string? Authenticate(string email, string password);
    }
}
