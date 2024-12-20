using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Ifrastacture.DataBase;
using PortfolioAPI.Ifrastacture.Models;
using PortfolioAPI.Implimentation.Interfaces;
using PortfolioAPI.Infrastacture.DTOs;

namespace PortfolioAPI.Implimentation.Services
{
    public class ServiceService:IServiceService
    {
        private readonly ContentDataContext _contentDataContext;
        public ServiceService(ContentDataContext contentDataContext)
        {
            _contentDataContext = contentDataContext;
        }

        public async Task<ResponseData<Guid>> AddService(ServiceDto serviceDto)
        {
            try
            {
                if (serviceDto == null)
                {
                  return new ResponseData<Guid>
                    {
                        IsSuccess = true,
                        Message = "Successfully Updated",
                    };
                }
                var data = new Ifrastacture.Models.Services
                {
                    Id=Guid.NewGuid(),
                    Title=serviceDto.Title,
                    Icon=serviceDto.Icon,
                    Description=serviceDto.Description,
                    IsActive=true,
                    Status="Not Approved"

                };
                await _contentDataContext.services.AddAsync(data);
                await _contentDataContext.SaveChangesAsync();
              return  new ResponseData<Guid>
                {
                    IsSuccess = true,
                    Message = "Successfully Added",
                     data = data.Id
                };
            } catch(Exception ex)
            {
             return  new ResponseData<Guid>
                {
                    IsSuccess = false,
                    Message =ex.Message,
                    // data = getData.Id
                };
            }
        }

        public async Task<ResponseData<ServiceDto>> ApproveService(Guid id)
        {

            try
            {
                var getData = await _contentDataContext.services.Where(x => x.Id == id && x.IsActive == true).FirstOrDefaultAsync();
                if (getData == null)
                {
                    return new ResponseData<ServiceDto>
                    {
                        IsSuccess = false,
                        Message = "There Is Not Data"
                    };
                }

                getData.Status = "Approved";
                await _contentDataContext.SaveChangesAsync();
                return new ResponseData<ServiceDto>
                {
                    IsSuccess = true,
                    Message = "Successfully Approved",
                    // data = getData.Id
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<ServiceDto>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    // data = getData.Id
                };
            }

        }

        public async Task<ResponseData> DeleteService(Guid id)
        {

            try
            {
                var getData = await _contentDataContext.services.Where(x => x.Id == id && x.IsActive == true).FirstOrDefaultAsync();
                if (getData == null)
                {
                    return new ResponseData
                    {
                        IsSuccess = false,
                        Message = "There Is Not Data"
                    };
                }

                getData.IsActive =false;
                await _contentDataContext.SaveChangesAsync();
                return new ResponseData
                {
                    IsSuccess = true,
                    Message = "Successfully Updated",
                    // data = getData.Id
                };
            }
            catch (Exception ex)
            {
                return new ResponseData
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    // data = getData.Id
                };
            }

        }

        public async Task<ResponseData<List<ServiceDto>>> GetAllApprovedServices()
        {

            try
            {
                var getData = await _contentDataContext.services.Where(x => x.Status == "Approved" && x.IsActive == true).Select(x=> new ServiceDto
                {
                    Id=x.Id,
                    Title=x.Title,
                    Icon=x.Icon,

                }).ToListAsync();
                if (getData == null)
                {
                    return new ResponseData<List<ServiceDto>>
                    {
                        IsSuccess = false,
                        Message = "There Is Not Data"
                    };
                }

                getData.IsActive = false;
                await _contentDataContext.SaveChangesAsync();
                return new ResponseData<List<ServiceDto>>
                {
                    IsSuccess = true,
                    Message = "Successfully Updated",
                    // data = getData.Id
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<List<ServiceDto>>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    // data = getData.Id
                };
            }

        }

        public Task<ResponseData<List<ServiceDto>>> GetAllServices()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<ServiceDto>> GetServiceById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Guid>> UpdateService(ServiceDto serviceDto)
        {
            throw new NotImplementedException();
        }
    }
}
