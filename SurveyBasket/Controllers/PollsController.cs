using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;




namespace ProjectRESTFullApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class PollsController(IPollService PollService) : ControllerBase
    {
        private readonly IPollService _PollService = PollService;


		[HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute]int id, CancellationToken cancellationToken )
        {
            var Result = await _PollService.GetAsync(id, cancellationToken);


            return Result.IsSuccess ? 
                Ok(Result.Value) : 
                NotFound(Result.Error);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken ) {
            var Polls = await _PollService.GetAllAsync(cancellationToken);
            var PollResponse = Polls.Adapt<List<PollResponse>>();

            return Ok(PollResponse);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PollRequest Request, CancellationToken cancellationToken)
        {
            var Result = await _PollService.AddAsync(Request, cancellationToken);
            return CreatedAtAction(nameof(Get), new { id = Result.Value.Id }, Result.Value);
        }

        [HttpPost("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, PollRequest Request, CancellationToken cancellationToken)
        {
            var Result = await _PollService.UpdateAsync(id, Request, cancellationToken);


            return Result.IsFailure ? NotFound(Result.Error) : NoContent();

        }

        [HttpPut("{id:int}/TogglePublish")]
        public async Task<IActionResult> Update([FromRoute] int id, CancellationToken cancellationToken)
        {
            var Result = await _PollService.TogglePublishAsync(id, cancellationToken);

            return Result.IsFailure ? NotFound() : NoContent();

        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {

            var Result = await _PollService.DeleteAsync(id, cancellationToken);
            
            return Result.IsSuccess? NoContent() : NotFound();
        }



    }
}
