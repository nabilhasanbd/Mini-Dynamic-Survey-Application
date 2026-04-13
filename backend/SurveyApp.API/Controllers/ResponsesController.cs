using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Application.DTOs.Response;
using SurveyApp.Application.Interfaces.Services;

namespace SurveyApp.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class ResponsesController : ControllerBase
    {
        private readonly IResponseService _responseService;

        public ResponsesController(IResponseService responseService)
        {
            _responseService = responseService;
        }

        [HttpPost("s/{slug}/respond")]
        [AllowAnonymous]
        public async Task<IActionResult> Submit(string slug, [FromBody] SubmitResponseDto dto)
        {
            var responseId = await _responseService.SubmitAsync(slug, dto);
            return Ok(new { responseId });
        }

        [HttpGet("surveys/{surveyId:guid}/responses")]
        [Authorize]
        public async Task<IActionResult> GetBySurvey(Guid surveyId)
        {
            var responses = await _responseService.GetBySurveyIdAsync(surveyId);
            return Ok(responses);
        }
    }
}
