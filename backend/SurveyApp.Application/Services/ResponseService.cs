using SurveyApp.Application.DTOs.Response;
using SurveyApp.Application.Interfaces.Repositories;
using SurveyApp.Application.Interfaces.Services;

namespace SurveyApp.Application.Services
{
    public class ResponseService : IResponseService
    {
        private readonly IResponseRepository _responseRepository;
        private readonly ISurveyRepository _surveyRepository;

        public ResponseService(IResponseRepository responseRepository, ISurveyRepository surveyRepository)
        {
            _responseRepository = responseRepository;
            _surveyRepository = surveyRepository;
        }

        public Task<Guid> SubmitAsync(string slug, SubmitResponseDto dto) => throw new NotImplementedException();
        public Task<IEnumerable<ResponseDetailDto>> GetBySurveyIdAsync(Guid surveyId) => throw new NotImplementedException();
    }
}
