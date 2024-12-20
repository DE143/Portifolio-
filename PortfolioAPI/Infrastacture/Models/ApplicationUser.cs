using Microsoft.AspNetCore.Identity;

namespace PortfolioAPI.Infrastacture.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
