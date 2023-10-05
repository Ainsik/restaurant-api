using Application.Contracts.Infrastructure;
using Core.Entities;

namespace Infrastructure.Repositories;

public class DishRepository : Repository<Dish>, IDishRepository
{
    public DishRepository(Microsoft.EntityFrameworkCore.DbContext context) : base(context)
    {
    }
}