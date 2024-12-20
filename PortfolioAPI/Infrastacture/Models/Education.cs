namespace PortfolioAPI.Ifrastacture.Models
{
    public class Education
    {
        public Guid Id { get; set; }
        public string SchoolName { get; set; }
        public string Grade { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; } 
        public string Description { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
    }
}
