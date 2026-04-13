using SurveyApp.Application.DTOs.Analytics;

namespace SurveyApp.Application.Interfaces.Services
{
    public interface IAnalyticsService
    {
        Task<SurveyAnalyticsDto> GetSurveyAnalyticsAsync(Guid surveyId);
        Task<byte[]> ExportResponsesToCsvAsync(Guid surveyId);
    }
}
