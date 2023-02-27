using AutoMapper;
using Task2.Database.Entities;
using Task2.Mappers.DTOs;

namespace Task2.Mappers.Profiles;

public class RatingProfile : Profile
{
    public RatingProfile()
    {
        CreateMap<RatingDTO, Rating>();
    }
}