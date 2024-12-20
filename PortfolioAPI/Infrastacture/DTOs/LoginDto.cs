using System.ComponentModel.DataAnnotations;

namespace PortfolioAPI.Infrastacture.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "User Neme is Required !")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email is Required !")]
        public string Password { get; set; }
    }
}
