namespace PortfolioAPI.Infrastacture.DTOs
{
    public class AuthServiceResponseDto
    {
        public bool IsSucceed { get; set; }
        public string Message { get; set; }
        public string User { get; set; } = null!;
    }
}
