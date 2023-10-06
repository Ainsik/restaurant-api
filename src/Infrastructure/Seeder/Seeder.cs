﻿using Core.Entities;
using Infrastructure.DbContext;

namespace Infrastructure.Seeder;

public class Seeder
{
    private readonly RestaurantDbContext _dbContext;

    public Seeder(RestaurantDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Seed()
    {
        if (_dbContext.Database.CanConnect())
        {
            if (!_dbContext.Roles.Any())
            {
                var roles = GetRoles();
                _dbContext.Roles.AddRange(roles);
                _dbContext.SaveChanges();
            }

            if (!_dbContext.Users.Any())
            {
                var users = GetUsers();
                _dbContext.Users.AddRange(users);
                _dbContext.SaveChanges();
            }

            if (!_dbContext.Restaurants.Any())
            {
                var restaurants = GetRestaurants();
                _dbContext.Restaurants.AddRange(restaurants);
                _dbContext.SaveChanges();
            }
        }
    }

    private static IEnumerable<Restaurant> GetRestaurants()
    {
        var restaurants = new List<Restaurant>
        {
            new()
            {
                Name = "KFC",
                Description =
                    "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                Category = "Fast Food",
                HasDelivery = true,
                ContactEmail = "contact@kfc.com",
                ContactNumber = "123123123",
                Address = new Address
                {
                    City = "Kraków",
                    Street = "Długa 5",
                    PostalCode = "30-001"
                },
                Dishes = new List<Dish>
                {
                    new()
                    {
                        Name = "Nashville Hot Chicken",
                        Description =
                            "Nashville Hot Chicken is a fiery and flavorful Southern dish featuring crispy fried chicken drenched in a spicy cayenne pepper-infused glaze.",
                        Price = 10.30M
                    },

                    new()
                    {
                        Name = "Chicken Nuggets",
                        Description =
                            "Chicken nuggets are bite-sized, golden-fried pieces of tender chicken, perfect for dipping and snacking.",
                        Price = 5.30M
                    }
                },
                CreatedById = 2
            },
            new()
            {
                Name = "McDonald Szewska",
                Description =
                    "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                Category = "Fast Food",
                HasDelivery = true,
                ContactEmail = "contact1@mcdonald.com",
                ContactNumber = "321321322",
                Address = new Address
                {
                    City = "Kraków",
                    Street = "Szewska 2",
                    PostalCode = "30-001"
                },
                CreatedById = 2
            },
            new()
            {
                Name = "McDonald Szewska",
                Description =
                    "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                Category = "Fast Food",
                HasDelivery = true,
                ContactEmail = "contact2@mcdonald.com",
                ContactNumber = "321321331",
                Address = new Address
                {
                    City = "Kraków",
                    Street = "Szewska 2",
                    PostalCode = "30-001"
                },
                CreatedById = 2
            },
            new()
            {
                Name = "McDonald Szewska",
                Description =
                    "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                Category = "Fast Food",
                HasDelivery = true,
                ContactEmail = "contat@mcdonald.com",
                ContactNumber = "311321321",
                Address = new Address
                {
                    City = "Kraków",
                    Street = "Szewska 2",
                    PostalCode = "30-001"
                },
                CreatedById = 2
            },
            new()
            {
                Name = "McDonald Szewska",
                Description =
                    "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                Category = "Fast Food",
                HasDelivery = true,
                ContactEmail = "conta7ct@mcdonald.com",
                ContactNumber = "321721321",
                Address = new Address
                {
                    City = "Kraków",
                    Street = "Szewska 2",
                    PostalCode = "30-001"
                },
                CreatedById = 2
            },
            new()
            {
                Name = "McDonald Szewska",
                Description =
                    "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                Category = "Fast Food",
                HasDelivery = true,
                ContactEmail = "contac4t@mcdonald.com",
                ContactNumber = "321341321",
                Address = new Address
                {
                    City = "Kraków",
                    Street = "Szewska 2",
                    PostalCode = "30-001"
                },
                CreatedById = 2
            },
            new()
            {
                Name = "McDonald Szewska",
                Description =
                    "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                Category = "Fast Food",
                HasDelivery = true,
                ContactEmail = "contac12t@mcdonald.com",
                ContactNumber = "320321321",
                Address = new Address
                {
                    City = "Kraków",
                    Street = "Szewska 2",
                    PostalCode = "30-001"
                },
                CreatedById = 2
            },
            new()
            {
                Name = "McDonald Szewska",
                Description =
                    "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                Category = "Fast Food",
                HasDelivery = true,
                ContactEmail = "contac5t@mcdonald.com",
                ContactNumber = "321381321",
                Address = new Address
                {
                    City = "Kraków",
                    Street = "Szewska 2",
                    PostalCode = "30-001"
                },
                CreatedById = 2
            },
            new()
            {
                Name = "McDonald Szewska",
                Description =
                    "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                Category = "Fast Food",
                HasDelivery = true,
                ContactEmail = "contact0@mcdonald.com",
                ContactNumber = "321321301",
                Address = new Address
                {
                    City = "Kraków",
                    Street = "Szewska 2",
                    PostalCode = "30-001"
                },
                CreatedById = 2
            },
            new()
            {
                Name = "McDonald Szewska",
                Description =
                    "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                Category = "Fast Food",
                HasDelivery = true,
                ContactEmail = "contact00@mcdonald.com",
                ContactNumber = "321320021",
                Address = new Address
                {
                    City = "Kraków",
                    Street = "Szewska 2",
                    PostalCode = "30-001"
                },
                CreatedById = 2
            },
            new()
            {
                Name = "McDonald Szewska",
                Description =
                    "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                Category = "Fast Food",
                HasDelivery = true,
                ContactEmail = "contac68t@mcdonald.com",
                ContactNumber = "321321328",
                Address = new Address
                {
                    City = "Kraków",
                    Street = "Szewska 2",
                    PostalCode = "30-001"
                },
                CreatedById = 2
            },
            new()
            {
                Name = "McDonald Szewska",
                Description =
                    "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                Category = "Fast Food",
                HasDelivery = true,
                ContactEmail = "con5tact@mcdonald.com",
                ContactNumber = "325321321",
                Address = new Address
                {
                    City = "Kraków",
                    Street = "Szewska 2",
                    PostalCode = "30-001"
                },
                CreatedById = 2
            }
        };
        return restaurants;
    }

    private static IEnumerable<Role> GetRoles()
    {
        var roles = new List<Role>
        {
            new()
            {
                Name = "User"
            },
            new()
            {
                Name = "Owner"
            },
            new()
            {
                Name = "Admin"
            }
        };

        return roles;
    }

    private static IEnumerable<User> GetUsers()
    {
        var users = new List<User>
        {
            new()
            {
                Email = "admin@gmail.com",
                FirstName = "Admin",
                LastName = "Admin",
                PasswordHash =
                    "AQAAAAEAACcQAAAAEPuCnIw7Bt+Gu/5v8qb4c7bv8YwdSF+G1TT+XJE9ItfXL3QsmFQvzUJ2Gs54EWXzaQ==", // Admin01!
                RoleId = 3
            },
            new()
            {
                Email = "manager@gmail.com",
                FirstName = "Manager",
                LastName = "Manager",
                PasswordHash =
                    "AQAAAAEAACcQAAAAEL1e8K19IqLWTvStxzflYU5LjYjQRXW/DwjbGdsQA0n2sgu9NcZWcWwKeDvOVb4Q5Q==", // Manager1!
                RoleId = 2
            },
            new()
            {
                Email = "user@gmail.com",
                FirstName = "User",
                LastName = "User",
                PasswordHash =
                    "AQAAAAEAACcQAAAAELvo4KMDKWD9WU6YPeZv71KM2WV/jJdc0nOvV9yqdzUmLHkS3efgsrvglFfmQlH/zg==", // User012!
                RoleId = 1
            }
        };

        return users;
    }
}