using Task2.DTOs;
using Task2.Entities;
using AutoMapper;

namespace Task2.Profiles;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookWReviewNumDTO>()
            .ForMember(d => d.AvgRating,
                o =>
                    o.MapFrom(b => Math.Round(b.Ratings.DefaultIfEmpty().Average(r => r!.Score), 2)))
            .ForMember(d => d.ReviewsNumber,
                o => o.MapFrom(b => b.Reviews.DefaultIfEmpty().Count()));

        CreateMap<Book, BookDetailReviewDTO>()
            .ForMember(d => d.AvgRating,
                o => o.MapFrom(b => b.Ratings.Average(r => r.Score)));

        CreateMap<Book, IdDTO>();
    }
}