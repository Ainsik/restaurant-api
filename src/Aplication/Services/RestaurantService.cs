using Application.Contracts.Application;
using Application.Contracts.Infrastructure;
using Application.Dto.Restaurant;
using AutoMapper;

namespace Application.Services;
public class RestaurantService : IRestaurantService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RestaurantService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RestaurantDto>> GetAllAsync()
    {
        var restaurants = await _unitOfWork.RestaurantRepository
            .GetAllAsync(includeProperties: "Address, Dishes");

        var restaurantsDto = _mapper.Map<List<RestaurantDto>>(restaurants);

        return restaurantsDto;
    }

    public async Task<RestaurantDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<(int, NewRestaurantDto)> CreateAsync(NewRestaurantDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(int id, UpdateRestaurantDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}
