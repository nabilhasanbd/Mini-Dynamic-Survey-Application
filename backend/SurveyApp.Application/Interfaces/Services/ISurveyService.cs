using SurveyApp.Application.DTOs.Survey;

namespace SurveyApp.Application.Interfaces.Services
{
    public interface ISurveyService
    {
        Task<IEnumerable<SurveyResponseDto>> GetAllByUserAsync(Guid userId);
        Task<SurveyResponseDto?> GetByIdAsync(Guid id);
        Task<SurveyResponseDto?> GetBySlugAsync(string slug);
        Task<SurveyResponseDto> CreateAsync(Guid userId, CreateSurveyDto dto);
        Task<SurveyResponseDto> UpdateAsync(Guid id, UpdateSurveyDto dto);
        Task DeleteAsync(Guid id);
        Task PublishAsync(Guid id);
        Task UnpublishAsync(Guid id);
    }
}
