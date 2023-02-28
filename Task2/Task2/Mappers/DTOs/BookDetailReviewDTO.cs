using Task2.Database.Entities;

namespace Task2.Mappers.DTOs;

public class BookDetailReviewDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Cover { get; set; }
    public string Content { get; set; }
    public string Genre { get; set; }
    public decimal AvgRating { get; set; }
    public ICollection<Review> Reviews { get; set; }
}