using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Dto;
using System.Collections.Generic;

namespace API.Controllers
{
    [Authorize]
    [Route("api/sites")]
    [ApiController]
    public class SiteController : Controller
    {
        private SiteService _siteService;

        public SiteController(SiteService siteService)
        {
            _siteService = siteService;
        }

        // GET: api/sites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SiteDto>>> GetSites()
        {
            IEnumerable<SiteDto> sites = await _siteService.GetSites();
            return Ok(sites);
        }

        // GET : api/sites/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SiteDto>> GetSite(int id) 
        {
            SiteDto site = await _siteService.GetSite(id);
            if (site == null)
            {
                return NotFound();
            }
            return Ok(site);
        }


        // POST: api/sites
        [HttpPost]
        public async Task<ActionResult<SiteDto>> PostSite(SiteDto siteDto)
        {
            if (siteDto == null) return BadRequest("Site data is null.");

            SiteDto newSiteDto = await _siteService.AddSite(siteDto);
            return CreatedAtAction(nameof(GetSite), new { id = newSiteDto.Id }, newSiteDto);
        }
    }
}
