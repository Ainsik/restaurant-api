using Application.Dto.Restaurant;
using System.Security.Claims;

namespace Application.Contracts.Application;
public interface IRestaurantService
{
    Task<IEnumerable<RestaurantDto>> GetAllAsync();
    Task<RestaurantDto> GetByIdAsync(int id);
    Task CreateAsync(NewRestaurantDto dto);
    Task UpdateAsync(int id, UpdateRestaurantDto dto);
    Task DeleteAsync(int id);
}