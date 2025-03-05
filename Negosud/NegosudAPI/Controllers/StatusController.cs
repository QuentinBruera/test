using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NegosudAPI.Services.Interfaces;
using NegosudModel.Context;
using NegosudModel.Dto;
using NegosudModel.Entities;

namespace NegosudAPI.Controllers
{
    [Authorize]
    [Route("api/status")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        // GET: api/status/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<StatusDto>> GetStatus(int id)
        {
            StatusDto? statusDto = await _statusService.GetStatus(id);
            if (statusDto == null) return NotFound();
            return Ok(statusDto);
        }
    }
}
