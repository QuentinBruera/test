using Microsoft.AspNetCore.Mvc;
using NegosudModel.Dto;
using NegosudAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using NegosudAPI.Services.Implementations;
using NegosudModel.Request;


namespace NegosudAPI.Controllers
{
    [Authorize]
    [Route("api/suppliers")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private ISupplierService _supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        // GET: api/Suppliers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDto>>> GetSuppliers()
        {
            IEnumerable<SupplierDto> suppliers = await _supplierService.GetSuppliers();
            return Ok(suppliers);
        }

        // GET: api/Suppliers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDto>> GetSupplier(int id)
        {

            SupplierDto? supplierDto = await _supplierService.GetSupplierDto(id);
            if (supplierDto == null) return NotFound();
            return Ok(supplierDto);
        }

        // POST: api/suppliers
        [HttpPost()]
        public async Task<ActionResult<int>> CreateSupplier([FromBody] CreateUpdateSupplierRequest request)
        {
            if (request == null) return BadRequest("Request data is null.");

            try
            {
                int supplierId = await _supplierService.CreateSupplier(request);
                return Ok(supplierId);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/suppliers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, [FromBody] CreateUpdateSupplierRequest request)
        {
            try
            {
                bool result = await _supplierService.UpdateSupplier(id, request);
                if (!result) return NotFound($"Supplier with ID {id} not found.");
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/suppliers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            try
            {
                bool result = await _supplierService.DeleteSupplier(id);
                if (!result) return NotFound($"Supplier with ID {id} not found.");
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
