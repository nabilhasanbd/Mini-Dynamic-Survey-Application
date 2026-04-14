using Microsoft.EntityFrameworkCore;
using SurveyApp.Application.Interfaces.Repositories;
using SurveyApp.Domain.Entities;
using SurveyApp.Infrastructure.Data;

namespace SurveyApp.Infrastructure.Repositories
{
    public class ResponseRepository : BaseRepository<Response>, IResponseRepository
    {
        public ResponseRepository(SurveyDbContext context) : base(context) { }

        public async Task<IEnumerable<Response>> GetBySurveyIdAsync(Guid surveyId)
            => await _dbSet
                .Include(r => r.Answers)
                    .ThenInclude(a => a.Question)
                .Where(r => r.SurveyId == surveyId)
                .OrderByDescending(r => r.SubmittedAt)
                .ToListAsync();

        public async Task<Response?> GetWithAnswersAsync(Guid id)
            => await _dbSet
                .Include(r => r.Answers)
                .FirstOrDefaultAsync(r => r.Id == id);

        public async Task<int> CountBySurveyIdAsync(Guid surveyId)
            => await _dbSet.CountAsync(r => r.SurveyId == surveyId);
    }
}
