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
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseData<List<ServiceDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllServices()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _serviceService.GetAllServices());
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [HttpPost]
        [ProducesResponseType(typeof(ResponseData<Guid>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddService(ServiceDto serviceDto )
        {
            if (ModelState.IsValid)
            {
                return Ok(await _serviceService.AddService(serviceDto));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseData<ServiceDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetServiceById(Guid id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _serviceService.GetServiceById(id));
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseData<Guid>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateService( ServiceDto serviceDto)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _serviceService.UpdateService(serviceDto));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseData<Guid>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ApproveService(Guid Id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _serviceService.ApproveService(Id));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [HttpGet]
        [ProducesResponseType(typeof(ResponseData<List<ServiceDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllApprovedServices()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _serviceService.GetAllApprovedServices());
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ResponseData), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteService(Guid Id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _serviceService.DeleteService(Id));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


    }
}
