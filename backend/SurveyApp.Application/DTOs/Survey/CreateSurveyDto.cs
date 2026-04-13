namespace SurveyApp.Application.DTOs.Survey
{
    public class CreateSurveyDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
