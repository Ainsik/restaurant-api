using Application.Contracts.Application;
using Application.Models.Dto.Restaurant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("/api/restaurant")]
[Authorize]
public class RestaurantController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;

    public RestaurantController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAllRestaurantsAsync([FromQuery] string? searchPhrase)
    {
        var restaurants = await _restaurantService.GetAllAsync(searchPhrase);

        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RestaurantDto>> GetRestaurantAsync([FromRoute] int id)
    {
        var restaurant = await _restaurantService.GetByIdAsync(id);

        return Ok(restaurant);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<ActionResult> CreateRestaurantAsync([FromBody] NewRestaurantDto dto)
    {
        await _restaurantService.CreateAsync(dto);

        return Ok();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<ActionResult> UpdateRestaurant([FromRoute] int id, [FromBody] UpdateRestaurantDto dto)
    {
        await _restaurantService.UpdateAsync(id, dto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<ActionResult> DeleteRestaurant([FromRoute] int id)
    {
        await _restaurantService.DeleteAsync(id);

        return NoContent();
    }
}