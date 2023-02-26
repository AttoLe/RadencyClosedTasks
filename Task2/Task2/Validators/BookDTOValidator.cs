using System.Data;
using FluentValidation;
using Task2.Mappers.DTOs;

namespace Task2.Validators;

public class BookDTOValidator : AbstractValidator<BookDTO>
{
    public BookDTOValidator()
    {
        RuleFor(b => b.Title).Length(2, 35); //not null??
        RuleFor(b => b.Cover).NotEmpty();
        RuleFor(b => b.Content).NotEmpty();
        RuleFor(b => b.Author).NotEmpty().MaximumLength(100);
        RuleFor(b => b.Genre).Length(2, 20);
    }
}