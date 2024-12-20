using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Implimentation.Interfaces;
using PortfolioAPI.Infrastacture.DTOs;
using System.Net;

namespace PortfolioAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly IContactUsService _contactUsService;
        public ContactUsController(IContactUsService contactUsService)
        {
            _contactUsService = contactUsService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseData<ContactUsGetDto>),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllContactUs()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _contactUsService.GetAllContactUs());
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseData<Guid>),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddContactUs(ContactUsGetDto contactUsGetDto)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _contactUsService.AddContactUs(contactUsGetDto));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseData<Guid>),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteContactUs(Guid Id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _contactUsService.DeleteContactUs(Id));

            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
