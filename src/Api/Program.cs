using Api.Middleware;
using Application.Contracts.Application;
using Application.Contracts.Infrastructure;
using Application.Dto.Address;
using Application.Dto.Dish;
using Application.Dto.Restaurant;
using Application.Profiles;
using Application.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.DbContext;
using Infrastructure.Repositories;
using Infrastructure.Seeder;
using Infrastructure.Validations.Address;
using Infrastructure.Validations.Dish;
using Infrastructure.Validations.Restaurant;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using Polly;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Host.UseNLog();

builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<RequestTimeMiddleware>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IDishService, DishService>();

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
