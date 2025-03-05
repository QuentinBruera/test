using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NegosudAPI.Services.Interfaces;
using NegosudModel.Dto;

namespace NegosudAPI.Controllers
{
    [Authorize]
    [Route("api/families")]
    [ApiController]
    public class FamiliesController : ControllerBase
    {
       private IFamilyService _familyService;

        public FamiliesController(IFamilyService familyService)
        {
            _familyService = familyService;
        }

        // GET: api/families
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FamilyDto>>> GetFamilies()
        {
            IEnumerable<FamilyDto> families = await _familyService.GetFamilies();
            return Ok(families);
        }

        // GET: api/families/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<FamilyDto>> GetFamily(int id)
        {
            FamilyDto? familyDto = await _familyService.GetFamily(id);
            if (familyDto == null) return NotFound();
            return Ok(familyDto);
        }
    }
}
