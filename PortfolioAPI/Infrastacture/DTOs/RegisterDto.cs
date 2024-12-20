using System.ComponentModel.DataAnnotations;

namespace PortfolioAPI.Infrastacture.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "First Neme is Required !")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Neme is Required !")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "User Neme is Required !")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email is Required !")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Reqyyired")]
        public string Password { get; set; }
    }
}
