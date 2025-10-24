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

        [HttpGet("")]
        public async Task<IActionResult> GetAllAsync([FromRoute] int pollId, CancellationToken cancellation = default)
        {
            var result =  await QuestionService.GetAllAsync(pollId, cancellation);
            return result.IsSuccess
                ? Ok(result.Value)
                : result.ToProblem(StatusCodes.Status404NotFound);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int pollId, [FromRoute]int id, CancellationToken cancellation = default)
        {
            var result = await QuestionService.GetAsync(pollId, id, cancellation);

            return result.IsSuccess?
                Ok(result.Value)
                : result.ToProblem(StatusCodes.Status404NotFound);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddAsync([FromRoute] int pollId, [FromBody] QuestionRequest request, CancellationToken cancellation = default)
        {
            var result = await QuestionService.AddAsync(pollId, request, cancellation);

            if (result.IsSuccess)
               return CreatedAtAction(nameof(Get), new { pollId, result.Value.Id}, result.Value);
            
            return result.Error!.Equals(QuestionErrors.DuplicateQuestionContent())
                ? result.ToProblem(StatusCodes.Status409Conflict)
                : result.ToProblem(StatusCodes.Status404NotFound);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int pollId ,[FromRoute] int id ,[FromBody] QuestionRequest request, CancellationToken cancellation = default)
        {
            var result = await QuestionService.UpdateAsync(pollId, id, request, cancellation);
            return result.IsSuccess ?
                  NoContent() :
                  result.Error!.Equals(QuestionErrors.DuplicateQuestionContent()) ?
                      result.ToProblem(StatusCodes.Status409Conflict) :
                      result.ToProblem(StatusCodes.Status404NotFound);
        }


        [HttpPut("{id:int}/ToggleStatus")]
        public async Task<IActionResult> Update([FromRoute]int pollId ,[FromRoute] int id, CancellationToken cancellationToken)
        {
            var Result = await QuestionService.ToggleStatusAsync(pollId ,id, cancellationToken);

            return Result.IsSuccess ?
                  NoContent() :
                  Result.ToProblem(StatusCodes.Status404NotFound);

        }
    }
}
