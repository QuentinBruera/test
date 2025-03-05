using Microsoft.AspNetCore.Mvc;
using NegosudModel.Dto;
using NegosudAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace NegosudAPI.Controllers
{
    [Authorize]
    [Route("api/articleOrders")]
    [ApiController]
    public class ArticleOrdersController : ControllerBase
    {
        private IArticleOrderService _articleOrderService;

        public ArticleOrdersController(IArticleOrderService articleOrderService)
        {
            _articleOrderService = articleOrderService;
        }

        // GET: api/articleOrders/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleOrderDto>> GetArticleOrder(int id)
        {
            ArticleOrderDto? articleOrderDto = await _articleOrderService.GetArticleOrder(id);
            if (articleOrderDto == null) return NotFound();
            return Ok(articleOrderDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleOrderDto>>> GetArticleOrders([FromQuery] int? purchaseId)
        {
            // GET : api/articleOrders?purchaseId=""
            if (purchaseId.HasValue)
            {
                IEnumerable<ArticleOrderDto> articleOrdersByPurchase = await _articleOrderService.GetArticleOrderDtoByOrderId(purchaseId.Value);
                if (articleOrdersByPurchase == null || !articleOrdersByPurchase.Any())
                {
                    return NotFound($"No article orders found for purchase ID {purchaseId}");
                }
                return Ok(articleOrdersByPurchase);
            }

            return BadRequest("No valid query parameters provided.");
        }
    }
}
