namespace PortfolioAPI.Infrastacture.DTOs
{
    public class HeroDto
    {
        public Guid? ID { get; set; }
        public string FullName { get; set; }
        public string Cv { get; set; }
        public string? PhotoUrl { get; set; }
        public IFormFile Photo { get; set; }
        public string? VideoUrl { get; set; } 
        public IFormFile Video { get; set; } 
        public string Position { get; set; }
        public string? Status { get; set; }
        public bool? IsActive { get; set; }
    }
}
