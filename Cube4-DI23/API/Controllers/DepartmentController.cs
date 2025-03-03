using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dto;

namespace API.Controllers
{
    [Authorize]
    [Route("api/departments")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly DepartmentService _departmentService;

        public DepartmentController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // GET: api/departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetDepartments()
        {
            IEnumerable<DepartmentDto> departments = await _departmentService.GetDepartments();
            return Ok(departments);
        }

        // GET: api/departments/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDto>> GetDepartment(int id)
        {
            DepartmentDto department = await _departmentService.GetDepartment(id);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        // POST: api/departments
        [HttpPost]
        public async Task<ActionResult<DepartmentDto>> PostDepartment(DepartmentDto departmentDto)
        {
            if (departmentDto == null) return BadRequest("Les données du département sont nulles.");

            DepartmentDto newDepartmentDto = await _departmentService.AddDepartment(departmentDto);
            return CreatedAtAction(nameof(GetDepartment), new { id = newDepartmentDto.Id }, newDepartmentDto);
        }

        // PUT: api/departments/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, DepartmentDto departmentDto)
        {
            if (id != departmentDto.Id)
            {
                return BadRequest("L'ID du département ne correspond pas à l'ID de la route.");
            }

            bool result = await _departmentService.UpdateDepartment(departmentDto);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/departments/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            bool result = await _departmentService.DeleteDepartment(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}