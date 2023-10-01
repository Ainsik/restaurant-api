using Application.Contracts.Application;
using Application.Dto.User;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("/api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser([FromBody] NewUserDto dto)
    {
        await _userService.CreateAsync(dto);

        return Ok();
    }
}
