using SurveyApp.Domain.Enums;

namespace SurveyApp.Application.DTOs.Question
{
    public class QuestionResponseDto
    {
        public Guid Id { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public QuestionType QuestionType { get; set; }
        public bool IsRequired { get; set; }
        public int OrderIndex { get; set; }
        public List<OptionDto> Options { get; set; } = new();
    }

    public class OptionDto
    {
        public Guid Id { get; set; }
        public string OptionText { get; set; } = string.Empty;
        public int OrderIndex { get; set; }
    }
}
