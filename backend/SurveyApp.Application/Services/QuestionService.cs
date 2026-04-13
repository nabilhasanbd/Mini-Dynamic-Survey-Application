using SurveyApp.Application.DTOs.Question;
using SurveyApp.Application.Interfaces.Repositories;
using SurveyApp.Application.Interfaces.Services;

namespace SurveyApp.Application.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public Task<IEnumerable<QuestionResponseDto>> GetBySurveyIdAsync(Guid surveyId) => throw new NotImplementedException();
        public Task<QuestionResponseDto> CreateAsync(Guid surveyId, CreateQuestionDto dto) => throw new NotImplementedException();
        public Task<QuestionResponseDto> UpdateAsync(Guid id, UpdateQuestionDto dto) => throw new NotImplementedException();
        public Task DeleteAsync(Guid id) => throw new NotImplementedException();
        public Task ReorderAsync(Guid surveyId, List<Guid> orderedIds) => throw new NotImplementedException();
    }
}
