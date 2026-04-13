using SurveyApp.Application.DTOs.Analytics;
using SurveyApp.Application.Interfaces.Repositories;
using SurveyApp.Application.Interfaces.Services;

namespace SurveyApp.Application.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IResponseRepository _responseRepository;

        public AnalyticsService(ISurveyRepository surveyRepository, IResponseRepository responseRepository)
        {
            _surveyRepository = surveyRepository;
            _responseRepository = responseRepository;
        }

        public Task<SurveyAnalyticsDto> GetSurveyAnalyticsAsync(Guid surveyId) => throw new NotImplementedException();
        public Task<byte[]> ExportResponsesToCsvAsync(Guid surveyId) => throw new NotImplementedException();
    }
}
