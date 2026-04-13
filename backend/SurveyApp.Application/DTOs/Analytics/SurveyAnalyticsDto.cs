namespace SurveyApp.Application.DTOs.Analytics
{
    public class SurveyAnalyticsDto
    {
        public Guid SurveyId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int TotalResponses { get; set; }
        public List<QuestionAnalyticsDto> Questions { get; set; } = new();
    }
}
