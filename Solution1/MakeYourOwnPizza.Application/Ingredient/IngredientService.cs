using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakeYourOwnPizza.Application.Abstractions.Persistence;
using MakeYourOwnPizza.Domain.Entities;
namespace MakeYourOwnPizza.Application.Ingredient
{
    internal class IngredientService : IIngredientService
    {
        private readonly IIngredientRepo _ingredientRepo;
        public IngredientService(IIngredientRepo ingredientRepo)
        {
            _ingredientRepo = ingredientRepo;
        }
        public async Task<List<GetIngredientDTO>> GetAllIngredientsAsync()
        {
            return await _ingredientRepo.GetAllIngredientsAsync();
        }
        public async Task<GetIngredientDTO?> GetIngredientByIdAsync(Guid id)
        {
            return await _ingredientRepo.GetIngredientByIdAsync(id);
        }
        public async Task<GetIngredientDTO> AddIngredientAsync(AddIngredientDTO ingredient)
        {
            var newIngredient = new Ingredients
            {
                name = ingredient.Name,
                price = ingredient.Price,
                colorHex = ingredient.colorHex,
                isAvailable = true,
                category = ingredient.category
            };
            return await _ingredientRepo.AddIngredientAsync(newIngredient);
        }
        public async Task<bool> DeleteIngredientAsync(Guid id)
        {
            return await _ingredientRepo.DeleteIngredientAsync(id);
        }
    }
}