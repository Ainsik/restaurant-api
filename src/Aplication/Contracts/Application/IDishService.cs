﻿using Application.Models.Dto.Dish;

namespace Application.Contracts.Application;

public interface IDishService
{
    Task<IEnumerable<DishDto>> GetAllAsync(int restaurantId);
    Task<DishDto> GetByIdAsync(int restaurantId, int id);
    Task CreateAsync(int restaurantId, NewDishDto dto);
    Task UpdateAsync(int restaurantId, int id, UpdateDishDto dto);
    Task DeleteAllAsync(int restaurantId);
    Task DeleteAsync(int restaurantId, int id);
}