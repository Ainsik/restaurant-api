using Application.Dto.Restaurant;
using AutoMapper;
using Infrastructure.DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("/api/restaurant")]
public class RestaurantController : ControllerBase
{
    private readonly RestaurantDbContext _dbContext;
    private readonly IMapper _mapper;

    public RestaurantController(RestaurantDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync()
    {
        var restaurants =  _dbContext
            .Restaurants
            .Include(r => r.Address)
            .Include(r => r.Dishes)
            .ToList();

        var restaurantsDtos = _mapper.Map<List<RestaurantDto>>(restaurants);

        return Ok(restaurantsDtos);
    }

    [HttpGet("{id}")]
    public ActionResult<RestaurantDto> GetRestaurantAsync([FromRoute] int id)
    {
        var restaurant = _dbContext
            .Restaurants
            .Include(r => r.Address)
            .Include(r => r.Dishes)
            .FirstOrDefault(r => r.Id == id);

        if (restaurant is null)
        {
            return NotFound();
        }

        var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

        return Ok(restaurantDto);
    }
}
