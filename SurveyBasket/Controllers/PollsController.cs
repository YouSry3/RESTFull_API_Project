using Microsoft.AspNetCore.Mvc;
using SurveyBasket.Services;
using SurveyBasket.Contract.Requests;
using SurveyBasket.Contract.Responses;


namespace ProjectRESTFullApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollsController(IPollService PollService) : ControllerBase
    {
        private readonly IPollService _PollService = PollService;


		[HttpGet("{id:int}")]
        public IActionResult Get([FromRoute]int id)
        {
            var IsExist = _PollService.Get(id);


            return IsExist == null ? NotFound() : Ok(IsExist);
        }

        [HttpGet]
        public IActionResult GetAll() {
            var response = _PollService.GetAll().Select(p => p.Adapt<PollResponse>());

            return Ok(response);
        }


        [HttpPost]
        public IActionResult Add([FromBody]PollRequest Request)
        {
            var createdPoll = _PollService.Add(Request.Adapt<Poll>());
            return CreatedAtAction(nameof(Get), new { id = createdPoll.Id }, createdPoll.Adapt<PollResponse>());
        }

        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute]int id, PollRequest Request)
        {
            var isUpdate = _PollService.Update(id, Request.Adapt<Poll>());
           

            return !isUpdate ? NotFound() :NoContent();

		}
        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)=> _PollService.Delete(id) ? NoContent() : NotFound();


     



    }
}
