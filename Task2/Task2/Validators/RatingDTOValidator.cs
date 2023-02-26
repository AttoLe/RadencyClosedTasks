using FluentValidation;
using Task2.Mappers.DTOs;

namespace Task2.Validators;

public class RatingDTOValidator : AbstractValidator<RatingDTO>
{
    public RatingDTOValidator()
    {
        RuleFor(r => r.Score).InclusiveBetween(1, 5);
    }
}