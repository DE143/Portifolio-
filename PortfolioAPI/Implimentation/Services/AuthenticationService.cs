using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PortfolioAPI.Implimentation.Interfaces;
using PortfolioAPI.Infrastacture.DTOs;
using PortfolioAPI.Infrastacture.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace PortfolioAPI.Implimentation.Services
{
    public class AuthenticationService:IAuthenticationService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }


        //---------------------------------Seed Role -------------------------------
        public async Task<AuthServiceResponseDto> SeedRoleAsync()
        {
            bool isAdminExist = await _roleManager.RoleExistsAsync(StaticUserRole.ADMIN);
            bool isContentWriterExist = await _roleManager.RoleExistsAsync(StaticUserRole.ContentWritter);

            if (isAdminExist && isContentWriterExist)
            {
                return new AuthServiceResponseDto
                {
                    IsSucceed = false,
                    Message = "Role Seeding Allready Exist !!"
                };
            }



            await _roleManager.CreateAsync(new IdentityRole(StaticUserRole.ADMIN));
            await _roleManager.CreateAsync(new IdentityRole(StaticUserRole.ContentWritter));

            return new AuthServiceResponseDto
            {
                IsSucceed = true,
                Message = "User Seeding Successfully"
            };
        }
        //----------------------Register User With Custom Application User Model | DTO ----------------------

        public async Task<AuthServiceResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var isExistUser = await _userManager.FindByNameAsync(registerDto.UserName);
            if (isExistUser != null)
            {
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "User Allready Exist"
                };
            }

            ApplicationUser newUser = new ApplicationUser()
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var createUserResult = await _userManager.CreateAsync(newUser, registerDto.Password);

            if (!createUserResult.Succeeded)
            {
                var errorString = "Account Creation is Failed Because of :";
                foreach (var error in createUserResult.Errors)
                {
                    errorString += "#" + error.Description;

                }
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = errorString
                };
            }

            // Adding Default User Role To All User
            await _userManager.AddToRoleAsync(newUser, StaticUserRole.ContentWritter);
            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Message = "User Created Successsfully"
            };
        }


        //-------------------------------Login User --------------------------------------------

        public async Task<AuthServiceResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user is null)
            {
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Invalid Credential"
                };
            }

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!isPasswordCorrect)
            {
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Invalid Credential"
                };
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim("JWTID",Guid.NewGuid().ToString()),
                new Claim("FirstName",user.FirstName),
                new Claim("LastName",user.LastName)

            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var token = GenerateNewJsonWebToken(authClaims);  //  used for Generating Toke to Auth User

            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Message = token,
                User=userRoles[0]
            };
        }
        //-----------------------------------------------Give Admin Role to User---------------------------

        public async Task<AuthServiceResponseDto> ChangeUserStatus(string UserName, string role)
        {

            var user = await _userManager.FindByNameAsync(UserName);
            if (user is null)
            {
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Invalid User Name"
                };
            }
            await _userManager.AddToRoleAsync(user, role);
            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Message = "User Is Now An Admin Role"
            };
        }
   

       //------------------------------------Generating New Token for Login User----------------------------


        private string GenerateNewJsonWebToken(List<Claim> claims)
        {
            var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var tokenObject = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: claims,
                signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)

                );
            string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);
            return token;
        }

        public async Task<ResponseData<List<RegisterDto>>> GetAllUser()
        {
            try
            {
                var getUser = await _userManager.Users.Select(x=> new RegisterDto
                {
                    Email=x.Email,
                    FirstName=x.FirstName,
                    LastName=x.LastName,
                    UserName=x.UserName,
                    Password=x.PasswordHash,

                }
                    ).ToListAsync();

            

                return new ResponseData<List<RegisterDto>>
                {
                    IsSuccess = true,
                    Message = "success",
                    data = getUser
                };

            }
            catch(Exception ex)
            {
                return ExceptionHandler.HandleException<List<RegisterDto>>(ex);

            }
        }
    }
}
