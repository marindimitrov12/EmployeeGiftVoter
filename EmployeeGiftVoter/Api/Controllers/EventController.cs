using Core.Dtos;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class EventController : Controller
    {
        private readonly IClientService _clientService;
        public EventController(IClientService clientService)
        {
            this._clientService = clientService;
        }
        [HttpPost("startEvent")]
        [ProducesResponseType(typeof(EventResponseDto),StatusCodes.Status200OK)]
        public async Task<IActionResult> StartEvent(CreateEvetDto ev)
        {
            var result=new EventResponseDto();
            try
            {
                result = await _clientService.StartEvent(ev);
            }
            catch (Exception)
            {

                return BadRequest(new {message="The event for this user is alredy started!"});
            }
            return Ok(result);

        }
        [HttpGet("getAllEvents")]
        [ProducesResponseType(typeof(List<EventResponseDto>),StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(int id)
        {var result=new List<EventResponseDto>();
            try
            {
                result = await _clientService.GetAllEvents(id);
            }
            catch (Exception)
            {
                return BadRequest();
                
            }
            return Ok(result);
        }
        [HttpPost("vote")]
        [ProducesResponseType(typeof(VoteResponseDto),StatusCodes.Status200OK)]
        public async Task<IActionResult> Vote(CreateVoteDto vote)
        {
            var result=new VoteResponseDto();
            try
            {
                result = await _clientService.Vote(vote);
            }
            catch (Exception ex)
            {
                return BadRequest(new {message=ex.Message });
                
            }
            return Ok(result);
        }
        [HttpPut("closeEvent")]
        [ProducesResponseType(typeof(CloseEventDto),StatusCodes.Status200OK)]
        public async Task<IActionResult> CloseEvent(CloseEventDto dto)
        {
            var result = new CloseEventDto();
            try
            {
                result = await _clientService.CloseEvent(dto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok(result);
        }
        [HttpGet("getResult")]
        [ProducesResponseType(typeof(List<ResultDto>),StatusCodes.Status200OK)]
        public async Task<IActionResult> GetResult(ResultRequestDto result)
        {
            var res = new List<ResultDto>();
            try
            {
                res = await _clientService.GetResults(result);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message=ex.Message});
            }
            return Ok(res);

        }
        [HttpGet("trackVoting")]
        [ProducesResponseType(typeof(List<TrackVotingDto>),StatusCodes.Status200OK)]
        public async Task<IActionResult> TrackVoting(ResultRequestDto result)
        {
            var res = new List<TrackVotingDto>();
            try
            {
                res = await _clientService.TrackVoting(result);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message=ex.Message});
            }
            return Ok(res);

        }
    }
}
