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
    private readonly IAuthorizationService _authorizationService;
    private readonly ILogger<RestaurantService> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContextService _userContextService;

    public RestaurantService(IUnitOfWork unitOfWork, IMapper mapper,
        ILogger<RestaurantService> logger, IAuthorizationService authorizationService,
        IUserContextService userContextService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _authorizationService = authorizationService;
        _userContextService = userContextService;
    }

    public async Task<IEnumerable<RestaurantDto>> GetAllAsync()
    {
        _logger.LogTrace("GET ALL restaurants action invoked.");

        var restaurants = await _unitOfWork.RestaurantRepository
            .GetAllAsync(includeProperties: "Address,Dishes");

        var restaurantsDto = _mapper.Map<List<RestaurantDto>>(restaurants);

        return restaurantsDto;
    }

    public async Task<RestaurantDto> GetByIdAsync(int id)
    {
        _logger.LogTrace($"GET restaurant id: {id} action invoked.");

        var restaurant = await _unitOfWork.RestaurantRepository
            .GetAsync(d => d.Id == id, "Address,Dishes");

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

        restaurant.CreatedById = _userContextService.GetUserId;

        await _unitOfWork.RestaurantRepository.AddAsync(restaurant);
        await _unitOfWork.SaveAsync();

        _logger.LogTrace($"New restaurant id: {restaurant.Id} created by user id: {restaurant.CreatedById}");
    }

    public async Task UpdateAsync(int id, UpdateRestaurantDto dto)
    {
        _logger.LogTrace($"UPDATE restaurant id: {id} action invoked.");

        var updateRestaurant = await _unitOfWork.RestaurantRepository.GetAsync(id);

        if (updateRestaurant is null)
        {
            _logger.LogError($"ACTION: UPDATE, Restaurant id: {id} doesn't exist.");
            throw new NotFoundApiException(nameof(RestaurantDto), id.ToString());
        }

        var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, updateRestaurant,
            new ResourceOperationRequirement(ResourceOperation.Update)).Result;

        if (!authorizationResult.Succeeded)
        {
            _logger.LogError(
                $"ACTION: UPDATE, User id: {_userContextService.GetUserId} isn't a manager of restaurant id: {updateRestaurant.Id}.");
            throw new ForbidException("You aren't a manager of this restaurant.");
        }

        _mapper.Map(dto, updateRestaurant);
        _unitOfWork.RestaurantRepository.Modify(updateRestaurant);
        await _unitOfWork.SaveAsync();

        _logger.LogTrace($"Restaurant id: {updateRestaurant.Id} updated by user id: {_userContextService.GetUserId}.");
    }

    public async Task DeleteAsync(int id)
    {
        _logger.LogTrace($"DELETE restaurant id: {id} action invoked.");

        var deleteRestaurant = await _unitOfWork.RestaurantRepository.GetAsync(id);

        if (deleteRestaurant is null)
        {
            _logger.LogError($"ACTION: DELETE, Restaurant id: {id} doesn't exist.");
            throw new NotFoundApiException("Wrong id.");
        }

        var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, deleteRestaurant,
            new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

        if (!authorizationResult.Succeeded)
        {
            _logger.LogError(
                $"ACTION: DELETE, User id: {_userContextService.GetUserId} isn't a manager of restaurant id: {deleteRestaurant.Id}.");
            throw new ForbidException("You aren't a manager of this restaurant.");
        }

        _unitOfWork.RestaurantRepository.Remove(deleteRestaurant);
        await _unitOfWork.SaveAsync();

        _logger.LogTrace($"Restaurant id: {deleteRestaurant.Id} deleted by user id: {_userContextService.GetUserId}.");
    }
}