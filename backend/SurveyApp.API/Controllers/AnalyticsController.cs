using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Application.Interfaces.Services;

namespace SurveyApp.API.Controllers
{
    [ApiController]
    [Route("api/surveys/{surveyId:guid}/analytics")]
    [Authorize]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;

        public AnalyticsController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAnalytics(Guid surveyId)
        {
            var analytics = await _analyticsService.GetSurveyAnalyticsAsync(surveyId);
            return Ok(analytics);
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportCsv(Guid surveyId)
        {
            var csv = await _analyticsService.ExportResponsesToCsvAsync(surveyId);
            return File(csv, "text/csv", $"survey-{surveyId}-responses.csv");
        }
    }
}
