using PortfolioAPI.Infrastacture.DTOs;

namespace PortfolioAPI.Implimentation.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthServiceResponseDto> SeedRoleAsync();
        Task<AuthServiceResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<ResponseData<List<RegisterDto>>> GetAllUser();
        Task<AuthServiceResponseDto> LoginAsync(LoginDto loginDto);
        Task<AuthServiceResponseDto> ChangeUserStatus(string UserName, string role);
    }
}
