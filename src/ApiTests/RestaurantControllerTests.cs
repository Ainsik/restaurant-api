using ApiTests.Helpers;
using Application.Models.Dto.Restaurant;
using Core.Entities;
using FluentAssertions;
using Infrastructure.DbContext;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ApiTests;

public class RestaurantControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public RestaurantControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var dbContextOptions = services
                        .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<RestaurantDbContext>));

                    services.Remove(dbContextOptions);

                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();

                    services.AddMvc(option => option.Filters.Add(new FakeUserFilter()));


                    services
                        .AddDbContext<RestaurantDbContext>(options => options.UseInMemoryDatabase("RestaurantDb"));

                });
            });

        _client = _factory.CreateClient();
    }

    private void SeedRestaurant(Restaurant restaurant)
    {
        var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        var _dbContext = scope.ServiceProvider.GetService<RestaurantDbContext>();

        _dbContext.Restaurants.Add(restaurant);
        _dbContext.SaveChanges();
    }

    [Fact]
    public async Task Delete_ForNonRestaurantOwner_ReturnsForbidden()
    {
        // arrange
        var restaurant = new Restaurant()
        {
            CreatedById = 900,
            Name = "Test",
            Description = "Test",
            Category = "Test",
            ContactEmail = "test@o2.pl",
            ContactNumber = "123123123"
        };

        SeedRestaurant(restaurant);
        // act
        var response = await _client.DeleteAsync("/api/restaurant/" + restaurant.Id);

        // assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Forbidden);
    }
    [Fact]
    public async Task Delete_ForRestaurantOwner_ReturnsNoContent()
    {
        // arrange
        var restaurant = new Restaurant
        {
            CreatedById = 1, 
            Name = "Test",
            Description = "Test",
            Category = "Test",
            ContactEmail = "test@o2.pl",
            ContactNumber = "123123123"
        };

        SeedRestaurant(restaurant);
        // act
        var response = await _client.DeleteAsync("/api/restaurant/" + restaurant.Id);

        // assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Delete_ForNonExistingRestaurant_ReturnsNotFound()
    {
        // act
        var response = await _client.DeleteAsync("/api/restaurant/987");

        // assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Theory]
    [InlineData("pageSize=5&pageNumber=1")]
    [InlineData("pageSize=15&pageNumber=2")]
    [InlineData("pageSize=10&pageNumber=3")]
    public async Task GetAll_WithQueryParameters_ReturnsOkResult(string queryParams)
    {
        // act

        var response = await _client.GetAsync("/api/restaurant?" + queryParams);

        // assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Theory]
    [InlineData("pageSize=100&pageNumber=3")]
    [InlineData("pageSize=11&pageNumber=1")]
    [InlineData(null)]
    [InlineData("")]
    public async Task GetAll_WithInvalidQueryParams_ReturnsBadRequest(string queryParams)
    {
        // act
        var response = await _client.GetAsync("/api/restaurant?" + queryParams);

        //assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task CreateRestaurant_WithValidModel_ReturnsOkStatus()
    {
        // arrange
        var model = new NewRestaurantDto
        {
            Name = "TestRestaurant",
            Description = "test",
            Category = "test",
            HasDelivery = true,
            ContactEmail = "testemail@o2.pl",
            ContactNumber = "123123123",
            City = "Test",
            Street = "Test",
            PostalCode = "12-123"
        };

        var httpContent = model.ToJsonHttpContent();

        // act
        var response = await _client.PostAsync("/api/restaurant", httpContent);

        // arrange 
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }


    [Fact]
    public async Task CreateRestaurant_WithInvalidModel_ReturnsBadRequest()
    {
        // arrange
        var model = new NewRestaurantDto()
        {
            ContactEmail = "test@test.com",
            Description = "test desc",
            ContactNumber = "999 888 777"
        };

        var httpContent = model.ToJsonHttpContent();

        // act
        var response = await _client.PostAsync("/api/restaurant", httpContent);

        // arrange
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }
}