using Microsoft.EntityFrameworkCore;
using SurveyApp.Application.Interfaces.Repositories;
using SurveyApp.Domain.Entities;
using SurveyApp.Infrastructure.Data;

namespace SurveyApp.Infrastructure.Repositories
{
    public class SurveyRepository : BaseRepository<Survey>, ISurveyRepository
    {
        public SurveyRepository(SurveyDbContext context) : base(context) { }

        public async Task<Survey?> GetBySlugAsync(string slug)
            => await _dbSet
                .Include(s => s.Questions.OrderBy(q => q.OrderIndex))
                    .ThenInclude(q => q.QuestionOptions.OrderBy(o => o.OrderIndex))
                .FirstOrDefaultAsync(s => s.Slug == slug && !s.IsDeleted);

        public async Task<Survey?> GetWithQuestionsAsync(Guid id)
            => await _dbSet
                .Include(s => s.Questions.OrderBy(q => q.OrderIndex))
                    .ThenInclude(q => q.QuestionOptions.OrderBy(o => o.OrderIndex))
                .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);

        public async Task<IEnumerable<Survey>> GetByUserIdAsync(Guid userId)
            => await _dbSet
                .Where(s => s.UserId == userId && !s.IsDeleted)
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();

        public async Task<bool> SlugExistsAsync(string slug)
            => await _dbSet.AnyAsync(s => s.Slug == slug);
    }
}
