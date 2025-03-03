using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dto;

namespace API.Controllers
{
    [Authorize]
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            IEnumerable<EmployeeDto> employees = await _employeeService.GetEmployees();
            return Ok(employees);
        }

        // GET: api/employees/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            EmployeeDto employee = await _employeeService.GetEmployee(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        // GET: api/employees/search?term={searchTerm}
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> SearchEmployees([FromQuery] string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return await GetEmployees();
            }

            IEnumerable<EmployeeDto> employees = await _employeeService.SearchEmployees(term);
            return Ok(employees);
        }

        // GET: api/employees/site/{siteId}
        [HttpGet("site/{siteId}")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesBySite(int siteId)
        {
            IEnumerable<EmployeeDto> employees = await _employeeService.GetEmployeesBySite(siteId);
            return Ok(employees);
        }

        // GET: api/employees/department/{departmentId}
        [HttpGet("department/{departmentId}")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesByDepartment(int departmentId)
        {
            IEnumerable<EmployeeDto> employees = await _employeeService.GetEmployeesByDepartment(departmentId);
            return Ok(employees);
        }

        // POST: api/employees
        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> PostEmployee(EmployeeDto employeeDto)
        {
            if (employeeDto == null) return BadRequest("Les données de l'employé sont nulles.");

            EmployeeDto newEmployeeDto = await _employeeService.AddEmployee(employeeDto);
            return CreatedAtAction(nameof(GetEmployee), new { id = newEmployeeDto.Id }, newEmployeeDto);
        }

        // PUT: api/employees/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, EmployeeDto employeeDto)
        {
            if (id != employeeDto.Id)
            {
                return BadRequest("L'ID de l'employé ne correspond pas à l'ID de la route.");
            }

            bool result = await _employeeService.UpdateEmployee(employeeDto);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/employees/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            bool result = await _employeeService.DeleteEmployee(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}