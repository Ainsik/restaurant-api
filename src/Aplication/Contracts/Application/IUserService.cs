﻿using Application.Dto.User;

namespace Application.Contracts.Application;
public interface IUserService
{
    Task CreateAsync(RegisterDto dto);
    Task<string> GenerateJwt(LoginDto dto);
}
