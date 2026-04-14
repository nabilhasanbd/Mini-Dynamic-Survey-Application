using Microsoft.EntityFrameworkCore;
using SurveyApp.Application.Interfaces.Repositories;
using SurveyApp.Domain.Entities;
using SurveyApp.Infrastructure.Data;

namespace SurveyApp.Infrastructure.Repositories
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(SurveyDbContext context) : base(context) { }

        public async Task<IEnumerable<Question>> GetBySurveyIdAsync(Guid surveyId)
            => await _dbSet
                .Include(q => q.QuestionOptions.OrderBy(o => o.OrderIndex))
                .Where(q => q.SurveyId == surveyId)
                .OrderBy(q => q.OrderIndex)
                .ToListAsync();

        public async Task<Question?> GetWithOptionsAsync(Guid id)
            => await _dbSet
                .Include(q => q.QuestionOptions.OrderBy(o => o.OrderIndex))
                .FirstOrDefaultAsync(q => q.Id == id);

        public async Task ReorderAsync(Guid surveyId, List<Guid> orderedIds)
        {
            var questions = await _dbSet
                .Where(q => q.SurveyId == surveyId)
                .ToListAsync();

            for (int i = 0; i < orderedIds.Count; i++)
            {
                var question = questions.FirstOrDefault(q => q.Id == orderedIds[i]);
                if (question is not null)
                    question.OrderIndex = i;
            }
        }

        public async Task ReplaceOptionsAsync(Guid questionId, List<QuestionOption> newOptions)
        {
            var old = await _context.QuestionOptions
                .Where(o => o.QuestionId == questionId)
                .ToListAsync();
            _context.QuestionOptions.RemoveRange(old);
            await _context.QuestionOptions.AddRangeAsync(newOptions);
        }
    }
}
