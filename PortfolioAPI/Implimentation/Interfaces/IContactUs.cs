using PortfolioAPI.Infrastacture.DTOs;

namespace PortfolioAPI.Implimentation.Interfaces
{
    public interface IContactUsService
    {
        Task<ResponseData<List<ContactUsGetDto>>> GetAllContactUs();
        Task<ResponseData<Guid>> AddContactUs(ContactUsGetDto contactUsGetDto);
        Task<ResponseData<Guid>> DeleteContactUs(Guid Id);
    }
}
