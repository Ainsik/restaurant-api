using System.Security.Claims;
using Application.Authorization;
using Application.Contracts.Application;
using Application.Contracts.Infrastructure;
using Application.Dto.Restaurant;
using Application.Exceptions;
using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Application.Services;
public class RestaurantService : IRestaurantService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<RestaurantService> _logger;
    private readonly IAuthorizationService _authorizationService;

    public RestaurantService(IUnitOfWork unitOfWork, IMapper mapper, 
        ILogger<RestaurantService> logger, IAuthorizationService authorizationService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _authorizationService = authorizationService;
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

    public async Task CreateAsync(NewRestaurantDto dto, int userId)
    {
        _logger.LogTrace("CREATE new restaurant action invoked.");

        var restaurant = _mapper.Map<Restaurant>(dto);

        restaurant.CreatedById = userId;

        await _unitOfWork.RestaurantRepository.AddAsync(restaurant);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateAsync(int id, UpdateRestaurantDto dto, ClaimsPrincipal user)
    {
        _logger.LogTrace($"UPDATE restaurant with id: {id} action invoked.");

        var updateRestaurant = await _unitOfWork.RestaurantRepository.GetAsync(id);

        if (updateRestaurant is null)
        {
            _logger.LogError($"ACTION: UPDATE, Restaurant with id: {id} doesn't exist.");
            throw new NotFoundApiException(nameof(RestaurantDto), id.ToString());
        }

        var authorizationResult = _authorizationService.AuthorizeAsync(user, updateRestaurant,
            new ResourceOperationRequirement(ResourceOperation.Update)).Result;

        if (!authorizationResult.Succeeded)
        {
            _logger.LogError($"ACTION: UPDATE, User isn't a manager of restaurant: {updateRestaurant.Id}.");
            throw new ForbidException("You aren't a manager of this restaurant.");
        }

        _mapper.Map(dto, updateRestaurant);
        _unitOfWork.RestaurantRepository.Modify(updateRestaurant);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteAsync(int id, ClaimsPrincipal user)
    {
        _logger.LogTrace($"DELETE restaurant with id: {id} action invoked.");

        var deleteRestaurant = await _unitOfWork.RestaurantRepository.GetAsync(id);

        if (deleteRestaurant is null)
        {
            _logger.LogError($"ACTION: DELETE, Restaurant with id: {id} doesn't exist.");
            throw new NotFoundApiException("Wrong id.");
        }

        var authorizationResult = _authorizationService.AuthorizeAsync(user, deleteRestaurant,
            new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

        if (!authorizationResult.Succeeded)
        {
            _logger.LogError($"ACTION: DELETE, User isn't a manager of restaurant: {deleteRestaurant.Id}.");
            throw new ForbidException("You aren't a manager of this restaurant.");
        }

        _unitOfWork.RestaurantRepository.Remove(deleteRestaurant);
        await _unitOfWork.SaveAsync();
    }
}
