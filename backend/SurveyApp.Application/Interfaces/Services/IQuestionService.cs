using SurveyApp.Application.DTOs.Question;

namespace SurveyApp.Application.Interfaces.Services
{
    public interface IQuestionService
    {
        Task<IEnumerable<QuestionResponseDto>> GetBySurveyIdAsync(Guid surveyId);
        Task<QuestionResponseDto> CreateAsync(Guid surveyId, CreateQuestionDto dto);
        Task<QuestionResponseDto> UpdateAsync(Guid id, UpdateQuestionDto dto);
        Task DeleteAsync(Guid id);
        Task ReorderAsync(Guid surveyId, List<Guid> orderedIds);
    }
}
