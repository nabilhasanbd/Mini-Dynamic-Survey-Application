using SurveyApp.Domain.Enums;

namespace SurveyApp.Application.DTOs.Question
{
    public class UpdateQuestionDto
    {
        public string QuestionText { get; set; } = string.Empty;
        public QuestionType QuestionType { get; set; }
        public bool IsRequired { get; set; }
        public int OrderIndex { get; set; }
        public List<string> Options { get; set; } = new();
    }
}
