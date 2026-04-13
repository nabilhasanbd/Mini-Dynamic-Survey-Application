using Microsoft.EntityFrameworkCore;
using SurveyApp.Application.Interfaces.Repositories;
using SurveyApp.Domain.Entities;
using SurveyApp.Infrastructure.Data;

namespace SurveyApp.Infrastructure.Repositories
{
    public class AnswerRepository : BaseRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(SurveyDbContext context) : base(context) { }

        public async Task<IEnumerable<Answer>> GetByResponseIdAsync(Guid responseId)
            => await _dbSet.Where(a => a.ResponseId == responseId).ToListAsync();

        public async Task<IEnumerable<Answer>> GetByQuestionIdAsync(Guid questionId)
            => await _dbSet.Where(a => a.QuestionId == questionId).ToListAsync();
    }
}
