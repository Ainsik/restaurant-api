using Application.Contracts.Application;
using Application.Dto.Dish;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("/api/restaurant/{restaurantId}/dish")]
public class DishController : ControllerBase
{
    private readonly IDishService _dishService;

    public DishController(IDishService dishService)
    {
        _dishService = dishService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateDishAsync([FromRoute] int restaurantId,[FromBody] NewDishDto dto)
    {
        await _dishService.CreateAsync(restaurantId, dto);

        return Ok();
    }
}
