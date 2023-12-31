﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Contracts.Application;
using Application.Contracts.Infrastructure;
using Application.Exceptions;
using Application.Models.Dto.User;
using AutoMapper;
using Core;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly AuthenticationSettings _authenticationSettings;
    private readonly ILogger<UserService> _logger;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserService> logger,
        IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _passwordHasher = passwordHasher;
        _authenticationSettings = authenticationSettings;
    }

    public async Task Register(RegisterDto dto)
    {
        _logger.LogTrace("Register action invoked.");

        var user = _mapper.Map<User>(dto);

        var hashedPassword = _passwordHasher.HashPassword(user, dto.Password);
        user.PasswordHash = hashedPassword;

        await _unitOfWork.UserRepository.AddAsync(user);
        await _unitOfWork.SaveAsync();
    }

    public async Task<string> Login(LoginDto dto)
    {
        _logger.LogTrace("Login action invoked.");

        var user = await _unitOfWork.UserRepository.GetAsync(u => u.Email == dto.Email, "Role");

        if (user is null)
        {
            _logger.LogError($"ACTION: GET, user with email: {dto.Email} doesn't exist.");
            throw new BadRequestException("Invalid username or password");
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

        if (result == PasswordVerificationResult.Failed)
        {
            _logger.LogError($"ACTION: GET, user password: {dto.Password} doesn't exist.");
            throw new BadRequestException("Invalid username or password");
        }

        var tokenJwt = GenerateJwt(user);

        return tokenJwt;
    }

    private string GenerateJwt(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new(ClaimTypes.Role, user.Role.Name)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

        var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
            _authenticationSettings.JwtIssuer,
            claims,
            expires: expires,
            signingCredentials: cred);

        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(token);
    }
}