using SurveyApp.Application.DTOs.Question;
using SurveyApp.Application.Interfaces.Repositories;
using SurveyApp.Application.Interfaces.Services;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Application.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<IEnumerable<QuestionResponseDto>> GetBySurveyIdAsync(Guid surveyId)
        {
            var questions = await _questionRepository.GetBySurveyIdAsync(surveyId);
            return questions.Select(MapToDto);
        }

        public async Task<QuestionResponseDto> CreateAsync(Guid surveyId, CreateQuestionDto dto)
        {
            var question = new Question
            {
                Id = Guid.NewGuid(),
                SurveyId = surveyId,
                QuestionText = dto.QuestionText,
                QuestionType = dto.QuestionType,
                IsRequired = dto.IsRequired,
                OrderIndex = dto.OrderIndex,
                QuestionOptions = dto.Options.Select((text, i) => new QuestionOption
                {
                    Id = Guid.NewGuid(),
                    OptionText = text,
                    OrderIndex = i,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }).ToList(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _questionRepository.AddAsync(question);
            await _questionRepository.SaveChangesAsync();

            return MapToDto(question);
        }

        public async Task<QuestionResponseDto> UpdateAsync(Guid id, UpdateQuestionDto dto)
        {
            var question = await _questionRepository.GetWithOptionsAsync(id)
                ?? throw new KeyNotFoundException("Question not found.");

            question.QuestionText = dto.QuestionText;
            question.QuestionType = dto.QuestionType;
            question.IsRequired = dto.IsRequired;
            question.OrderIndex = dto.OrderIndex;
            question.UpdatedAt = DateTime.UtcNow;

            var newOptions = dto.Options.Select((text, i) => new QuestionOption
            {
                Id = Guid.NewGuid(),
                QuestionId = id,
                OptionText = text,
                OrderIndex = i,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }).ToList();

            await _questionRepository.ReplaceOptionsAsync(id, newOptions);
            await _questionRepository.UpdateAsync(question);
            await _questionRepository.SaveChangesAsync();

            question.QuestionOptions = newOptions;
            return MapToDto(question);
        }

        public async Task DeleteAsync(Guid id)
        {
            var question = await _questionRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Question not found.");

            await _questionRepository.DeleteAsync(question);
            await _questionRepository.SaveChangesAsync();
        }

        public async Task ReorderAsync(Guid surveyId, List<Guid> orderedIds)
        {
            await _questionRepository.ReorderAsync(surveyId, orderedIds);
            await _questionRepository.SaveChangesAsync();
        }

        private static QuestionResponseDto MapToDto(Question q) => new()
        {
            Id = q.Id,
            QuestionText = q.QuestionText,
            QuestionType = q.QuestionType,
            IsRequired = q.IsRequired,
            OrderIndex = q.OrderIndex,
            Options = q.QuestionOptions.Select(o => new OptionDto
            {
                Id = o.Id,
                OptionText = o.OptionText,
                OrderIndex = o.OrderIndex
            }).ToList()
        };
    }
}
