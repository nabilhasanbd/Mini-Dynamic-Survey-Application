namespace SurveyApp.Application.DTOs.Survey
{
    public class UpdateSurveyDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
