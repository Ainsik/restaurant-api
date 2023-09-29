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

    public Task<IEnumerable<DishDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<DishDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
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

    public Task UpdateAsync(int id, UpdateDishDto dto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}
