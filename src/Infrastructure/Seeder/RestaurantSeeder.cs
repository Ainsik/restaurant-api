﻿using Core.Entities;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seeder;
public class RestaurantSeeder
{
    private readonly RestaurantDbContext _dbContext;

    public RestaurantSeeder(RestaurantDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Seed()
    {
        if (_dbContext.Database.CanConnect())
        {
            if (!_dbContext.Restaurants.Any())
            {
                var restaurants = GetRestaurants();
                _dbContext.Restaurants.AddRange(restaurants);
                _dbContext.SaveChanges();
            }
        }
    }

    private IEnumerable<Restaurant> GetRestaurants()
    {
        var restaurants = new List<Restaurant>()
        {
            new Restaurant()
            {
                Name = "KFC",
                Description =
                    "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                Category = "Fast Food",
                HasDelivery = true,
                ContactEmail = "contact@kfc.com",
                ContactNumber = "123123123",
                Address = new Address()
                {
                    City = "Kraków",
                    Street = "Długa 5",
                    PostalCode = "30-001"
                },
                Dishes = new List<Dish>()
                {
                    new Dish()
                    {
                        Name = "Nashville Hot Chicken",
                        Description = "Nashville Hot Chicken is a fiery and flavorful Southern dish featuring crispy fried chicken drenched in a spicy cayenne pepper-infused glaze.",
                        Price = 10.30M,
                    },

                    new Dish()
                    {
                        Name = "Chicken Nuggets",
                        Description = "Chicken nuggets are bite-sized, golden-fried pieces of tender chicken, perfect for dipping and snacking.",
                        Price = 5.30M,
                    },
                }
            },
            new Restaurant()
            {
                Name = "McDonald Szewska",
                Description =
                    "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                Category = "Fast Food",
                HasDelivery = true,
                ContactEmail = "contact@mcdonald.com",
                ContactNumber = "321321321",
                Address = new Address()
                {
                    City = "Kraków",
                    Street = "Szewska 2",
                    PostalCode = "30-001"
                }
            }
        };
        return restaurants;
    }

}