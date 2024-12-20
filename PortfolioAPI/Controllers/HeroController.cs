using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Implimentation.Interfaces;
using PortfolioAPI.Infrastacture.DTOs;
using System.Net;

namespace PortfolioAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HeroController : ControllerBase
    {
        private IHeroService _heroService;
        public HeroController( IHeroService heroService )
        {
            _heroService = heroService;
        }
        [HttpPost]
        [ProducesResponseType(typeof(ResponseData<Guid>),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddHero ( [FromForm] HeroDto heroDto)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _heroService.AddHero(heroDto));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpGet]
        [ProducesResponseType(typeof(ResponseData<List<HeroDto>>),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllHero()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _heroService.GetAllHero());
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPut]
        [ProducesResponseType(typeof(ResponseData<Guid>),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateHero([FromForm] HeroDto heroDto)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _heroService.UpdateHero(heroDto));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPut]
        [ProducesResponseType(typeof(ResponseData<Guid>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ApproveHero( Guid Id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _heroService.ApproveHero(Id));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpGet]
        [ProducesResponseType(typeof(ResponseData<List<HeroDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllApprovedHero()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _heroService.GetAllApprovedHero());
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpDelete]
        [ProducesResponseType(typeof(ResponseData), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteHero(Guid Id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _heroService.DeleteHero(Id));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
   
    }
}
