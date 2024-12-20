namespace PortfolioAPI.Infrastacture.DTOs
{
    public class ResponseData<T>
    {
        public T data { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public string? ErrorCode { get; set; }
    }
    public class ResponseData
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public string? ErrorCode { get; set; }

    }
}
