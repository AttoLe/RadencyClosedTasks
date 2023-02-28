using AutoMapper;
using Task2.Database.Entities;
using Task2.Mappers.DTOs;

namespace Task2.Mappers.Profiles;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookWReviewNumDTO>()
            .ForMember(d => d.AvgRating,
                o =>
                    o.MapFrom(b => Math.Round(b.Ratings.DefaultIfEmpty().Average(r => r!.Score), 2)))
            .ForMember(d => d.ReviewsNumber,
                o =>
                    o.MapFrom(b => b.Reviews.DefaultIfEmpty().Count()));

        CreateMap<Book, BookDetailReviewDTO>()
            .ForMember(d => d.AvgRating,
                o =>
                    o.MapFrom(b => Math.Round(b.Ratings.DefaultIfEmpty().Average(r => r!.Score),2)));

        CreateMap<BookDTO, Book>();
        
        CreateMap<Book, IdDTO>();
    }
}