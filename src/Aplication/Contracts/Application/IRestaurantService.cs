using Application.Dto.Restaurant;

namespace Application.Contracts.Application;
public interface IRestaurantService
{
    Task<IEnumerable<RestaurantDto>> GetAllAsync();
    Task<RestaurantDto> GetByIdAsync(int id);
    Task<(int, NewRestaurantDto)> CreateAsync(NewRestaurantDto dto);
    Task UpdateAsync(int id, UpdateRestaurantDto dto);
    Task DeleteAsync(int id);
}