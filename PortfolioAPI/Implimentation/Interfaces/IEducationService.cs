using PortfolioAPI.Infrastacture.DTOs;

namespace PortfolioAPI.Implimentation.Interfaces
{
    public interface IEducationService
    {
        Task<ResponseData<Guid>> AddEducation(EducationGetDto educationGetDto);
        Task<ResponseData<List<EducationGetDto>>> GetAllEducation();
        //Task<ResponseData<EducationGetDto>> GetEducationById(Guid Id);
        Task<ResponseData<Guid>> UpdateEducation(EducationGetDto educationGetDto);
        Task<ResponseData> DeleteEducation(Guid Id);
        Task<ResponseData<Guid>> ApproveEducation(EducationGetDto educationGetDto);
        Task<ResponseData<List<EducationGetDto>>> GetAllApprovedEducation();
      //  Task<ResponseData<EducationGetDto>> GetApprovedEducationById(Guid Id);
    }
}
