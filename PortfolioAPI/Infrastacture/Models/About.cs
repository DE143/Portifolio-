namespace PortfolioAPI.Ifrastacture.Models
{
    public class About
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string PhotoUrl { get; set; }
        public string Degree { get;set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; }
        public string Address { get; set; }
        public string BirthDay { get; set; }
        public string Experiance { get; set; }
        public string Freelance { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
    }
}
