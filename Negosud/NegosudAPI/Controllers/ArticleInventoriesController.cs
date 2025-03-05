using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NegosudAPI.Services.Interfaces;
using NegosudModel.Context;
using NegosudModel.Dto;
using NegosudModel.Entities;

namespace NegosudAPI.Controllers
{
    [Authorize]
    [Route("api/articleInventories")]
    [ApiController]
    public class ArticleInventoriesController : ControllerBase
    {
        private IArticleInventoryService _articleInventoryService;

        public ArticleInventoriesController(IArticleInventoryService articleInventoryService)
        {
            _articleInventoryService = articleInventoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleInventoryDto>>> GetArticleInventories([FromQuery] int? inventoryId)
        {
            // GET : api/articleInventories?inventoryId=""
            if (inventoryId.HasValue)
            {
                IEnumerable<ArticleInventoryDto> articleInventoriesByInventory = await _articleInventoryService.GetArticleInventoriesDtoByInventoryId(inventoryId.Value);
                if (articleInventoriesByInventory == null || !articleInventoriesByInventory.Any())
                {
                    return NotFound($"No article inventories found for inventory ID {inventoryId}");
                }
                return Ok(articleInventoriesByInventory);
            }

            return BadRequest("No valid query parameters provided.");
        }

        // GET : api/articleInventories/byArticleId?articleId=""
        [HttpGet("byArticleId")]
        public async Task<ActionResult<IEnumerable<ArticleInventoryDto>>> GetArticleInventoriesByArticleId([FromQuery] int? articleId)
        {
            if (articleId.HasValue)
            {
                IEnumerable<ArticleInventoryDto> articleInventoriesByArticle = await _articleInventoryService.GetArticleInventoriesDtoByArticleId(articleId.Value);
                if (articleInventoriesByArticle == null || !articleInventoriesByArticle.Any())
                {
                    return NotFound($"No article inventories found for article ID {articleId}");
                }
                return Ok(articleInventoriesByArticle);
            }

            return BadRequest("No valid query parameters provided.");
        }
    }
}
