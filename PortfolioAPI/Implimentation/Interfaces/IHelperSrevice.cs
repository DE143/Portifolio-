namespace PortfolioAPI.Implimentation.Interfaces
{
    public interface IHelperSrevice
    {

        public Task<string> UploadFiles(IFormFile formFile, string Name, string FolderName);
    }
}
