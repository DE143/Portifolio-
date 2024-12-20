using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Implimentation.Interfaces;
using PortfolioAPI.Implimentation.Services;
using PortfolioAPI.Infrastacture.DTOs;
using System.Net;

namespace PortfolioAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PortifolioController : ControllerBase
    {
        private IPortifolioService _portifolioService;
        public PortifolioController( IPortifolioService portifolioService )
        {
            _portifolioService = portifolioService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseData<Guid>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddPortifolio([FromForm] PortifolioDto portifolioDto)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _portifolioService.AddPortifolio(portifolioDto));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpGet]
        [ProducesResponseType(typeof(ResponseData<List<PortifolioDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllPortifolio()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _portifolioService.GetAllPortifolio());
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPut]
        [ProducesResponseType(typeof(ResponseData<Guid>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdatePortifolio([FromForm] PortifolioDto portifolioDto)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _portifolioService.UpdatePortifolio(portifolioDto));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPut]
        [ProducesResponseType(typeof(ResponseData<Guid>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ApprovePortifolio(Guid Id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _portifolioService.ApprovePortifolio(Id));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpGet]
        [ProducesResponseType(typeof(ResponseData<List<PortifolioDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllApprovedPortifolio()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _portifolioService.GetAllApprovedPortifolio());
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpDelete]
        [ProducesResponseType(typeof(ResponseData), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeletePortifolio(Guid Id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _portifolioService.DeletePortifolio(Id));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    }
}
