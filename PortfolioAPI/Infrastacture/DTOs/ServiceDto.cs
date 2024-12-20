namespace PortfolioAPI.Infrastacture.DTOs
{
    public class ServiceDto
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; } = null;
        public string Description { get; set; }
        public string? Status { get; set; }
        public bool? IsActive { get; set; }
    }
}
