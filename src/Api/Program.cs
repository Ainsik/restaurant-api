using System.Text;
using Api.Middleware;
using Application.Authorization;
using Application.Contracts.Application;
using Application.Contracts.Infrastructure;
using Application.Models.Dto.Address;
using Application.Models.Dto.Dish;
using Application.Models.Dto.Restaurant;
using Application.Models.Dto.User;
using Application.Models.Pagination;
using Application.Profiles;
using Application.Services;
using Core;
using Core.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.DbContext;
using Infrastructure.Repositories;
using Infrastructure.Seeder;
using Infrastructure.Validations.Address;
using Infrastructure.Validations.Dish;
using Infrastructure.Validations.Pagination;
using Infrastructure.Validations.Restaurant;
using Infrastructure.Validations.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Web;
using Polly;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Host.UseNLog();

var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
    };
});

builder.Services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>();

builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<RequestTimeMiddleware>();

builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddDbContext<RestaurantDbContext>(
    option => option.UseSqlServer(builder.Configuration.GetConnectionString("RestaurantConnectionString"))
);

builder.Services.AddScoped<Seeder>();

builder.Services.AddFluentValidation();

builder.Services.AddScoped<IValidator<NewAddressDto>, NewAddressDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateAddressDto>, UpdateAddressDtoValidator>();

builder.Services.AddScoped<IValidator<NewRestaurantDto>, NewRestaurantDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateRestaurantDto>, UpdateRestaurantDtoValidator>();

builder.Services.AddScoped<IValidator<NewDishDto>, NewDishDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateDishDto>, UpdateDishDtoValidator>();

builder.Services.AddScoped<IValidator<RegisterDto>, RegisterDtoValidator>();
builder.Services.AddScoped<IValidator<LoginDto>, LoginValidator>();

builder.Services.AddScoped<IValidator<PaginationQuery>, PaginationQueryValidator>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder
            .WithOrigins(builder.Configuration["AllowedOrigins"])
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeMiddleware>();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetService<RestaurantDbContext>();
var pendingMigrations = dbContext.Database.GetPendingMigrations();

var policy = Policy
    .Handle<Exception>()
    .WaitAndRetry(3, attempt => TimeSpan.FromSeconds(attempt * 3));

var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
seeder.Seed();

policy.Execute(() =>
{
    if (pendingMigrations.Any()) dbContext.Database.Migrate();
});

app.Run();

public partial class Program { }