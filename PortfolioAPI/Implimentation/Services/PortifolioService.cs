using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Ifrastacture.DataBase;
using PortfolioAPI.Ifrastacture.Models;
using PortfolioAPI.Implimentation.Interfaces;
using PortfolioAPI.Infrastacture.DTOs;
using PortfolioAPI.Infrastacture.Models;
namespace PortfolioAPI.Implimentation.Services
{
    public class PortifolioService:IPortifolioService
    {
        private readonly IHelperSrevice _helperSrevice;
        private readonly ContentDataContext _contentData;
        public PortifolioService( ContentDataContext contentData, IHelperSrevice helperSrevice )
        {
            _contentData = contentData; _helperSrevice = helperSrevice;
        }


        public async Task<ResponseData<Guid>> AddPortifolio(PortifolioDto portifolioDto)
        {
            try
            {
                if (portifolioDto == null)
                {

                    return new ResponseData<Guid>
                    {
                        IsSuccess = false,
                        Message = "Please Fill The Form"

                    };
                }
                var photoPath = "";
                if (portifolioDto.Image != null)
                {
                    var photoName = Guid.NewGuid();
                    photoPath = await _helperSrevice.UploadFiles(portifolioDto.Image, photoName.ToString(), "Photos");

                }
               

                var addData = new Portifolio
                {
                    Id = Guid.NewGuid(),
                    ImageUrl = photoPath,
                    Status = "Not Approved",
                    IsActive = true
                };
                await _contentData.portifolios.AddAsync(addData);
                await _contentData.SaveChangesAsync();

                return new ResponseData<Guid>
                {
                    IsSuccess = true,
                    Message = "Successfully Added",
                    data = addData.Id

                };
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException<Guid>(ex);
            }
        }

        public async Task<ResponseData<Guid>> ApprovePortifolio(Guid Id)
        {
            try
            {
                var getData = await _contentData.portifolios.Where(x => x.IsActive == true && x.Id == Id).FirstOrDefaultAsync();
                if (getData == null)
                {
                    return new ResponseData<Guid>
                    {
                        IsSuccess = false,
                        Message = "Portifolio Not Found"
                    };
                }
                getData.Status = "Approved";
                await _contentData.SaveChangesAsync();

                return new ResponseData<Guid>
                {
                    IsSuccess = true,
                    Message = $"Successfully "
                };
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException<Guid>(ex);
            }
        }

        public async Task<ResponseData> DeletePortifolio(Guid Id)
        {
            try
            {
                var getData = await _contentData.portifolios.Where(x => x.IsActive == true && x.Id == Id).FirstOrDefaultAsync();
                if (getData == null)
                {
                    return new ResponseData
                    {
                        IsSuccess = false,
                        Message = "Portifolio Not Found"
                    };
                }
                getData.IsActive = false;
                await _contentData.SaveChangesAsync();

                return new ResponseData
                {
                    IsSuccess = true,
                    Message = $"Successfully "
                };
            }
            catch (Exception ex)
            {
                return new ResponseData
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseData<List<PortifolioDto>>> GetAllPortifolio()
        {
            try
            {
                var data = await _contentData.portifolios.Where(x => x.IsActive == true).Select(x => new PortifolioDto
                {
                    Id = x.Id,
                    ImageUrl = x.ImageUrl,
                    IsActive = x.IsActive,
                    Status = x.Status
                }).ToListAsync();
                if (data == null)
                {
                    return new ResponseData<List<PortifolioDto>>
                    {
                        IsSuccess = false,
                        Message = "There Is No Data"
                    };
                }
                return new ResponseData<List<PortifolioDto>>
                {
                    IsSuccess = true,
                    Message = "Successfully Fetched",
                    data = data
                };
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException<List<PortifolioDto>>(ex);
            }
        }

        public async Task<ResponseData<List<PortifolioDto>>> GetAllApprovedPortifolio()
        {
            try
            {
                var data = await _contentData.portifolios.Where(x => x.IsActive == true && x.Status == "Approved").Select(x => new PortifolioDto
                {
                    Id = x.Id,
                    ImageUrl = x.ImageUrl,
                    IsActive = x.IsActive,
                    Status = x.Status
                }).ToListAsync();
                if (data == null)
                {
                    return new ResponseData<List<PortifolioDto>>
                    {
                        IsSuccess = false,
                        Message = "There Is No Data"
                    };
                }
                return new ResponseData<List<PortifolioDto>>
                {
                    IsSuccess = true,
                    Message = "Successfully Fetched",
                    data = data
                };
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException<List<PortifolioDto>>(ex);
            }
        }

        public async Task<ResponseData<Guid>> UpdatePortifolio(PortifolioDto portifolioDto)
        {
            try
            {
                var getData = await _contentData.portifolios.Where(x => x.IsActive == true && x.Id == portifolioDto.Id).FirstOrDefaultAsync();
                if (getData == null)
                {
                    return new ResponseData<Guid>
                    {
                        IsSuccess = false,
                        Message = "There No Data Based On This Id"
                    };
                }
                var photoPath = "";
                if (portifolioDto.Image != null)
                {
                    var photoName = Guid.NewGuid();
                    photoPath = await _helperSrevice.UploadFiles(portifolioDto.Image, photoName.ToString(), "Hero/Photos");

                }
               

               
                getData.ImageUrl = photoPath;
                getData.Status = "Not Approved";
                getData.IsActive = true;
                await _contentData.SaveChangesAsync();
                return new ResponseData<Guid>
                {
                    IsSuccess = true,
                    Message = "Successfuly Updated"
                };
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException<Guid>(ex);
            }
        }


    }
}
