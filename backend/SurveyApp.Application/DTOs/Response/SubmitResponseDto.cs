namespace SurveyApp.Application.DTOs.Response
{
    public class SubmitResponseDto
    {
        public List<AnswerDto> Answers { get; set; } = new();
    }

    public class AnswerDto
    {
        public Guid QuestionId { get; set; }
        public string? AnswerText { get; set; }
        public List<Guid>? SelectedOptionIds { get; set; }
        public int? RatingValue { get; set; }
    }
}
