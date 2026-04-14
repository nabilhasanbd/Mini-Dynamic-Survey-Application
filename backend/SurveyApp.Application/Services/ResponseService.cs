using SurveyApp.Application.DTOs.Response;
using SurveyApp.Application.Interfaces.Repositories;
using SurveyApp.Application.Interfaces.Services;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Application.Services
{
    public class ResponseService : IResponseService
    {
        private readonly IResponseRepository _responseRepository;
        private readonly ISurveyRepository _surveyRepository;

        public ResponseService(IResponseRepository responseRepository, ISurveyRepository surveyRepository)
        {
            _responseRepository = responseRepository;
            _surveyRepository = surveyRepository;
        }

        public async Task<Guid> SubmitAsync(string slug, SubmitResponseDto dto)
        {
            var survey = await _surveyRepository.GetBySlugAsync(slug)
                ?? throw new KeyNotFoundException("Survey not found.");

            if (!survey.IsPublished)
                throw new InvalidOperationException("Survey is not accepting responses.");

            if (survey.ExpiryDate.HasValue && survey.ExpiryDate < DateTime.UtcNow)
                throw new InvalidOperationException("Survey has expired.");

            var response = new Response
            {
                Id = Guid.NewGuid(),
                SurveyId = survey.Id,
                SubmittedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Answers = dto.Answers.Select(a => new Answer
                {
                    Id = Guid.NewGuid(),
                    QuestionId = a.QuestionId,
                    AnswerText = a.AnswerText,
                    SelectedOptionIds = a.SelectedOptionIds,
                    RatingValue = a.RatingValue,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }).ToList()
            };

            await _responseRepository.AddAsync(response);
            await _responseRepository.SaveChangesAsync();

            return response.Id;
        }

        public async Task<IEnumerable<ResponseDetailDto>> GetBySurveyIdAsync(Guid surveyId)
        {
            var responses = await _responseRepository.GetBySurveyIdAsync(surveyId);
            return responses.Select(r => new ResponseDetailDto
            {
                Id = r.Id,
                SubmittedAt = r.SubmittedAt,
                Answers = r.Answers.Select(a => new AnswerDetailDto
                {
                    QuestionId = a.QuestionId,
                    QuestionText = a.Question?.QuestionText ?? string.Empty,
                    AnswerText = a.AnswerText,
                    SelectedOptionIds = a.SelectedOptionIds,
                    RatingValue = a.RatingValue
                }).ToList()
            });
        }
    }
}
