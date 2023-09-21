using Application.Contracts.Infrastructure;
using Core.Entities;

namespace Infrastructure.Repositories;
public class RestaurantRepository : Repository<Restaurant>, IRestaurantRepository
{
    public RestaurantRepository(Microsoft.EntityFrameworkCore.DbContext context) : base(context)
    {
        
    }
}
