using Microsoft.AspNetCore.Mvc;
using OnlineCoffeeStore.Models;
using OnlineCoffeeStore.Services;

namespace OnlineCoffeeStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        public readonly IUserService _service;
        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var allUsers = await _service.GetAllUsers();
            return Ok(allUsers);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUserById(int id)
        {
            var userById = await _service.GetUserById(id);
            return Ok(userById);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateCoffee([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest(); 
            }

            var createdUser = await _service.CreateUser(user); 
            if (createdUser == null)
            {
                return StatusCode(500, "Failed to create coffee"); 
            }

            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut("{id}")] // PUT api/Coffee/5
        public async Task<ActionResult<IEnumerable<User>>> UpdateCoffee(int id, User user)
        {
            var updatedUser = await _service.UpdateUser(id, user);
            return Ok(updatedUser);
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<User>>> DeleteUsers(int id)
        {
            var removedUser = await _service.DeleteUser(id);
            return Ok(removedUser);

        }

        [HttpGet("searchByCriteria")]
        public async Task<ActionResult<IEnumerable<User>>> SearchByCriteria([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var searchStrings = await _service.SearchByCriteria(firstName, lastName);
            return Ok(searchStrings);
        }

        

    }
}
