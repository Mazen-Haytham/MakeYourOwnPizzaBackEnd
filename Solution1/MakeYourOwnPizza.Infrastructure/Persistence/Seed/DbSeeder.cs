using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MakeYourOwnPizza.Domain.Entities;
using MakeYourOwnPizza.Domain.Enums;

namespace MakeYourOwnPizza.Infrastructure.Persistence.Seed
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext context, IPasswordHasher<User> passwordHasher)
        {
            await context.Database.EnsureCreatedAsync();

            // 1. Seed Ingredients
            if (!await context.Ingredients.AnyAsync())
            {
                var ingredients = new List<Ingredients>
                {
                    new Ingredients { Id = Guid.NewGuid(), name = "Mozzarella", category = IngredientCategory.Cheese, price = 1.50m, colorHex = "#ffffe0", stock = 100, isAvailable = true, imageUrl = "" },
                    new Ingredients { Id = Guid.NewGuid(), name = "Cheddar", category = IngredientCategory.Cheese, price = 1.75m, colorHex = "#ffb02e", stock = 100, isAvailable = true, imageUrl = "" },
                    new Ingredients { Id = Guid.NewGuid(), name = "Pepperoni", category = IngredientCategory.Meats, price = 2.00m, colorHex = "#bb2c22", stock = 100, isAvailable = true, imageUrl = "" },
                    new Ingredients { Id = Guid.NewGuid(), name = "Sausage", category = IngredientCategory.Meats, price = 2.25m, colorHex = "#7a3f3a", stock = 100, isAvailable = true, imageUrl = "" },
                    new Ingredients { Id = Guid.NewGuid(), name = "Mushrooms", category = IngredientCategory.Veggies, price = 1.00m, colorHex = "#d8ccb8", stock = 100, isAvailable = true, imageUrl = "" },
                    new Ingredients { Id = Guid.NewGuid(), name = "Onions", category = IngredientCategory.Veggies, price = 0.75m, colorHex = "#e3c2eb", stock = 100, isAvailable = true, imageUrl = "" },
                    new Ingredients { Id = Guid.NewGuid(), name = "Bell Peppers", category = IngredientCategory.Veggies, price = 1.25m, colorHex = "#327f31", stock = 100, isAvailable = true, imageUrl = "" },
                    new Ingredients { Id = Guid.NewGuid(), name = "Black Olives", category = IngredientCategory.Veggies, price = 1.50m, colorHex = "#3a3c3e", stock = 100, isAvailable = true, imageUrl = "" }
                };

                await context.Ingredients.AddRangeAsync(ingredients);
                await context.SaveChangesAsync();
            }

            // 2. Seed Pizzas
            if (!await context.Pizza.AnyAsync())
            {
                var pizzas = new List<Pizza>
                {
                    new Pizza { Id = Guid.NewGuid(), name = "Margherita", price = 8.99m },
                    new Pizza { Id = Guid.NewGuid(), name = "Pepperoni Feast", price = 12.99m },
                    new Pizza { Id = Guid.NewGuid(), name = "Veggie Supreme", price = 14.99m }
                };

                await context.Pizza.AddRangeAsync(pizzas);
                await context.SaveChangesAsync();
            }

            // 3. Seed Users
            if (!await context.User.AnyAsync())
            {
                var users = new List<User>
                {
                    new User { Id = Guid.NewGuid(), firstName = "John", lastName = "Customer", email = "customer@test.com", phone = "1234567890", role = Role.Customer, isActive = true },
                    new User { Id = Guid.NewGuid(), firstName = "Alice", lastName = "Manager", email = "manager@test.com", phone = "1234567891", role = Role.Manager, isActive = true },
                    new User { Id = Guid.NewGuid(), firstName = "Bob", lastName = "Driver", email = "delivery@test.com", phone = "1234567892", role = Role.Delivery, isActive = true }
                };

                foreach (var user in users)
                {
                    user.password = passwordHasher.HashPassword(user, "Password123!");
                }

                await context.User.AddRangeAsync(users);
                await context.SaveChangesAsync();

                // Create Cart for Customer
                var customer = users.First(u => u.role == Role.Customer);
                var cart = new Cart { Id = Guid.NewGuid(), UserId = customer.Id };
                await context.Cart.AddAsync(cart);

                // Create Driver profile for Delivery
                var delivery = users.First(u => u.role == Role.Delivery);
                var driver = new Driver { Id = Guid.NewGuid(), UserId = delivery.Id, Zone = "Central", Status = DriverStatus.Available };
                await context.Driver.AddAsync(driver);

                await context.SaveChangesAsync();
            }
        }
    }
}
