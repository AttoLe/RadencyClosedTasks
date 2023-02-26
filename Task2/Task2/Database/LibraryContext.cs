using Microsoft.EntityFrameworkCore;
using Task2.Database.Entities;

namespace Task2.Database;

public class LibraryContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => 
        optionsBuilder.UseLazyLoadingProxies().UseInMemoryDatabase(databaseName: "Library");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.SeedBooks();
        modelBuilder.SeedReviews();
        modelBuilder.SeedRatings();
    }

    public DbSet<Book> Books { get; set; }
}

public static class ModelBuilderExtensions
{
    public static void SeedBooks(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasData(Enumerable.Range(1, 10).Select(i => new Book
            {
                BookId = i,
                Author = "author" + i,
                Content = "content" + i,
                Cover = "cover" + i,
                Genre = "genre" + new Random().Next(1, 4)
            }));
    }
    
    public static void SeedReviews(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasData(Enumerable.Range(1, 150).Select(i => new Review
            {
                ReviewId = i,
                BookId = new Random().Next(1, 10),
                Message = "message" + i,
                Reviewer = "reviewer" + new Random().Next(1, 20),
            }));
    }
    
    public static void SeedRatings(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasData(Enumerable.Range(1, 300).Select(i => new Rating
            {
                RatingId = i,
                BookId = new Random().Next(1, 10),
                Score = new Random().Next(1, 5)
            }));
    }
}