using SurveyApp.Application.DTOs.Response;

namespace SurveyApp.Application.Interfaces.Services
{
    public interface IResponseService
    {
        Task<Guid> SubmitAsync(string slug, SubmitResponseDto dto);
        Task<IEnumerable<ResponseDetailDto>> GetBySurveyIdAsync(Guid surveyId);
    }
}
