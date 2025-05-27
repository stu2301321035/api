using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Json; 
using System.Text.Json.Serialization;
using OnlineCoffeeStoreClientSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.Data;
using System.Threading.Tasks;
using OnlineCoffeeStoreClientSite.DTOs;
using LoginRequest = OnlineCoffeeStoreClientSite.DTOs.LoginRequest;


namespace OnlineCoffeeStoreClientSite.Services
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("https://localhost:5002");

        }

        public async Task<List<Coffee>> GetAllCoffees()
        {
            var response = await _httpClient.GetAsync("https://localhost:5002/api/coffee");

            if (response.IsSuccessStatusCode)
            {
                var coffees = await response.Content.ReadFromJsonAsync<List<Coffee>>();
                return coffees;
            }

            return new List<Coffee>(); 
        } 
        public async Task<List<Order>> GetAllOrders()
        {
            var response = await _httpClient.GetAsync("https://localhost:5002/api/Orders");

            if (response.IsSuccessStatusCode)
            {
                var allOrders = await response.Content.ReadFromJsonAsync<List<Order>>();
                return allOrders;
            }

            return new List<Order>(); 
        } 
        public async Task<List<Category>> GetAllCategories()
        {
            var response = await _httpClient.GetAsync("https://localhost:5002/api/category");

            if (response.IsSuccessStatusCode)
            {
                var categories = await response.Content.ReadFromJsonAsync<List<Category>>();
                return categories;
            }

            return new List<Category>(); 
        }

        public async Task<Coffee> GetCoffeeById(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Coffee/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var coffeeId = await response.Content.ReadFromJsonAsync<Coffee>();
            return coffeeId;
        }
        public async Task<Order> GetOrderById(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Orders/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var orderId = await response.Content.ReadFromJsonAsync<Order>();
            return orderId;
        }
          public async Task<Category> GetCategoryById(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Category/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var categoryId = await response.Content.ReadFromJsonAsync<Category>();
            return categoryId;
        }

        public async Task<Coffee> CreateCoffee(Coffee coffee)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Coffee", coffee);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var createdCoffee = await response.Content.ReadFromJsonAsync<Coffee>();
            return createdCoffee;
        } 
        public async Task<Order> CreateOrder(Order order)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Orders", order);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var createdOrder= await response.Content.ReadFromJsonAsync<Order>();
            return createdOrder;
        }
        public async Task<Category> CreateCategory(Category category)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Category", category);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var createdCategory = await response.Content.ReadFromJsonAsync<Category>();
            return createdCategory;
        }
        public async Task<List<User>> GetAllUsers()
        {
            var response = await _httpClient.GetAsync("https://localhost:5002/api/Users");

            if (response.IsSuccessStatusCode)
            {
                var users = await response.Content.ReadFromJsonAsync<List<User>>();
                return users;
            }

            return new List<User>(); 
         }

        public async Task<User> GetUserById(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Users/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var user = await response.Content.ReadFromJsonAsync<User>();
            return user;
        }

        public async Task<User> CreateUser(User user)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Users", user);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var createdUser = await response.Content.ReadFromJsonAsync<User>();
            return createdUser;
        }
        public async Task<bool> DeleteUser(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Users/{id}");
            return response.IsSuccessStatusCode;
        }
         public async Task<bool> DeleteCategory(int id)
         {
            var response = await _httpClient.DeleteAsync($"/api/Category/{id}");
            return response.IsSuccessStatusCode;
         }
         public async Task<bool> DeleteCoffee(int id)
         {
            var response = await _httpClient.DeleteAsync($"/api/Coffee/{id}");
            return response.IsSuccessStatusCode;
         } 
        public async Task<bool> DeleteOrder(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Orders/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<User> UpdateUser(int id, User user)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Console.WriteLine("Sending JSON to API:");
            Console.WriteLine(json);

            var response = await _httpClient.PutAsync($"/api/Users/{id}", content);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<User>();
           
        }
        public async Task<Coffee> UpdateCoffee(int id, Coffee coffee)
        {
            // Създаване на JsonContent директно от обекта (без нужда от JsonConvert.SerializeObject)
            var content = JsonContent.Create(coffee);

            Console.WriteLine("Sending JSON to API:");
            Console.WriteLine(await content.ReadAsStringAsync());  // За дебъгване

            var response = await _httpClient.PutAsync($"/api/Coffee/{id}", content);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Failed to update coffee. Status: " + response.StatusCode);
                return null;
            }

            return await response.Content.ReadFromJsonAsync<Coffee>();
        }
        public async Task<Order> UpdateOrder(int id,Order order)
        {
            // Създаване на JsonContent директно от обекта (без нужда от JsonConvert.SerializeObject)
            var content = JsonContent.Create(order);

            Console.WriteLine("Sending JSON to API:");
            Console.WriteLine(await content.ReadAsStringAsync());  // За дебъгване

            var response = await _httpClient.PutAsync($"/api/Orders/{id}", content);
            Console.WriteLine($"Content {content}");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Failed to update order. Status: " + response.StatusCode);
                return null;
            }

            return await response.Content.ReadFromJsonAsync<Order>();
        }
        public async Task<Category> UpdateCategory(int id, Category category)
        {
            // Създаване на JsonContent директно от обекта (без нужда от JsonConvert.SerializeObject)
            var content = JsonContent.Create(category);

            Console.WriteLine("Sending JSON to API:");
            Console.WriteLine(await content.ReadAsStringAsync());  // За дебъгване

            var response = await _httpClient.PutAsync($"/api/Category/{id}", content);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Failed to update coffee. Status: " + response.StatusCode);
                return null;
            }

            return await response.Content.ReadFromJsonAsync<Category>();
        }

        public async Task<IEnumerable<User>> SearchByCriteria([FromQuery] string firstName, [FromQuery] string lastName) 
        {
         
            var response = await _httpClient.GetAsync($"/api/Users/searchByCriteria?firstName={firstName}&lastName={lastName}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var user = await response.Content.ReadFromJsonAsync<IEnumerable<User>>();
            return user;
        }  
        public async Task<IEnumerable<Coffee>> SearchByCriteriaCoffee([FromQuery] string name, [FromQuery] double price) 
        {
         
            var response = await _httpClient.GetAsync($"/api/Coffee/searchByCriteria?name={name}&price={price}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var coffees = await response.Content.ReadFromJsonAsync<IEnumerable<Coffee>>();
            return coffees;
        }
        public async Task<IEnumerable<Order>> SearchByCriteriaOrder([FromQuery] DateTime orderDate, [FromQuery] OrderStatus status)
        {
            var formattedDate = orderDate.ToString("yyyy-MM-dd"); // ISO формат
            var response = await _httpClient.GetAsync($"/api/Orders/searchByCriteria?orderDate={formattedDate}&status={status}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var orders = await response.Content.ReadFromJsonAsync<IEnumerable<Order>>();
            return orders;
        }

        public async Task<IEnumerable<Coffee>> FindCoffeeByCategory(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Coffee/category/{id}");
            if (!response.IsSuccessStatusCode) 
            {
                return null;
            }

            var coffeeByCategory = await response.Content.ReadFromJsonAsync<IEnumerable<Coffee>>();
            return coffeeByCategory;
        }
        public async Task<IEnumerable<Order>> GetOrdersByUserId(int userId)
        {
            var response = await _httpClient.GetAsync($"/api/Orders/ByUser/{userId}");
            if (!response.IsSuccessStatusCode) 
            {
                return null;
            }

            var ordersByUserId = await response.Content.ReadFromJsonAsync<IEnumerable<Order>>();
            return ordersByUserId;
        }

        public async Task<String> Login([FromBody] LoginRequest request)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/Authorization/login", content);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responsesString = await response.Content.ReadAsStringAsync();
            var tokenObj = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthenticationResponse>(responsesString);

            return tokenObj.Token;
        }

        public async Task<IEnumerable<OrderItem>> GetAllOrderItems()
        {
            var response = await _httpClient.GetAsync($"/api/OrderItem");

            if (response.IsSuccessStatusCode)
            {
                var allOrderItems = await response.Content.ReadFromJsonAsync<List<OrderItem>>();
                return allOrderItems;
            }

            return new List<OrderItem>();
        }
        public async Task<IEnumerable<OrderItem>> GetOrderIdsFromItems(int orderId)
        {
            var response = await _httpClient.GetAsync($"/api/OrderItem/ByOrder/{orderId}");

            if (response.IsSuccessStatusCode)
            {
                var orderItemsByOrderIds = await response.Content.ReadFromJsonAsync<List<OrderItem>>();
                return orderItemsByOrderIds;
            }


            return new List<OrderItem>();
        }
        public async Task<IEnumerable<OrderItem>> GetCoffeeIdsFromItems(int coffeeId)
        {
            var response = await _httpClient.GetAsync($"/api/OrderItem/ByCoffee/{coffeeId}");

            if (response.IsSuccessStatusCode)
            {
                var orderItemsByCoffeeIds = await response.Content.ReadFromJsonAsync<List<OrderItem>>();
                return orderItemsByCoffeeIds;
            }

            return new List<OrderItem>();
        }

        public async Task<bool> AddItemToOrder(int orderId, OrderItem orderItem)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/Orders/AddItem?orderId={orderId}", orderItem);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode} | Details: {error}");
            }
            Console.WriteLine($"DISCOUNT {orderItem.DiscountPercentage}");
            Console.WriteLine($"PRICE {orderItem.UnitPrice}");
            Console.WriteLine($"COFFEE {orderItem.CoffeeId}");

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> RemoveItemFromOrder(int orderId, int orderItemId)
        {
            var response = await _httpClient.DeleteAsync($"/api/Orders/RemoveItem?orderId={orderId}&orderItemId={orderItemId}");
            Console.WriteLine($"orderId{orderId}, order item{orderItemId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<decimal> CalculateTotalPrice(int orderId) {
            var response = await _httpClient.GetAsync($"/api/OrderItem/calculation?orderId={orderId}");
            response.EnsureSuccessStatusCode(); // Optional but good for error handling
            return await response.Content.ReadFromJsonAsync<decimal>();

        }
    }
}
