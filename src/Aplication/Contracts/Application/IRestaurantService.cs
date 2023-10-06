using Application.Models.Dto.Restaurant;
using Application.Models.Pagination;

namespace Application.Contracts.Application;

public interface IRestaurantService
{
    Task<IEnumerable<RestaurantDto>> GetAllAsync(RestaurantQuery query);
    Task<RestaurantDto> GetByIdAsync(int id);
    Task CreateAsync(NewRestaurantDto dto);
    Task UpdateAsync(int id, UpdateRestaurantDto dto);
    Task DeleteAsync(int id);
}