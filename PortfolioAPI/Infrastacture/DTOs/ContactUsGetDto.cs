namespace PortfolioAPI.Infrastacture.DTOs
{
    public class ContactUsGetDto
    {
        public Guid? Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsActive { get; set; }
    }
}
