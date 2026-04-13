using SurveyApp.Domain.Entities;

namespace SurveyApp.Application.Interfaces.Repositories
{
    public interface ISurveyRepository : IBaseRepository<Survey>
    {
        Task<Survey?> GetBySlugAsync(string slug);
        Task<Survey?> GetWithQuestionsAsync(Guid id);
        Task<IEnumerable<Survey>> GetByUserIdAsync(Guid userId);
        Task<bool> SlugExistsAsync(string slug);
    }
}
