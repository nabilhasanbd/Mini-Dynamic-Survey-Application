namespace SurveyApp.Application.DTOs.Response
{
    public class ResponseDetailDto
    {
        public Guid Id { get; set; }
        public DateTime SubmittedAt { get; set; }
        public List<AnswerDetailDto> Answers { get; set; } = new();
    }

    public class AnswerDetailDto
    {
        public Guid QuestionId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public string? AnswerText { get; set; }
        public List<Guid>? SelectedOptionIds { get; set; }
        public int? RatingValue { get; set; }
    }
}
