namespace Task2.Mappers.DTOs;

public class BookWReviewNumDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public string Cover { get; set; }
    public decimal AvgRating { get; set; }
    public int ReviewsNumber { get; set; }
}