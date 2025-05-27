using Microsoft.AspNetCore.Mvc;
using OnlineCoffeeStore.Models;
using OnlineCoffeeStore.Services;
using System.Diagnostics.Metrics;

namespace OnlineCoffeeStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoffeeController : ControllerBase
    {
        public readonly ICoffeeService _service;
        public CoffeeController(ICoffeeService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coffee>>> GetAllCoffeesAsync()
        {
            var allCoffees = await _service.GetAllCoffees();
            return Ok(allCoffees);

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Coffee>> GetCoffeeById(int id)
        {
            var coffee = await _service.GetCoffeeById(id);
            return Ok(coffee);
        }

        [HttpPost]
        public async Task<ActionResult<Coffee>> CreateCoffee([FromBody] Coffee coffee)
        {
            if (coffee == null)
            {
                return BadRequest();
            }

            var addedCoffee = await _service.AddCoffee(coffee); // Await the service call

            // Check if the service actually added the coffee.  The service could return null
            if (addedCoffee == null)
            {
                return StatusCode(500, "Failed to create coffee"); // Or another appropriate error code
            }

            return CreatedAtAction(nameof(GetCoffeeById), new { id = addedCoffee.Id }, addedCoffee);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteCoffee(int id)
        {
            var removedCoffee = await _service.DeleteCoffee(id);
            return Ok(removedCoffee);
        }

        [HttpPut("{id}")] // PUT api/Coffee/5
        public async Task<ActionResult<Coffee>> UpdateCoffee(int id, Coffee coffee)
        {
            var existingCoffee = await _service.UpdateCoffee(id, coffee);
            return Ok(existingCoffee);
        }

        [HttpGet("category/{id}")]
        public async Task<ActionResult<IEnumerable<Coffee>>> FindByCategory(int id)
        {
            var result = await _service.FindByCategory(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        [HttpGet("searchByCriteria")]
        public async Task<ActionResult<IEnumerable<User>>> SearchByCriteria([FromQuery] string name, [FromQuery] double price)
        {
            var searchFilter = await _service.SearchByCriteria(name, price);
            return Ok(searchFilter);
        }


    }
}
