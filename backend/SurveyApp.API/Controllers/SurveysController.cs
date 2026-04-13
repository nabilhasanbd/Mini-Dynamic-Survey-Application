using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Application.DTOs.Survey;
using SurveyApp.Application.Interfaces.Services;
using System.Security.Claims;

namespace SurveyApp.API.Controllers
{
    [ApiController]
    [Route("api/surveys")]
    [Authorize]
    public class SurveysController : ControllerBase
    {
        private readonly ISurveyService _surveyService;

        public SurveysController(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }

        private Guid GetUserId() =>
            Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var surveys = await _surveyService.GetAllByUserAsync(GetUserId());
            return Ok(surveys);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var survey = await _surveyService.GetByIdAsync(id);
            return survey is null ? NotFound() : Ok(survey);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSurveyDto dto)
        {
            var survey = await _surveyService.CreateAsync(GetUserId(), dto);
            return CreatedAtAction(nameof(GetById), new { id = survey.Id }, survey);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSurveyDto dto)
        {
            var survey = await _surveyService.UpdateAsync(id, dto);
            return Ok(survey);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _surveyService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPatch("{id:guid}/publish")]
        public async Task<IActionResult> Publish(Guid id)
        {
            await _surveyService.PublishAsync(id);
            return NoContent();
        }

        [HttpPatch("{id:guid}/unpublish")]
        public async Task<IActionResult> Unpublish(Guid id)
        {
            await _surveyService.UnpublishAsync(id);
            return NoContent();
        }

        [HttpGet("slug/{slug}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            var survey = await _surveyService.GetBySlugAsync(slug);
            return survey is null ? NotFound() : Ok(survey);
        }
    }
}
