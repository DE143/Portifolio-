using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Implimentation.Interfaces;
using PortfolioAPI.Infrastacture.DTOs;
using System.Net;

namespace PortfolioAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;
        public SkillController(ISkillService skillService ) {
            _skillService = skillService;
        }



        [HttpGet]
        [ProducesResponseType(typeof(ResponseData<List<SkillDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllSkills()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _skillService.GetAllSkills());
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [HttpPost]
        [ProducesResponseType(typeof(ResponseData<Guid>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddSkill(SkillDto SkillDto)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _skillService.AddSkill(SkillDto));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseData<SkillDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSkillById(Guid id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _skillService.GetSkillById(id));
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseData<Guid>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateSkill(SkillDto SkillDto)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _skillService.UpdateSkill(SkillDto));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseData<Guid>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ApproveSkill(Guid Id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _skillService.ApproveSkill(Id));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [HttpGet]
        [ProducesResponseType(typeof(ResponseData<List<SkillDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllApprovedSkills()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _skillService.GetAllApprovedSkills());
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ResponseData), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteSkill(Guid Id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _skillService.DeleteSkill(Id));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    }
}
