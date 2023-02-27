namespace Task2.Database.Entities;

public class Review
{
    public int Id { get; set; }
    public string Message { get; set; }
    public int BookId { get; set; }
    public string Reviewer { get; set; }
}