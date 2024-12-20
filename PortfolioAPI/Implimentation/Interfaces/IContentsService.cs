using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Infrastacture.DTOs;

namespace PortfolioAPI.Implimentation.Interfaces
{
    public interface IContentsService
    {
        Task<ResponseData<List<AboutUsDto>>> GetAllAboutUs();
        Task<ResponseData<Guid>> AddAboutUs(AboutUsPostDto aboutUs);
        Task<ResponseData<List<AboutUsDto>>> GetAboutUsById(Guid id);
        Task<ResponseData<Guid>> UpdateAboutUs(UpdateAboutUsDto aboutUs);
        Task<ResponseData<int>> DeleteAboutUs(Guid id);
        Task<ResponseData<AboutUsDto>> ApproveAboutUs(Guid id);
    }
}
