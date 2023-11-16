using Core.Dtos;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeesService _employeesService;
        public EmployeeController(IEmployeesService employeesService)
        {
            this._employeesService = employeesService;
        }
        [HttpGet("getAll")]
        [ProducesResponseType(typeof(List<ClientResponseDto>),StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = new List<ClientResponseDto>();
            try
            {
                result = await _employeesService.GetAll();
            }
            catch (Exception)
            {

                return BadRequest();
            }
            return Ok(result);
        }
        [HttpGet("getAllGifts")]
        [ProducesResponseType(typeof(List<GiftDto>),StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllGifts()
        {
            var result= new List<GiftDto>();
            try
            {
                result = await _employeesService.GetAllGifts();
            }
            catch (Exception)
            {

                BadRequest();
            }
            return Ok(result);
        }
    }
}
