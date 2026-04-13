using SurveyApp.Application.DTOs.Question;

namespace SurveyApp.Application.DTOs.Survey
{
    public class SurveyResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Slug { get; set; } = string.Empty;
        public bool IsPublished { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<QuestionResponseDto> Questions { get; set; } = new();
    }
}
