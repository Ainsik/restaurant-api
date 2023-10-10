using Application.Models.Dto.Restaurant;
using Application.Models.Dto.User;
using Application.Profiles;
using AutoMapper;
using Core.Entities;
using FluentAssertions;
using Xunit;

namespace ApplicationTests.Profiles;
public class MappingProfileTests
{
    [Fact]
    public void MappingProfile_ShouldMapNewRestaurantDtoToRestaurant()
    {
        //arrange
        var configuration = new MapperConfiguration(cfg => 
            cfg.AddProfile(new MappingProfile()));

        var mapper = configuration.CreateMapper();

        var dto = new NewRestaurantDto
        {
            City = "Test",
            Street = "Test",
            PostalCode = "12-123"
        };

        //act
        var result = mapper.Map<Restaurant>(dto);

        //assert
        result.Should().NotBeNull();
        result.Address.Should().NotBeNull();
        result.Address.City.Should().Be(dto.City);
        result.Address.Street.Should().Be(dto.Street);
        result.Address.PostalCode.Should().Be(dto.PostalCode);
    }

    [Fact]
    public void MappingProfile_ShouldMapRestaurantToRestaurantDto()
    {
        //arrange
        var configuration = new MapperConfiguration(cfg =>
            cfg.AddProfile(new MappingProfile()));

        var mapper = configuration.CreateMapper();

        var restaurant = new Restaurant
        {
            Address = new Address
            {            
                City = "Test",
                Street = "Test",
                PostalCode = "12-123"
            },
        };

        //act
        var result = mapper.Map<RestaurantDto>(restaurant);

        //assert
        result.Should().NotBeNull();
        result.City.Should().Be(restaurant.Address.City);
        result.Street.Should().Be(restaurant.Address.Street);
        result.PostalCode.Should().Be(restaurant.Address.PostalCode);
    }

    [Fact]
    public void MappingProfile_ShouldMapRegisterDtoToUser()
    {
        //arrange
        var configuration = new MapperConfiguration(cfg =>
            cfg.AddProfile(new MappingProfile()));

        var mapper = configuration.CreateMapper();

        var dto = new RegisterDto
        {
            Password = "User1234!"
        };

        //act
        var result = mapper.Map<User>(dto);

        //assert
        result.Should().NotBeNull();
        result.PasswordHash.Should().Be(dto.Password);
    }
}