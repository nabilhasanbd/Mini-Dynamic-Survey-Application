using SurveyApp.Domain.Entities;

namespace SurveyApp.Application.Interfaces.Repositories
{
    public interface IResponseRepository : IBaseRepository<Response>
    {
        Task<IEnumerable<Response>> GetBySurveyIdAsync(Guid surveyId);
        Task<Response?> GetWithAnswersAsync(Guid id);
        Task<int> CountBySurveyIdAsync(Guid surveyId);
    }
}
