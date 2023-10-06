using Application.Contracts.Application;
using Application.Contracts.Infrastructure;
using Application.Exceptions;
using Application.Models.Dto.Dish;
using Application.Models.Dto.Restaurant;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class DishService : IDishService
{
    private readonly ILogger<DishService> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public DishService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DishService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<DishDto>> GetAllAsync(int restaurantId)
    {
        _logger.LogTrace("GET ALL dishes action invoked.");

        var restaurant = await GetRestaurantById(restaurantId, "Dishes");

        var dishesDto = _mapper.Map<List<DishDto>>(restaurant.Dishes);

        return dishesDto;
    }

    public async Task<DishDto> GetByIdAsync(int restaurantId, int id)
    {
        _logger.LogTrace($"GET dish with id: {id} action invoked.");

        await GetRestaurantById(restaurantId);

        var dish = await GetDishById(restaurantId, id);

        var dishDto = _mapper.Map<DishDto>(dish);

        return dishDto;
    }

    public async Task CreateAsync(int restaurantId, NewDishDto dto)
    {
        _logger.LogTrace("CREATE new dish action invoked.");

        await GetRestaurantById(restaurantId);

        var dish = _mapper.Map<Dish>(dto);

        dish.RestaurantId = restaurantId;

        await _unitOfWork.DishRepository.AddAsync(dish);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateAsync(int restaurantId, int id, UpdateDishDto dto)
    {
        _logger.LogTrace($"UPDATE dish with id: {id} from restaurant id: {restaurantId} action invoked.");

        await GetRestaurantById(restaurantId, "Dishes");

        var updateDish = await GetDishById(restaurantId, id);

        _mapper.Map(dto, updateDish);
        _unitOfWork.DishRepository.Modify(updateDish);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteAllAsync(int restaurantId)
    {
        _logger.LogTrace($"DELETE all dishes from restaurant id: {restaurantId} action invoked.");

        var restaurant = await GetRestaurantById(restaurantId, "Dishes");

        _unitOfWork.DishRepository.RemoveRange(restaurant.Dishes);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteAsync(int restaurantId, int id)
    {
        _logger.LogTrace($"DELETE dish with id: {id} from restaurant id: {restaurantId} action invoked.");

        await GetRestaurantById(restaurantId);

        var dish = await GetDishById(restaurantId, id);

        _unitOfWork.DishRepository.Remove(dish);
        await _unitOfWork.SaveAsync();
    }

    private async Task<Restaurant> GetRestaurantById(int restaurantId, string? includeProperties = null)
    {
        var restaurant = await _unitOfWork.RestaurantRepository
            .GetAsync(d => d.Id == restaurantId, includeProperties);

        if (restaurant is not null) return restaurant;

        _logger.LogError($"ACTION: GET, Restaurant with id: {restaurantId} doesn't exist.");
        throw new NotFoundApiException(nameof(RestaurantDto), restaurantId.ToString());
    }

    private async Task<Dish> GetDishById(int restaurantId, int dishId)
    {
        var dish = await _unitOfWork.DishRepository.GetAsync(dishId);

        if (dish is not null && dish.RestaurantId == restaurantId) return dish;

        _logger.LogError($"ACTION: GET, Dish with id: {dishId} from restaurant id: {restaurantId} doesn't exist.");
        throw new NotFoundApiException(nameof(RestaurantDto), dishId.ToString());
    }
}