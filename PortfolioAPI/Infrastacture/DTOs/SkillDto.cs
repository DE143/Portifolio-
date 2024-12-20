namespace PortfolioAPI.Infrastacture.DTOs
{
    public class SkillDto
    {
        public Guid? Id { get; set; }
        public string LanguageName { get; set; }
        public string Percent { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
    }
}
