using NegosudAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NegosudModel.Dto;
using NegosudModel.Request;

namespace NegosudAPI.Controllers
{
    [Authorize]
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {
            IEnumerable<CustomerDto> customers = await _customerService.GetCustomers();
            return Ok(customers);
        }

        // GET: api/customers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {

            CustomerDto? customerDto = await _customerService.GetCustomerDto(id);
            if (customerDto == null) return NotFound();
            return Ok(customerDto);
        }

        // POST: api/customers
        [HttpPost()]
        public async Task<ActionResult<int>> CreateCustomer([FromBody] CreateUpdateCustomerRequest request)
        {
            if (request == null) return BadRequest("Request data is null.");

            try
            {
                int customerId = await _customerService.CreateCustomer(request);
                return Ok(customerId);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/customers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CreateUpdateCustomerRequest request)
        {
            try
            {
                bool result = await _customerService.UpdateCustomer(id, request);
                if (!result) return NotFound($"Customer with ID {id} not found.");
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/customers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                bool result = await _customerService.DeleteCustomer(id);
                if (!result) return NotFound($"Customer with ID {id} not found.");
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
