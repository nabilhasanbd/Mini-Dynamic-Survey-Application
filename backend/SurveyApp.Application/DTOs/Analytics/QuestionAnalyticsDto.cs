namespace SurveyApp.Application.DTOs.Analytics
{
    public class QuestionAnalyticsDto
    {
        public Guid QuestionId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public string QuestionType { get; set; } = string.Empty;
        public int TotalAnswers { get; set; }
        public List<OptionCountDto> OptionCounts { get; set; } = new();
        public double? AverageRating { get; set; }
    }

    public class OptionCountDto
    {
        public Guid OptionId { get; set; }
        public string OptionText { get; set; } = string.Empty;
        public int Count { get; set; }
        public double Percentage { get; set; }
    }
}
