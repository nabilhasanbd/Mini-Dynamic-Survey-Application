using FluentValidation;
using SurveyApp.Application.DTOs.Response;

namespace SurveyApp.Application.Validators
{
    public class SubmitResponseValidator : AbstractValidator<SubmitResponseDto>
    {
        public SubmitResponseValidator()
        {
            RuleFor(x => x.Answers).NotNull();
            RuleForEach(x => x.Answers).ChildRules(answer =>
            {
                answer.RuleFor(a => a.QuestionId).NotEmpty();
            });
        }
    }
}
