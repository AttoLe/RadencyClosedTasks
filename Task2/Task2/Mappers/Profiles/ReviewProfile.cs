using AutoMapper;
using Task2.Database.Entities;
using Task2.Mappers.DTOs;

namespace Task2.Mappers.Profiles;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<ReviewDTO, Review>();
        CreateMap<Review, IdDTO>();
    }
}