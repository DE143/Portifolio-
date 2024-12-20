using PortfolioAPI.Infrastacture.DTOs;

namespace PortfolioAPI.Implimentation.Interfaces
{
    public interface ISkillService
    {
        Task<ResponseData<List<SkillDto>>> GetAllSkills();
        Task<ResponseData<Guid>> AddSkill(SkillDto skillDto);
        Task<ResponseData<SkillDto>> GetSkillById(Guid id);
        Task<ResponseData<Guid>> UpdateSkill(SkillDto skillDto);
        Task<ResponseData> DeleteSkill(Guid id);
        Task<ResponseData<SkillDto>> ApproveSkill(Guid id);
        Task<ResponseData<List<SkillDto>>> GetAllApprovedSkills();
    }
}
