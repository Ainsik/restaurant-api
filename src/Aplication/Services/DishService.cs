using Application.Contracts.Application;
using Application.Contracts.Infrastructure;
using Application.Dto.Dish;
using Application.Dto.Restaurant;
using Application.Exceptions;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services;
public class DishService : IDishService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<DishService> _logger;

    public DishService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DishService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<DishDto>> GetAllAsync(int restaurantId)
    {
        _logger.LogTrace($"GET restaurant with id: {restaurantId} action invoked.");

        var restaurant = await _unitOfWork.RestaurantRepository
            .GetAsync(d => d.Id == restaurantId, includeProperties: "Dishes");

        if (restaurant is null)
        {
            _logger.LogError($"ACTION: GET, Restaurant with id: {restaurantId} doesn't exist.");
            throw new NotFoundApiException(nameof(RestaurantDto), restaurantId.ToString());
        }

        var dishDtos = _mapper.Map<List<DishDto>>(restaurant.Dishes);

        return dishDtos;
    }

    public async Task<DishDto> GetByIdAsync(int restaurantId, int id)
    {
        _logger.LogTrace($"GET restaurant with id: {restaurantId} action invoked.");

        var restaurant = await _unitOfWork.RestaurantRepository
            .GetAsync(restaurantId);

        if (restaurant is null)
        {
            _logger.LogError($"ACTION: GET, Restaurant with id: {restaurantId} doesn't exist.");
            throw new NotFoundApiException(nameof(RestaurantDto), restaurantId.ToString());
        }

        var dish = await _unitOfWork.DishRepository.GetAsync(id);

        if (dish is null || dish.RestaurantId != restaurantId)
        {
            _logger.LogError($"ACTION: GET, Dish with id: {id} doesn't exist.");
            throw new NotFoundApiException(nameof(RestaurantDto), id.ToString());
        }

        var dishDto = _mapper.Map<DishDto>(dish);

        return dishDto;
    }

    public async Task CreateAsync(int restaurantId, NewDishDto dto)
    {
        _logger.LogTrace("CREATE new dish action invoked.");

        var restaurant = await _unitOfWork.RestaurantRepository
            .GetAsync(restaurantId);

        if (restaurant is null)
        {
            _logger.LogError($"ACTION: GET, Restaurant with id: {restaurantId} doesn't exist.");
            throw new NotFoundApiException(nameof(RestaurantDto), restaurantId.ToString());
        }

        var dish = _mapper.Map<Dish>(dto);

        dish.RestaurantId = restaurantId;

        await _unitOfWork.DishRepository.AddAsync(dish);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateAsync(int id, UpdateDishDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}
