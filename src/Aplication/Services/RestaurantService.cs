using Application.Contracts.Application;
using Application.Contracts.Infrastructure;
using Application.Dto.Restaurant;
using Application.Exceptions;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services;
public class RestaurantService : IRestaurantService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<RestaurantService> _logger;

    public RestaurantService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<RestaurantService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<RestaurantDto>> GetAllAsync()
    {
        _logger.LogTrace("GET ALL restaurants action invoked.");

        var restaurants = await _unitOfWork.RestaurantRepository
            .GetAllAsync(includeProperties: "Dishes");

        var restaurantsDto = _mapper.Map<List<RestaurantDto>>(restaurants);

        return restaurantsDto;
    }

    public async Task<RestaurantDto> GetByIdAsync(int id)
    {
        _logger.LogTrace($"GET restaurant with id: {id} action invoked.");

        var restaurant = await _unitOfWork.RestaurantRepository
            .GetAsync(d => d.Id == id, includeProperties: "Dishes");

        if (restaurant is null)
        {
            _logger.LogError($"ACTION: GET, Restaurant with id: {id} doesn't exist.");
            throw new NotFoundApiException(nameof(RestaurantDto), id.ToString());
        }

        var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

        return restaurantDto;
    }

    public async Task CreateAsync(NewRestaurantDto dto)
    {
        _logger.LogTrace("CREATE new restaurant action invoked.");

        var restaurant = _mapper.Map<Restaurant>(dto);

        await _unitOfWork.RestaurantRepository.AddAsync(restaurant);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateAsync(int id, UpdateRestaurantDto dto)
    {
        _logger.LogTrace($"Restaurant with id: {id} UPDATE action invoked.");

        var updateRestaurant = await _unitOfWork.RestaurantRepository.GetAsync(id);

        if (updateRestaurant is null)
        {
            _logger.LogError($"ACTION: UPDATE, Restaurant with id: {id} doesn't exist.");
            throw new NotFoundApiException(nameof(RestaurantDto), id.ToString());
        }

        _mapper.Map(updateRestaurant, dto);
        _unitOfWork.RestaurantRepository.Modify(updateRestaurant);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        _logger.LogTrace($"Restaurant with id: {id} DELETE action invoked.");

        var deleteRestaurant = await _unitOfWork.RestaurantRepository.GetAsync(id);

        if (deleteRestaurant is null)
        {
            _logger.LogError($"ACTION: DELETE, Restaurant with id: {id} doesn't exist.");
            throw new NotFoundApiException("Wrong id.");
        }

        _unitOfWork.RestaurantRepository.Remove(deleteRestaurant);
        await _unitOfWork.SaveAsync();
    }
}
