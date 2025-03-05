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
using NegosudModel.Request;

namespace NegosudAPI.Controllers
{
    [Authorize]
    [Route("api/sales")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private ISaleService _saleService;

        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        // GET : api/sales/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SaleDto>> GetSale(int id)
        {
            SaleDto? saleDto = await _saleService.GetSaleDto(id);
            if (saleDto == null) return NotFound();
            return Ok(saleDto);
        }

        // GET : api/sales
        [HttpGet]
        public async Task<ActionResult<SaleDto>> GetSales()
        {
            IEnumerable<SaleDto> sales = await _saleService.GetSales();
            return Ok(sales);
        }

        // POST: api/sales
        [HttpPost()]
        public async Task<ActionResult<int>> CreateSale([FromBody] CreateSaleRequest request)
        {
            if (request == null) return BadRequest("Request data is null.");

            try
            {
                int saleId = await _saleService.CreateSaleWithArticles(request);
                return Ok(saleId);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/sales/cancel/{id}
        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> CancelSale(int id)
        {
            try
            {
                bool result = await _saleService.CancelSale(id);
                if (!result) return NotFound($"Sale with ID {id} not found.");
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/sales/receive/{id}
        [HttpPut("receive/{id}")]
        public async Task<IActionResult> ReceiveSale(int id)
        {
            try
            {
                bool result = await _saleService.ReceiveSale(id);
                if (!result) return NotFound($"Sale with ID {id} not found.");
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // DELETE : api/sales/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            try
            {
                bool result = await _saleService.DeleteSale(id);
                if (!result) return NotFound($"Sale with ID {id} not found.");
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
