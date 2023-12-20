using FoodOrderApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodsApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FoodsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFood(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest("Invalid id");
                }

                var food = await _context.Foods.FindAsync(id);

                if (food == null)
                {
                    return NotFound("Food not found");
                }

                return Ok(food);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Exception in GetFood: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }


}
