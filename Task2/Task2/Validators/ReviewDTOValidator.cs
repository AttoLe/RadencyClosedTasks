using FluentValidation;
using Task2.Mappers.DTOs;

namespace Task2.Validators;

public class ReviewDTOValidator : AbstractValidator<ReviewDTO>
{
    public ReviewDTOValidator()
    {
        RuleFor(r => r.Message).Length(2, 300);
        RuleFor(r => r.Reviewer).Length(2, 100);
    }
}