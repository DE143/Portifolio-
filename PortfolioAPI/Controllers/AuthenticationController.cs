using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Infrastacture.DTOs;
using PortfolioAPI.Implimentation.Interfaces;
using System.Net;
using Microsoft.AspNetCore.Authorization;
namespace PortfolioAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
   // [Authorize]

    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }


        [HttpPost]
   
        public async Task<IActionResult> SeedRoles()
        {
            var seedRole = await _authenticationService.SeedRoleAsync();
            return Ok(seedRole);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseData<List<RegisterDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllUser()
        {
            if(ModelState.IsValid)
            {
                return Ok(await _authenticationService.GetAllUser());
            }
            else
            {
                return BadRequest();
            }

        }


        [HttpPost]
    
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var regster = await _authenticationService.RegisterAsync(registerDto);
            if (regster.IsSucceed)
            {
                return Ok(regster);
            }
            return BadRequest(regster);

        }

        [HttpPost]
        
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var login = await _authenticationService.LoginAsync(loginDto);

            if (login.IsSucceed)
            {
                return Ok(login);
            }
            return BadRequest(login);

        }
        // Route Make User Admin
        [HttpPost]
        public async Task<IActionResult> ChangeUserStatus(string UserName, string role)
        {
            var adminRole = await _authenticationService.ChangeUserStatus(UserName, role);
            if (adminRole.IsSucceed)
            {
                return Ok(adminRole);
            }
            return BadRequest(adminRole);
        }
   
    }
}
