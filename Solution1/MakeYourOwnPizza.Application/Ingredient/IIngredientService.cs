using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakeYourOwnPizza.Application.Abstractions.Persistence;
namespace MakeYourOwnPizza.Application.Ingredient
{
    public interface IIngredientService
    {
        Task<List<GetIngredientDTO>> GetAllIngredientsAsync();
        Task<GetIngredientDTO?> GetIngredientByIdAsync(Guid id);
        Task<GetIngredientDTO> AddIngredientAsync(AddIngredientDTO ingredient);
        Task<bool> DeleteIngredientAsync(Guid id);
    }
}
