using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NegosudAPI.Services.Interfaces;
using NegosudModel.Dto;
using NegosudModel.Request;

namespace NegosudAPI.Controllers
{
    [Authorize]
    [Route("api/inventories")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        private IInventoryService _inventoryService;

        public InventoriesController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        // GET : api/inventories/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryDto>> GetInventory(int id)
        {
            InventoryDto? inventoryDto = await _inventoryService.GetInventoryDto(id);
            if (inventoryDto == null) return NotFound();
            return Ok(inventoryDto);
        }

        // GET : api/inventories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryDto>>> GetInventories()
        {
            IEnumerable<InventoryDto> inventories = await _inventoryService.GetInventories();
            return Ok(inventories);
        }

        // POST: api/inventories
        [HttpPost()]
        public async Task<ActionResult<int>> CreateInventory([FromBody] CreateInventoryRequest request)
        {
            if (request == null) return BadRequest("Request data is null.");

            try
            {
                int inventoryId = await _inventoryService.CreateInventoryWithArticles(request);
                return Ok(inventoryId);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE : api/inventories/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            try
            {
                bool result = await _inventoryService.DeleteInventory(id);
                if (!result) return NotFound($"Inventory with ID {id} not found.");
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
