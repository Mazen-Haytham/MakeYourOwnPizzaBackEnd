using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MakeYourOwnPizza.Application.Ingredient;
using MakeYourOwnPizza.Application.Abstractions.Persistence;
using Microsoft.AspNetCore.Authorization;
namespace MakeYourOwnPizza.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;
        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }
        [Authorize(Roles = "Manager")]
        [HttpPost("add")]
        public async Task<IActionResult> AddIngredient([FromBody] AddIngredientDTO request)
        {
            var result = await _ingredientService.AddIngredientAsync(request);
            
            return CreatedAtAction(nameof(GetIngredientById), new { id = result.Id }, result);
        }
        [Authorize(Roles = "Manager")]
        [HttpGet("{id}")]
        public async Task<ActionResult<GetIngredientDTO>> GetIngredientById(Guid id)
        {
            var ingredient = await _ingredientService.GetIngredientByIdAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }
            return Ok(ingredient);
        }
        [Authorize(Roles = "Manager")]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<GetIngredientDTO>>> GetAllIngredients()
        {
            var ingredients = await _ingredientService.GetAllIngredientsAsync();
            return Ok(ingredients);
        }
        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(Guid id)
        {
            var result = await _ingredientService.DeleteIngredientAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
