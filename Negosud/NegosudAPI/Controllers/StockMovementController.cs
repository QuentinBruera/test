using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NegosudAPI.Services.Interfaces;
using NegosudModel.Dto;

namespace NegosudAPI.Controllers
{
    [Authorize]
    [Route("api/stockMovement")]
    [ApiController]
    public class StockMovementController : Controller
    {
        private IStockMovementService _stockMovementService;

        public StockMovementController(IStockMovementService stockMovementService)
        {
            _stockMovementService = stockMovementService;
        }

        // GET: api/stockMovement?articleId=""
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockMovementDto>>> GetStockMovements([FromQuery] int? articleId)
        {
            if (articleId.HasValue)
            {
                IEnumerable<StockMovementDto> stockMovementsByArticle = await _stockMovementService.GetStockMovementsByArticleId(articleId.Value);
                if (stockMovementsByArticle == null || !stockMovementsByArticle.Any()) return NotFound();
                return Ok(stockMovementsByArticle);
            }

            return BadRequest("No valid query parameters provided.");
        }

        // POST: api/stockMovement
        [HttpPost()]
        public async Task<ActionResult<StockMovementDto>> CreateStockMovement([FromBody] StockMovementDto stockMovementDto)
        {
            if (stockMovementDto == null) return BadRequest("Request data is null.");

            try
            {
                StockMovementDto createdStockMovement = await _stockMovementService.CreateStockMovement(stockMovementDto);
                return CreatedAtAction(nameof(CreateStockMovement), new { id = createdStockMovement.Id }, createdStockMovement);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
