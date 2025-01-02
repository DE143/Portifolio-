namespace PortfolioAPI.Infrastacture.DTOs
{
    public class PortifolioDto
    {
        public Guid? Id { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile Image { get; set; }
        public string? Status { get; set; }
        public bool? IsActive { get; set; }
    }
}
