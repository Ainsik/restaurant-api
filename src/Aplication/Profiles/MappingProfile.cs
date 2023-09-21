using Application.Dto.Dish;
using Application.Dto.Restaurant;
using AutoMapper;
using Core.Entities;

namespace Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Restaurant, RestaurantDto>()
            .ForMember(r => r.City, x => x.MapFrom(d => d.Address.City))
            .ForMember(r => r.Street, x => x.MapFrom(d => d.Address.Street))
            .ForMember(r => r.PostalCode, x => x.MapFrom(d => d.Address.PostalCode));

        CreateMap<Dish, DishDto>();
    }
}

