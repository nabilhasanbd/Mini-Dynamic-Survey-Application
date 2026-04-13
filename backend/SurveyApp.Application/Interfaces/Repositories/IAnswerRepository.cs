using SurveyApp.Domain.Entities;

namespace SurveyApp.Application.Interfaces.Repositories
{
    public interface IAnswerRepository : IBaseRepository<Answer>
    {
        Task<IEnumerable<Answer>> GetByResponseIdAsync(Guid responseId);
        Task<IEnumerable<Answer>> GetByQuestionIdAsync(Guid questionId);
    }
}
