using PortfolioAPI.Infrastacture.DTOs;

namespace PortfolioAPI.Implimentation.Interfaces
{
    public interface IPortifolioService
    {
        Task<ResponseData<Guid>> AddPortifolio(PortifolioDto heroDto);
        Task<ResponseData<List<PortifolioDto>>> GetAllPortifolio();
        Task<ResponseData<List<PortifolioDto>>> GetAllApprovedPortifolio();
        Task<ResponseData<Guid>> UpdatePortifolio(PortifolioDto heroDto);
        Task<ResponseData<Guid>> ApprovePortifolio(Guid Id);
        Task<ResponseData> DeletePortifolio(Guid Id);
    }
}
