using Microsoft.AspNetCore.Mvc;
using NegosudModel.Dto;
using NegosudAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace NegosudAPI.Controllers
{
    [Authorize]
    [Route("api/articles")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        // GET : api/articles/details
        [HttpGet("details")]
        public async Task<ActionResult<IEnumerable<ArticleDetailsDto>>> GetArticlesDetails()
        {
            IEnumerable<ArticleDetailsDto> articles = await _articleService.GetArticlesDetails();
            if (articles == null || !articles.Any()) return NotFound();
            return Ok(articles);
        }

        // GET : api/articles/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDto>> GetArticle(int id)
        {

            ArticleDto? articleDto = await _articleService.GetArticleDto(id);
            if (articleDto == null) return NotFound();
            return Ok(articleDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleDto>>> GetArticles([FromQuery] int? supplierId, [FromQuery] string? name)
        {
            // GET : api/articles?supplierId=""
            if (supplierId.HasValue)
            {
                IEnumerable<ArticleDto> articlesBySupplier = await _articleService.GetArticlesBySupplierId(supplierId.Value);
                if (articlesBySupplier == null || !articlesBySupplier.Any()) return NotFound();
                return Ok(articlesBySupplier);
            }

            // GET : api/articles?name=""
            if (!string.IsNullOrEmpty(name))
            {
                ArticleDto? articleByName = await _articleService.GetArticleByName(name);
                if (articleByName == null) return NotFound($"Article with name '{name}' not found.");
                return Ok(articleByName);
            }

            // GET : api/articles
            IEnumerable<ArticleDto> articles = await _articleService.GetArticles();
            return Ok(articles);
        }


        // GET : api/articles/restocking
        [HttpGet("restocking")]
        public async Task<ActionResult<IEnumerable<ArticleDto>>> GetArticlesForRestocking()
        {
            IEnumerable<ArticleDto> articles = await _articleService.GetArticlesForRestocking();
            if (articles == null || !articles.Any()) return NotFound();
            return Ok(articles);
        }

        // PUT : api/articles/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticle(int id, [FromBody] ArticleDto articleDto)
        {
            if (articleDto == null || id != articleDto.Id) return BadRequest("Invalid data.");
            bool isUpdated = await _articleService.UpdateArticle(id, articleDto);
            if (!isUpdated) return NotFound($"Article with ID {id} not found.");
            return NoContent();
        }

        // POST: api/articles
        [HttpPost]
        public async Task<ActionResult<ArticleDto>> CreateArticle([FromBody] ArticleDto articleDto)
        {
            if (articleDto == null) return BadRequest("Article data is null.");
            
            try
            {
                ArticleDto createdArticle = await _articleService.CreateArticle(articleDto);
                return CreatedAtAction(nameof(GetArticle), new { id = createdArticle.Id }, createdArticle);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
