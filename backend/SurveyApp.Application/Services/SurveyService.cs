using SurveyApp.Application.DTOs.Question;
using SurveyApp.Application.DTOs.Survey;
using SurveyApp.Application.Interfaces.Repositories;
using SurveyApp.Application.Interfaces.Services;
using SurveyApp.Domain.Entities;
using SurveyApp.Domain.ValueObjects;

namespace SurveyApp.Application.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly ISurveyRepository _surveyRepository;

        public SurveyService(ISurveyRepository surveyRepository)
        {
            _surveyRepository = surveyRepository;
        }

        public async Task<IEnumerable<SurveyResponseDto>> GetAllByUserAsync(Guid userId)
        {
            var surveys = await _surveyRepository.GetByUserIdAsync(userId);
            return surveys.Select(MapToDto);
        }

        public async Task<SurveyResponseDto?> GetByIdAsync(Guid id)
        {
            var survey = await _surveyRepository.GetWithQuestionsAsync(id);
            return survey is null ? null : MapToDto(survey);
        }

        public async Task<SurveyResponseDto?> GetBySlugAsync(string slug)
        {
            var survey = await _surveyRepository.GetBySlugAsync(slug);
            return survey is null ? null : MapToDto(survey);
        }

        public async Task<SurveyResponseDto> CreateAsync(Guid userId, CreateSurveyDto dto)
        {
            string slug;
            do { slug = SurveySlug.Generate().Value; }
            while (await _surveyRepository.SlugExistsAsync(slug));

            var survey = new Survey
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                Slug = slug,
                ExpiryDate = dto.ExpiryDate,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _surveyRepository.AddAsync(survey);
            await _surveyRepository.SaveChangesAsync();

            return MapToDto(survey);
        }

        public async Task<SurveyResponseDto> UpdateAsync(Guid id, UpdateSurveyDto dto)
        {
            var survey = await _surveyRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Survey not found.");

            survey.Title = dto.Title;
            survey.Description = dto.Description;
            survey.ExpiryDate = dto.ExpiryDate;
            survey.UpdatedAt = DateTime.UtcNow;

            await _surveyRepository.UpdateAsync(survey);
            await _surveyRepository.SaveChangesAsync();

            var updated = await _surveyRepository.GetWithQuestionsAsync(id);
            return MapToDto(updated!);
        }

        public async Task DeleteAsync(Guid id)
        {
            var survey = await _surveyRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Survey not found.");

            survey.IsDeleted = true;
            survey.UpdatedAt = DateTime.UtcNow;

            await _surveyRepository.UpdateAsync(survey);
            await _surveyRepository.SaveChangesAsync();
        }

        public async Task PublishAsync(Guid id)
        {
            var survey = await _surveyRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Survey not found.");

            survey.IsPublished = true;
            survey.UpdatedAt = DateTime.UtcNow;

            await _surveyRepository.UpdateAsync(survey);
            await _surveyRepository.SaveChangesAsync();
        }

        public async Task UnpublishAsync(Guid id)
        {
            var survey = await _surveyRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Survey not found.");

            survey.IsPublished = false;
            survey.UpdatedAt = DateTime.UtcNow;

            await _surveyRepository.UpdateAsync(survey);
            await _surveyRepository.SaveChangesAsync();
        }

        private static SurveyResponseDto MapToDto(Survey s) => new()
        {
            Id = s.Id,
            Title = s.Title,
            Description = s.Description,
            Slug = s.Slug,
            IsPublished = s.IsPublished,
            ExpiryDate = s.ExpiryDate,
            CreatedAt = s.CreatedAt,
            UpdatedAt = s.UpdatedAt,
            Questions = s.Questions.Select(MapQuestion).ToList()
        };

        private static QuestionResponseDto MapQuestion(Question q) => new()
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
