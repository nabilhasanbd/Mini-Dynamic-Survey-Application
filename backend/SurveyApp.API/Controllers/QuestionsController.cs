using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.Application.DTOs.Question;
using SurveyApp.Application.Interfaces.Services;

namespace SurveyApp.API.Controllers
{
    [ApiController]
    [Route("api/surveys/{surveyId:guid}/questions")]
    [Authorize]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(Guid surveyId)
        {
            var questions = await _questionService.GetBySurveyIdAsync(surveyId);
            return Ok(questions);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guid surveyId, [FromBody] CreateQuestionDto dto)
        {
            var question = await _questionService.CreateAsync(surveyId, dto);
            return Ok(question);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid surveyId, Guid id, [FromBody] UpdateQuestionDto dto)
        {
            var question = await _questionService.UpdateAsync(id, dto);
            return Ok(question);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid surveyId, Guid id)
        {
            await _questionService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPatch("reorder")]
        public async Task<IActionResult> Reorder(Guid surveyId, [FromBody] List<Guid> orderedIds)
        {
            await _questionService.ReorderAsync(surveyId, orderedIds);
            return NoContent();
        }
    }
}
