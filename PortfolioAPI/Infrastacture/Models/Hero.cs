namespace PortfolioAPI.Ifrastacture.Models
{
    public class Hero
    {
        public Guid ID { get; set; }
        public string FullName { get; set; }
        public string Cv { get; set; }
        public string PhotoUrl { get; set; }
        public string VideoUrl { get; set; }
        public string Position { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
    }
}
