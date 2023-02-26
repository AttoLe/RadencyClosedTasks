using AutoMapper;
using Task2.DTOs;
using Task2.Entities;

namespace Task2.Profiles;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<ReviewDTO, Review>();
        CreateMap<Review, IdDTO>();
    }
}