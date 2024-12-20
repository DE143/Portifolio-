using PortfolioAPI.Infrastacture.DTOs;

namespace PortfolioAPI.Implimentation.Interfaces
{
    public interface IHeroService
    {
        Task<ResponseData<Guid>> AddHero(HeroDto heroDto);
        Task<ResponseData<List<HeroDto>>> GetAllHero();
        Task<ResponseData<List<HeroDto>>> GetAllApprovedHero();
        Task<ResponseData<Guid>> UpdateHero(HeroDto heroDto);
        Task<ResponseData<Guid>> ApproveHero(Guid Id);
        Task<ResponseData> DeleteHero(Guid Id);

    }
}
