using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Ifrastacture.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PortfolioAPI.Infrastacture.Models;
namespace PortfolioAPI.Ifrastacture.DataBase
{
    public class ContentDataContext:IdentityDbContext<ApplicationUser>
    {
        public ContentDataContext(DbContextOptions<ContentDataContext> options) : base(options)
        {
        }

        public DbSet <About> abouts { get; set; }
        public DbSet<Blogs> Blogs { get; set; }
        public DbSet<ContactUs> contactUs { get; set; }
        public DbSet<Education> educations { get; set; }
        public DbSet<Experiances> experiances { get; set; }
        public DbSet<Hero> heroes { get; set; }
        public DbSet<Portifolio> portifolios { get; set; }
        public DbSet<Services> services { get; set; }
        public DbSet<Skills> skills { get; set; }
    }
}
