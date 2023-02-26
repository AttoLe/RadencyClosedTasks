namespace Task2.Database.Entities;

public class Review
{
    public int ReviewId { get; set; }
    public string Message { get; set; }
    public int BookId { get; set; }
    public string Reviewer { get; set; }
}