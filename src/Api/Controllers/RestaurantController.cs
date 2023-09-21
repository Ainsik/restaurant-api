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
        //var restaurant = _dbContext
        //    .Restaurants
        //    .Include(r => r.Address)
        //    .Include(r => r.Dishes)
        //    .FirstOrDefault(r => r.Id == id);

        //if (restaurant is null)
        //{
        //    return NotFound();
        //}

        //var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

        //return Ok(restaurantDto);

        throw new NotImplementedException();
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
