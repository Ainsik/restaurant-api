using Application.Contracts.Application;
using Application.Contracts.Infrastructure;
using Application.Dto.Restaurant;
using Application.Exceptions;
using AutoMapper;
using Core.Entities;

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
        var restaurant = await _unitOfWork.RestaurantRepository
            .GetAsync(d => d.Id == id, includeProperties: "Address, Dishes");

        if (restaurant is null) throw new NotFoundApiException(nameof(RestaurantDto), id.ToString());

        var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

        return restaurantDto;
    }

    public async Task CreateAsync(NewRestaurantDto dto)
    {
        var restaurant = _mapper.Map<Restaurant>(dto);
        
        await _unitOfWork.RestaurantRepository.AddAsync(restaurant);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateAsync(int id, UpdateRestaurantDto dto)
    {
        var updateRestaurant = await _unitOfWork.RestaurantRepository.GetAsync(id);

        if (updateRestaurant is null) throw new NotFoundApiException(nameof(RestaurantDto), id.ToString());

        _mapper.Map(updateRestaurant, dto);
        _unitOfWork.RestaurantRepository.Modify(updateRestaurant);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var deleteRestaurant = await _unitOfWork.RestaurantRepository.GetAsync(id);

        if (deleteRestaurant is null) throw new NotFoundApiException("Wrong id.");

        _unitOfWork.RestaurantRepository.Remove(deleteRestaurant);
        await _unitOfWork.SaveAsync();
    }
}
