namespace Task2.Database.Entities;

public class Rating : BaseEntity
{
    public int BookId { get; set; }
    public int Score { get; set; }
}