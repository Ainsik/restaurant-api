﻿using System.Security.Claims;
using Application.Contracts.Application;
using Microsoft.AspNetCore.Http;

namespace Application.Services;

public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public ClaimsPrincipal User => _httpContextAccessor.HttpContext.User;

    public int GetUserId
        => int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
}