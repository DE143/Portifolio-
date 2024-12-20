namespace PortfolioAPI.Ifrastacture.Models
{
    public class Blogs
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
    }
}
