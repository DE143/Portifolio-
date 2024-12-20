using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Implimentation.Interfaces;
using PortfolioAPI.Infrastacture.DTOs;
using System.Net;

namespace PortfolioAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AboutUsController : ControllerBase
    {
        private readonly IContentsService _contentsService;
        public AboutUsController(IContentsService contentsService)
        {
            _contentsService = contentsService;  
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseData<AboutUsDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllAboutUsData()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            { 
                return Ok(await _contentsService.GetAllAboutUs());
            }
        
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseData<Guid>), (int)HttpStatusCode.OK)]
        public async Task <IActionResult> AddAboutUs( [FromForm] AboutUsPostDto aboutUs)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _contentsService.AddAboutUs(aboutUs));
            }
            else { return BadRequest(ModelState); }
        }
        [HttpGet]
        [ProducesResponseType(typeof(ResponseData<AboutUsDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAboutUsById(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
               return Ok(await _contentsService.GetAboutUsById(id));
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseData<Guid>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateAboutUs([FromForm] UpdateAboutUsDto aboutUs)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _contentsService.UpdateAboutUs(aboutUs));
            }
            else { return BadRequest(ModelState); }
        }


        [HttpDelete]
        [ProducesResponseType(typeof(ResponseData<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAboutUs(Guid id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _contentsService.DeleteAboutUs(id));
            }
            else 
            { 
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseData<AboutUsDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ApproveAboutUs(Guid id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _contentsService.ApproveAboutUs(id));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    }
}
