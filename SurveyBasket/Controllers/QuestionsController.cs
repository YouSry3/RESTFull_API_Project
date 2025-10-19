using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace SurveyBasket.Controllers
{
    [Route("api/polls/{pollId}/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionsController(IQuestionService questionService) : ControllerBase
    {
        private readonly IQuestionService QuestionService = questionService;

        [HttpGet("{id}")]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost("")]
        public async Task<IActionResult> AddAsync([FromRoute] int pollId, [FromBody] QuestionRequest request, CancellationToken cancellation = default)
        {
            var result = await QuestionService.AddAsync(pollId, request, cancellation);

            if (result.IsSuccess)
               return CreatedAtAction(nameof(Get), new { pollId, result.Value.Id}, result.Value);
            
            return result.Error!.Equals(QuestionErrors.DuplicateQuestionContent)
                ? result.ToProblem(StatusCodes.Status409Conflict)
                : result.ToProblem(StatusCodes.Status404NotFound);
        }
    }
}
