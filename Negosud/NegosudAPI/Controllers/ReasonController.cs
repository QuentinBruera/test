using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NegosudAPI.Services.Interfaces;
using NegosudModel.Dto;

namespace NegosudAPI.Controllers
{
    [Route("api/reasons")]
    [ApiController]
    public class ReasonController : Controller
    {
        private IReasonService _reasonService;
        public ReasonController(IReasonService reasonService)
        {
            _reasonService = reasonService;
        }

        // GET: api/reasons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReasonDto>>> GetReasons()
        {
            IEnumerable<ReasonDto> reasons = await _reasonService.GetReasons();
            if (reasons == null || !reasons.Any())
            {
                return NotFound("No reasons found.");
            }
            return Ok(reasons);
        }
    }
}
