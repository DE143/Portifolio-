namespace PortfolioAPI.Infrastacture.DTOs
{
    public class BlogsGetDto
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Date { get; set; } 
        public string Description { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
    }
    public class BlogsPostDto
    {
        public Guid? Id { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile Image { get; set; }
        public DateTime Date { get; set; } 
        public string Description { get; set; }
        public string? Status { get; set; }
        public bool? IsActive { get; set; }
    }
}
