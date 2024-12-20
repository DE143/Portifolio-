using PortfolioAPI.Infrastacture.DTOs;

namespace PortfolioAPI.Implimentation.Interfaces
{
    public interface IServiceService
    {
        Task<ResponseData<List<ServiceDto>>> GetAllServices();
        Task<ResponseData<Guid>> AddService(ServiceDto serviceDto);
        Task<ResponseData<ServiceDto>> GetServiceById(Guid id);
        Task<ResponseData<Guid>> UpdateService(ServiceDto serviceDto);
        Task<ResponseData> DeleteService(Guid id);
        Task<ResponseData<ServiceDto>> ApproveService(Guid id);
        Task<ResponseData<List<ServiceDto>>> GetAllApprovedServices();

    }
}
