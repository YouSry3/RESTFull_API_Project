using Microsoft.AspNetCore.Mvc;



namespace ProjectRESTFullApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollsController(IPollService PollService) : ControllerBase
    {
        private readonly IPollService _PollService = PollService;


		[HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute]int id, CancellationToken cancellationToken )
        {
            var IsExist =await _PollService.GetAsync(id, cancellationToken);


            return IsExist == null ? NotFound() : Ok(IsExist);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken ) {
            var Polls = await _PollService.GetAllAsync(cancellationToken);
            var PollResponse = Polls.Adapt<List<PollResponse>>();

            return Ok(PollResponse);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody]PollRequest Request, CancellationToken cancellationToken)
        {
            var createdPoll =await _PollService.AddAsync(Request.Adapt<Poll>(), cancellationToken);
            return CreatedAtAction(nameof(Get), new { id = createdPoll.Id }, createdPoll.Adapt<PollResponse>());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute]int id, PollRequest Requestm,CancellationToken cancellationToken)
        {
            var isUpdate =await _PollService.UpdateAsync(id, Request.Adapt<Poll>(), cancellationToken);
           

            return !isUpdate ? NotFound() :NoContent();

		}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)=>
            await _PollService.DeleteAsync(id, cancellationToken) ? NoContent() : NotFound();



    }
}
