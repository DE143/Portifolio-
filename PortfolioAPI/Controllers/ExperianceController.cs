using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Implimentation.Interfaces;
using PortfolioAPI.Infrastacture.DTOs;
using System.Net;

namespace PortfolioAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExperianceController : ControllerBase
    {
        private readonly IExperianceService _experianceService;
        public ExperianceController( IExperianceService experianceService )
        {
            _experianceService = experianceService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseData<Guid>),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddExperiance(ExperianceDto experianceDto)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _experianceService.AddExperiance(experianceDto));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseData<List<ExperianceDto>>),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllExperiance()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _experianceService.GettAllExperiance());
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseData),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateExperiance(ExperianceDto experianceDto)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _experianceService.UpdateExperiance(experianceDto));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseData),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> ApproveExperinace(Guid Id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _experianceService.ApproveExperiance(Id));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        
        
        [HttpGet]
        [ProducesResponseType(typeof(ResponseData<List<ExperianceDto>>),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllApprovedExperiance()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _experianceService.GettAllApprovedExperiance());
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ResponseData), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteExperinace(Guid Id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _experianceService.DeleteExperiance(Id));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
