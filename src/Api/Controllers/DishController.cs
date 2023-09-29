﻿using Application.Contracts.Application;
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DishDto>>> GetAllDishesAsync(int restaurantId)
    {
        var dishes = await _dishService.GetAllAsync(restaurantId);

        return Ok(dishes);
    }

    [HttpGet("{dishId}")]
    public async Task<ActionResult<DishDto>> GetDishAsync([FromRoute] int restaurantId, [FromRoute] int dishId)
    {
        var dish = await _dishService.GetByIdAsync(restaurantId, dishId);

        return Ok(dish);
    }

    [HttpPost]
    public async Task<ActionResult> CreateDishAsync([FromRoute] int restaurantId,[FromBody] NewDishDto dto)
    {
        await _dishService.CreateAsync(restaurantId, dto);

        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteAllDishes([FromRoute] int restaurantId)
    {
        await _dishService.DeleteAllAsync(restaurantId);

        return NoContent();
    }
}
