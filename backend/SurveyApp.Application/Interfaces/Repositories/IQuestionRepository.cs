using SurveyApp.Domain.Entities;

namespace SurveyApp.Application.Interfaces.Repositories
{
    public interface IQuestionRepository : IBaseRepository<Question>
    {
        Task<IEnumerable<Question>> GetBySurveyIdAsync(Guid surveyId);
        Task<Question?> GetWithOptionsAsync(Guid id);
        Task ReorderAsync(Guid surveyId, List<Guid> orderedIds);
    }
}
