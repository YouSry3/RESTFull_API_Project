using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Contract.Vote;
using SurveyBasket.Services.Votes;

namespace SurveyBasket.Controllers
{
    [Route("api/polls/{pollId}/[controller]")]
    [ApiController]
    [Authorize]
    public class VoteController(
        IQuestionService questionService
        , IVoteService voteService) : ControllerBase
    {
        private readonly IQuestionService QuestionService = questionService;

        public IVoteService VoteService { get; } = voteService;

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



        [HttpPost("")]
        public async Task<IActionResult> Vote([FromRoute] int pollId, [FromBody]VoteRequest request, CancellationToken cancellationToken)
        {
            var result = await VoteService.AddAsync(pollId, User.GetUserId()!, request, cancellationToken);
            if (result.IsSuccess)
                return Created();

            return result.Error!.Equals(VoteErrors.InvalidQuestionsInVote())
                ? result.ToProblem(StatusCodes.Status406NotAcceptable)
                : result.ToProblem(StatusCodes.Status404NotFound);
        }


    }
}
