using System.Security.Claims;
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
        var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

        await _restaurantService.CreateAsync(dto, userId);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateRestaurant([FromRoute] int id, [FromBody] UpdateRestaurantDto dto)
    {
        await _restaurantService.UpdateAsync(id, dto, User);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRestaurant([FromRoute] int id)
    {
        await _restaurantService.DeleteAsync(id, User);

        return NoContent();
    }
}
