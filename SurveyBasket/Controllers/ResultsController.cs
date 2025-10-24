using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SurveyBasket.Controllers
{
    [Route("api/polls/{pollId}/[controller]")]
    [ApiController]
    [Authorize]
    public class ResultsController(IResultService resultService) : ControllerBase
    {
        private readonly IResultService _ResultService = resultService;

        [HttpGet("row-data")]
        public async Task<IActionResult> PollVotes([FromRoute]int pollId, CancellationToken cancellationToken)
        {
            var result = await _ResultService.GetPollVotesAsync(pollId, cancellationToken);

            return result.IsSuccess?
                Ok(result.Value):
                result.ToProblem(StatusCodes.Status404NotFound)
                ;
        }
    

            [HttpGet("votes-per-day")]
        public async Task<IActionResult> VotesPerDay([FromRoute] int pollId, CancellationToken cancellationToken)
        {
            var result = await _ResultService.GetVotesPerDayAsync(pollId, cancellationToken);

            return result.IsSuccess ?
                Ok(result.Value) :
                result.ToProblem(StatusCodes.Status404NotFound)
                ;
        }
        [HttpGet("votes-per-question")]
        public async Task<IActionResult> VotesPerQuestion([FromRoute] int pollId, CancellationToken cancellationToken)
        {
            var result = await _ResultService.GetVotesPerQuestionAsync(pollId, cancellationToken);

            return result.IsSuccess ?
                Ok(result.Value) :
                result.ToProblem(StatusCodes.Status404NotFound)
                ;
        }
    }
}
