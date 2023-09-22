using Application.Contracts.Application;
using Application.Dto.Restaurant;
using Core.Entities;
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
        await _restaurantService.CreateAsync(dto);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateContact([FromRoute] int id, [FromBody] UpdateRestaurantDto dto)
    {
        await _restaurantService.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRestaurant([FromRoute] int id)
    {
        await _restaurantService.DeleteAsync(id);
        return NoContent();
    }
}
