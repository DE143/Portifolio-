using PortfolioAPI.Infrastacture.DTOs;

namespace PortfolioAPI.Implimentation.Interfaces
{
    public interface IExperianceService
    {
        Task<ResponseData<Guid>> AddExperiance(ExperianceDto experianceDto);
        Task<ResponseData<List<ExperianceDto>>> GettAllExperiance();
        Task<ResponseData<List<ExperianceDto>>> GettAllApprovedExperiance();
        Task<ResponseData> UpdateExperiance(ExperianceDto experianceDto);
        Task<ResponseData> ApproveExperiance(Guid  Id);
        Task<ResponseData> DeleteExperiance(Guid  Id);
    }
}
