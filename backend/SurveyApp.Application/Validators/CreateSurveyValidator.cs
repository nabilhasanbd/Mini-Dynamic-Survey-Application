using FluentValidation;
using SurveyApp.Application.DTOs.Survey;

namespace SurveyApp.Application.Validators
{
    public class CreateSurveyValidator : AbstractValidator<CreateSurveyDto>
    {
        public CreateSurveyValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(500);
            RuleFor(x => x.ExpiryDate)
                .GreaterThan(DateTime.UtcNow)
                .When(x => x.ExpiryDate.HasValue)
                .WithMessage("Expiry date must be in the future.");
        }
    }
}
