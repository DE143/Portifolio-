using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Implimentation.Interfaces;
using PortfolioAPI.Infrastacture.DTOs;
using System.Net;

namespace PortfolioAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private IEducationService _educationService;
        public EducationController(IEducationService educationService)
        {
            _educationService = educationService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseData<Guid>),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddEducation(EducationGetDto educationGetDto)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _educationService.AddEducation(educationGetDto));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseData<EducationGetDto>),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllEducation()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _educationService.GetAllEducation());
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseData<Guid>),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateEducation(EducationGetDto educationGetDto)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _educationService.UpdateEducation(educationGetDto));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ResponseData),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteEducation(Guid Id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _educationService.DeleteEducation(Id));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseData<Guid>),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> ApproveEducation(EducationGetDto educationGetDto)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _educationService.ApproveEducation(educationGetDto));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseData<EducationGetDto>),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllApprovedEducation()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _educationService.GetAllApprovedEducation());

            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
