using Microsoft.AspNetCore.Mvc;
using NegosudModel.Dto;
using NegosudAPI.Services.Interfaces;
using NegosudAPI.Services.Implementations;
using Microsoft.AspNetCore.Authorization;
using NegosudModel.Request;

namespace NegosudAPI.Controllers
{
    [Authorize]
    [Route("api/purchases")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private IPurchaseService _purchaseService;

        public PurchasesController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        // GET : api/purchases/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseDto>> GetPurchase(int id)
        {
            PurchaseDto? purchaseDto = await _purchaseService.GetPurchaseDto(id);
            if (purchaseDto == null) return NotFound();
            return Ok(purchaseDto);
        }

        // GET : api/purchases
        [HttpGet]
        public async Task<ActionResult<PurchaseDto>> GetPurchases()
        {
            IEnumerable<PurchaseDto> purchases = await _purchaseService.GetPurchases();
            return Ok(purchases);
        }

        // GET : api/purchases/receptions
        [HttpGet("receptions")]
        public async Task<ActionResult<PurchaseDto>> GetReceptions()
        {
            IEnumerable<PurchaseDto> receptions = await _purchaseService.GetReceptions();
            return Ok(receptions);
        }

        // POST: api/purchases
        [HttpPost()]
        public async Task<ActionResult<int>> CreatePurchase([FromBody] CreatePurchaseRequest request)
        {
            if (request == null) return BadRequest("Request data is null.");

            try
            {
                int purchaseId = await _purchaseService.CreatePurchaseWithArticles(request);
                return Ok(purchaseId);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/purchases/restocking/{articleId}
        [HttpPost("restocking/{articleId}")]
        public async Task<ActionResult<int>> CreatePurchaseRestocking(int articleId)
        {
            try
            {
                int purchaseId = await _purchaseService.CreatePurchaseRestocking(articleId);
                return Ok(purchaseId);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        // PUT: api/purchases/cancel/{id}
        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> CancelPurchase(int id)
        {
            try
            {
                bool result = await _purchaseService.CancelPurchase(id);
                if (!result) return NotFound($"Purchase with ID {id} not found.");
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/purchases/receive/{id}
        [HttpPut("receive/{id}")]
        public async Task<IActionResult> ReceivePurchase(int id)
        {
            try
            {
                bool result = await _purchaseService.ReceivePurchase(id);
                if (!result) return NotFound($"Purchase with ID {id} not found.");
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE : api/purchases/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchase(int id)
        {
            try
            {
                bool result = await _purchaseService.DeletePurchase(id);
                if (!result) return NotFound($"Purchase with ID {id} not found.");
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
