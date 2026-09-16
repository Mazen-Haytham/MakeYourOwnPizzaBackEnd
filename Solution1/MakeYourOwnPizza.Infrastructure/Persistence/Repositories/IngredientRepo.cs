using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakeYourOwnPizza.Application.Abstractions.Persistence;
using MakeYourOwnPizza.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace MakeYourOwnPizza.Infrastructure.Persistence.Repositories
{
    public class IngredientRepo : IIngredientRepo
    {
        private readonly AppDbContext _context;
        public IngredientRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<GetIngredientDTO>> GetAllIngredientsAsync()
        {
            return await _context.Ingredients
                .Select(i => new GetIngredientDTO
                {
                    Id = i.Id.ToString(),
                    Name = i.name,
                    Price = i.price,
                    colorHex = i.colorHex,
                    isAvailable = i.isAvailable,
                    category = i.category.ToString()
                })
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<GetIngredientDTO?> GetIngredientByIdAsync(Guid id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient == null)
            {
                return null;
            }

            return new GetIngredientDTO
            {
                Id = ingredient.Id.ToString(),
                Name = ingredient.name,
                Price = ingredient.price,
                colorHex = ingredient.colorHex,
                isAvailable = ingredient.isAvailable,
                category = ingredient.category.ToString()
            };
        }
        public async Task<GetIngredientDTO> AddIngredientAsync( Ingredients ingredient)
        {
            
            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();
            return new GetIngredientDTO
            {
                Id = ingredient.Id.ToString(),
                Name = ingredient.name,
                Price = ingredient.price,
                colorHex = ingredient.colorHex,
                isAvailable = ingredient.isAvailable,
                category = ingredient.category.ToString()
            };
        }
        public async Task<bool> DeleteIngredientAsync(Guid id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient == null)
            {
                return false;
            }
            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
