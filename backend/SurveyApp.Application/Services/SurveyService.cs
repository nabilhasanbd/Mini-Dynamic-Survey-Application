using SurveyApp.Application.DTOs.Survey;
using SurveyApp.Application.Interfaces.Repositories;
using SurveyApp.Application.Interfaces.Services;

namespace SurveyApp.Application.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly ISurveyRepository _surveyRepository;

        public SurveyService(ISurveyRepository surveyRepository)
        {
            _surveyRepository = surveyRepository;
        }

        public Task<IEnumerable<SurveyResponseDto>> GetAllByUserAsync(Guid userId) => throw new NotImplementedException();
        public Task<SurveyResponseDto?> GetByIdAsync(Guid id) => throw new NotImplementedException();
        public Task<SurveyResponseDto?> GetBySlugAsync(string slug) => throw new NotImplementedException();
        public Task<SurveyResponseDto> CreateAsync(Guid userId, CreateSurveyDto dto) => throw new NotImplementedException();
        public Task<SurveyResponseDto> UpdateAsync(Guid id, UpdateSurveyDto dto) => throw new NotImplementedException();
        public Task DeleteAsync(Guid id) => throw new NotImplementedException();
        public Task PublishAsync(Guid id) => throw new NotImplementedException();
        public Task UnpublishAsync(Guid id) => throw new NotImplementedException();
    }
}
