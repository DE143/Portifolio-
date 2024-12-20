using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Ifrastacture.DataBase;
using PortfolioAPI.Ifrastacture.Models;
using PortfolioAPI.Implimentation.Interfaces;
using PortfolioAPI.Infrastacture.DTOs;

namespace PortfolioAPI.Implimentation.Services
{
    public class ContactUsService : IContactUsService
    {
        private readonly ContentDataContext _contentDataContext;
        public ContactUsService(ContentDataContext contentDataContext)
        {
            _contentDataContext = contentDataContext;
        }

        public async Task<ResponseData<Guid>> AddContactUs(ContactUsGetDto contactUsGetDto)
        {
            try
            {
                if (contactUsGetDto == null)
                {
                    return new ResponseData<Guid>
                    {
                        IsSuccess = false,
                        Message = "Please Fill The Form"

                    };
                }
                var addData = new ContactUs
                {
                    Id=Guid.NewGuid(),
                    Email=contactUsGetDto.Email,
                    FullName=contactUsGetDto.FullName,
                    Message=contactUsGetDto.Message,
                    Subject=contactUsGetDto.Subject,
                    IsActive=contactUsGetDto.IsActive
                };
                await _contentDataContext.contactUs.AddAsync(addData);
                await _contentDataContext.SaveChangesAsync();
                return new ResponseData<Guid>
                {
                    data=addData.Id,
                    IsSuccess = true,
                    Message = "You Are Successfully Submitted"

                };

            }catch(Exception ex)
            {
                return ExceptionHandler.HandleException<Guid>(ex);
            }
        }

        public async Task<ResponseData<Guid>> DeleteContactUs(Guid Id)
        {
            try
            {
                var getData = await _contentDataContext.contactUs.Where(x => x.Id == Id && x.IsActive == true).FirstOrDefaultAsync();

                if (getData == null)
                {
                    return new ResponseData<Guid>
                    {
                        IsSuccess = false,
                        Message = " Data Not Found"
                    };
                }
                getData.IsActive = false;
                await _contentDataContext.SaveChangesAsync();

                return new ResponseData<Guid>
                {
                    IsSuccess = true,
                    Message = "Successfully Deleted"
                };
            }catch(Exception ex)
            {
                return ExceptionHandler.HandleException<Guid>(ex);
            }
        }

        public async Task<ResponseData<List<ContactUsGetDto>>> GetAllContactUs()
        {
            try
            {
                var allData = await _contentDataContext.contactUs.Where(x => x.IsActive == true).Select(x => new ContactUsGetDto
                {
                    Id=x.Id,
                    FullName=x.FullName,
                    Email=x.Email,
                    Message=x.Message,
                    Subject=x.Subject,
                    IsActive=x.IsActive
                }).ToListAsync();
                if (allData == null)
                {
                    return new ResponseData<List<ContactUsGetDto>>
                    {
                        IsSuccess = false,
                        Message = "There is no data",
                        data = null
                    };
                }

                return new ResponseData<List<ContactUsGetDto>>
                {
                    IsSuccess = true,
                    Message = "SuccessFully Fetched",
                    data = allData
                };
                
            }catch(Exception ex)
            {
                return ExceptionHandler.HandleException<List<ContactUsGetDto>>(ex);
            }
        }
    }
}
