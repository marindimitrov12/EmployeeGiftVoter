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

                return BadRequest();
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
    }
}
