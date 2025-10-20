using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SurveyBasket.Controllers
{
    [Route("api/polls/{pollId}/[controller]")]
    [ApiController]
    [Authorize]
    public class VoteController(IQuestionService questionService) : ControllerBase
    {
        private readonly IQuestionService QuestionService = questionService;

        [HttpGet]
        public async Task<IActionResult> Start([FromRoute]int pollId , CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            var result = await QuestionService.GetAvaildableAsync(pollId, userId!, cancellationToken);

            return result.IsSuccess?
                Ok(result.Value)
                : result.Error!.Equals(VoteErrors.DuplicateVotes())
                    ? result.ToProblem(StatusCodes.Status406NotAcceptable)
                    : result.ToProblem(StatusCodes.Status404NotFound);

        }
    }
}
