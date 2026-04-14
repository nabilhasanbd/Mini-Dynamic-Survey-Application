using System.Text;
using SurveyApp.Application.DTOs.Analytics;
using SurveyApp.Application.Interfaces.Repositories;
using SurveyApp.Application.Interfaces.Services;
using SurveyApp.Domain.Enums;

namespace SurveyApp.Application.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IResponseRepository _responseRepository;

        public AnalyticsService(ISurveyRepository surveyRepository, IResponseRepository responseRepository)
        {
            _surveyRepository = surveyRepository;
            _responseRepository = responseRepository;
        }

        public async Task<SurveyAnalyticsDto> GetSurveyAnalyticsAsync(Guid surveyId)
        {
            var survey = await _surveyRepository.GetWithQuestionsAsync(surveyId)
                ?? throw new KeyNotFoundException("Survey not found.");

            var responses = (await _responseRepository.GetBySurveyIdAsync(surveyId)).ToList();
            var allAnswers = responses.SelectMany(r => r.Answers).ToList();

            var questionAnalytics = survey.Questions
                .OrderBy(q => q.OrderIndex)
                .Select(q =>
                {
                    var qAnswers = allAnswers.Where(a => a.QuestionId == q.Id).ToList();
                    var analytics = new QuestionAnalyticsDto
                    {
                        QuestionId = q.Id,
                        QuestionText = q.QuestionText,
                        QuestionType = q.QuestionType.ToString(),
                        TotalAnswers = qAnswers.Count
                    };

                    if (q.QuestionType == QuestionType.Rating)
                    {
                        var ratings = qAnswers
                            .Where(a => a.RatingValue.HasValue)
                            .Select(a => (double)a.RatingValue!.Value)
                            .ToList();
                        analytics.AverageRating = ratings.Count > 0 ? Math.Round(ratings.Average(), 2) : null;
                    }
                    else if (q.QuestionType is QuestionType.MultipleChoice
                             or QuestionType.Checkboxes
                             or QuestionType.Dropdown)
                    {
                        var allSelections = qAnswers
                            .Where(a => a.SelectedOptionIds != null)
                            .SelectMany(a => a.SelectedOptionIds!)
                            .ToList();

                        analytics.OptionCounts = q.QuestionOptions
                            .OrderBy(o => o.OrderIndex)
                            .Select(o =>
                            {
                                var count = allSelections.Count(id => id == o.Id);
                                return new OptionCountDto
                                {
                                    OptionId = o.Id,
                                    OptionText = o.OptionText,
                                    Count = count,
                                    Percentage = qAnswers.Count > 0
                                        ? Math.Round((double)count / qAnswers.Count * 100, 1)
                                        : 0
                                };
                            }).ToList();
                    }

                    return analytics;
                }).ToList();

            return new SurveyAnalyticsDto
            {
                SurveyId = survey.Id,
                Title = survey.Title,
                TotalResponses = responses.Count,
                Questions = questionAnalytics
            };
        }

        public async Task<byte[]> ExportResponsesToCsvAsync(Guid surveyId)
        {
            var survey = await _surveyRepository.GetWithQuestionsAsync(surveyId)
                ?? throw new KeyNotFoundException("Survey not found.");

            var responses = (await _responseRepository.GetBySurveyIdAsync(surveyId)).ToList();
            var questions = survey.Questions.OrderBy(q => q.OrderIndex).ToList();

            var sb = new StringBuilder();

            // Header
            sb.Append("Response ID,Submitted At");
            foreach (var q in questions)
                sb.Append($",\"{q.QuestionText.Replace("\"", "\"\"")}\"");
            sb.AppendLine();

            // Rows
            foreach (var r in responses)
            {
                sb.Append($"{r.Id},{r.SubmittedAt:yyyy-MM-dd HH:mm:ss}");
                foreach (var q in questions)
                {
                    var answer = r.Answers.FirstOrDefault(a => a.QuestionId == q.Id);
                    var value = "";
                    if (answer != null)
                    {
                        if (answer.RatingValue.HasValue)
                            value = answer.RatingValue.Value.ToString();
                        else if (answer.SelectedOptionIds is { Count: > 0 })
                        {
                            var texts = q.QuestionOptions
                                .Where(o => answer.SelectedOptionIds.Contains(o.Id))
                                .Select(o => o.OptionText);
                            value = string.Join("; ", texts);
                        }
                        else if (answer.AnswerText != null)
                            value = answer.AnswerText;
                    }
                    sb.Append($",\"{value.Replace("\"", "\"\"")}\"");
                }
                sb.AppendLine();
            }

            return Encoding.UTF8.GetBytes(sb.ToString());
        }
    }
}
