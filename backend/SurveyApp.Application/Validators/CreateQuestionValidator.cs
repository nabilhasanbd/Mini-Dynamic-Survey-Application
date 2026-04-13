using FluentValidation;
using SurveyApp.Application.DTOs.Question;
using SurveyApp.Domain.Enums;

namespace SurveyApp.Application.Validators
{
    public class CreateQuestionValidator : AbstractValidator<CreateQuestionDto>
    {
        public CreateQuestionValidator()
        {
            RuleFor(x => x.QuestionText).NotEmpty().MaximumLength(1000);
            RuleFor(x => x.QuestionType).IsInEnum();
            RuleFor(x => x.Options)
                .NotEmpty()
                .When(x => x.QuestionType is QuestionType.MultipleChoice
                    or QuestionType.Checkboxes
                    or QuestionType.Dropdown)
                .WithMessage("Options are required for this question type.");
        }
    }
}
