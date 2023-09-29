using Application.Dto.Dish;

namespace Application.Contracts.Application;
public interface IDishService
{
    Task<IEnumerable<DishDto>> GetAllAsync();
    Task<DishDto> GetByIdAsync(int restaurantId, int id);
    Task CreateAsync(int restaurantId, NewDishDto dto);
    Task UpdateAsync(int id, UpdateDishDto dto);
    Task DeleteAsync(int id);
}
