using Application.Contracts.Application;
using Application.Dto.Restaurant;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("/api/restaurant")]
public class RestaurantController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;


    public RestaurantController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAllRestaurantsAsync()
    {
        var restaurants = await _restaurantService.GetAllAsync();

        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RestaurantDto>> GetRestaurantAsync([FromRoute] int id)
    {
        var restaurant = await _restaurantService.GetByIdAsync(id);

        return Ok(restaurant);
    }

    [HttpPost]
    public async Task<ActionResult> CreateRestaurantAsync([FromBody] NewRestaurantDto dto)
    {
        //var restaurant = _mapper.Map<Restaurant>(dto);
        //_dbContext.Restaurants.Add(restaurant);
        //_dbContext.SaveChanges();

        //return Created($"/api/restaurant/{restaurant.Id}", restaurant);

        throw new NotImplementedException();
    }
}
