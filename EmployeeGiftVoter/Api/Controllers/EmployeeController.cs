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
    }
}
