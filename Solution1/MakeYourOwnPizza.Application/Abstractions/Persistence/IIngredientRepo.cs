using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakeYourOwnPizza.Domain.Entities;
namespace MakeYourOwnPizza.Application.Abstractions.Persistence
{
    public interface IIngredientRepo
    {
        Task<MenuResponseDTO> GetAllIngredientsAsync();
        Task<GetIngredientDTO?> GetIngredientByIdAsync(Guid id);
        Task<GetIngredientDTO> AddIngredientAsync(Ingredients ingredient);
        Task<bool> DeleteIngredientAsync(Guid id);
    }
}
