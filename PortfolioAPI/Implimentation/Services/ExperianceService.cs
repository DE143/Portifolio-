using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Ifrastacture.DataBase;
using PortfolioAPI.Ifrastacture.Models;
using PortfolioAPI.Implimentation.Interfaces;
using PortfolioAPI.Infrastacture.DTOs;

namespace PortfolioAPI.Implimentation.Services
{
    public class ExperianceService:IExperianceService
    {
        private readonly ContentDataContext _contentDataContext;
        public ExperianceService( ContentDataContext contentDataContext )
        {
            _contentDataContext = contentDataContext;
        }

        public async Task<ResponseData<Guid>> AddExperiance(ExperianceDto experianceDto)
        {
            try
            {
                if (experianceDto == null)
                {
                    return new ResponseData<Guid>
                    {
                        IsSuccess = false,
                        Message = "Fill The Form"
                    };
                }
                var data = new Experiances
                {
                    Id = Guid.NewGuid(),
                    SchoolName = experianceDto.SchoolName,
                    Title = experianceDto.Title,
                    FromDate = experianceDto.FromDate,
                    ToDate = experianceDto.ToDate,
                    Description = experianceDto.Description,
                    Status = "Not Approved",
                    IsActive = true
                };
                await _contentDataContext.experiances.AddAsync(data);
                await _contentDataContext.SaveChangesAsync();
                return new ResponseData<Guid>
                {
                    IsSuccess = true,
                    Message = "Successsfully Added",
                    data = data.Id

                };
            }
            catch(Exception ex)
            {
                return ExceptionHandler.HandleException<Guid>(ex);
            }
        }

        public async Task<ResponseData> ApproveExperiance(Guid Id)
        {
            try
            {
                var getData = await _contentDataContext.experiances.Where(x => x.Id == Id && x.IsActive == true).FirstOrDefaultAsync();
                if (getData == null) { 
                return new ResponseData
                {
                    IsSuccess = false,
                    Message = "There is no data"
                };
               }
                getData.Status = "Approved";
                await _contentDataContext.SaveChangesAsync();
                return new ResponseData
                {
                    IsSuccess = true,
                    Message = "SuccessFully Change The Status"
                };
            }catch(Exception ex)
            {
                return new ResponseData
                {
                    IsSuccess=false,
                    Message="Something Went Wrong",
                    ErrorCode=ex.Message
                };
            }
        
        }

        public async Task<ResponseData> DeleteExperiance(Guid Id)
        {
            try
            {
                var getData = await _contentDataContext.experiances.Where(x => x.Id == Id && x.IsActive == true).FirstOrDefaultAsync();
                if (getData == null)
                {
                    return new ResponseData
                    {
                        IsSuccess = false,
                        Message = "There is no data"
                    };
                }
                getData.IsActive = false;
                await _contentDataContext.SaveChangesAsync();
                return new ResponseData
                {
                    IsSuccess = true,
                    Message = "SuccessFully deleted"
                };
            }
            catch (Exception ex)
            {
                return new ResponseData
                {
                    IsSuccess = false,
                    Message = "Something Went Wrong",
                    ErrorCode = ex.Message
                };
            }

        }

        public async Task<ResponseData<List<ExperianceDto>>> GettAllApprovedExperiance()
        {
            try
            {
                var getData = await _contentDataContext.experiances.Where(x => x.Status == "Approved" && x.IsActive == true)
                    .Select(x => new ExperianceDto
                    { 
                      Id=  x.Id,
                      SchoolName=  x.SchoolName,
                      Title=  x.Title,
                      Description=  x.Description,
                       FromDate= x.FromDate,
                     ToDate=   x.ToDate,
                      Status=  x.Status,
                      IsActive=  x.IsActive
                    }).ToListAsync();
                if (getData == null) { 
                return new ResponseData<List<ExperianceDto>>
                {
                    IsSuccess = false,
                    Message = "There Is No Data"
                };
            }
                return new ResponseData<List<ExperianceDto>>
                {
                    IsSuccess = true,
                    Message = "SuccessFully Fetched The Data",
                    data=getData
                };
            }catch(Exception ex)
            {
                return new ResponseData<List<ExperianceDto>>
                {
                    IsSuccess=false,
                    Message=ex.Message

                };
            }
        }

        public async Task<ResponseData<List<ExperianceDto>>> GettAllExperiance()
        {
            try
            {
                var getData = await _contentDataContext.experiances.Where(x => x.IsActive == true)
                    .Select(x => new ExperianceDto
                    {
                        Id = x.Id,
                        SchoolName = x.SchoolName,
                        Title = x.Title,
                        Description = x.Description,
                        FromDate = x.FromDate,
                        ToDate = x.ToDate,
                        Status = x.Status,
                        IsActive = x.IsActive
                    }).ToListAsync();
                if (getData == null)
                {
                    return new ResponseData<List<ExperianceDto>>
                    {
                        IsSuccess = false,
                        Message = "There Is No Data"
                    };
                }
                return new ResponseData<List<ExperianceDto>>
                {
                    IsSuccess = true,
                    Message = "SuccessFully Fetched The Data",
                    data = getData
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<List<ExperianceDto>>
                {
                    IsSuccess = false,
                    Message = ex.Message

                };
            }
            new NotImplementedException();
        }

        public async Task<ResponseData> UpdateExperiance(ExperianceDto experianceDto)
        {
            try
            {
                if (experianceDto == null)
                {
                    return new ResponseData
                    {
                        IsSuccess = false,
                        Message = "Fill The Form"
                    };
                }
                var getData = await _contentDataContext.experiances.Where(x => x.IsActive == true && x.Id == experianceDto.Id).FirstOrDefaultAsync();
                if (getData == null)
                {
                    return new ResponseData
                    {
                        IsSuccess = false,
                        Message = "There Is No Data"
                    };
                }

                getData.SchoolName = experianceDto.SchoolName;
                getData.Title = experianceDto.Title;
                getData.Description = experianceDto.Description;
                getData.FromDate = experianceDto.FromDate;
                getData.ToDate = experianceDto.ToDate;
                await _contentDataContext.SaveChangesAsync();
                return new ResponseData
                {
                    IsSuccess = true,
                    Message = "Successfully Updated"
                };
            }catch(Exception ex)
            {
                return new ResponseData
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    ErrorCode = ex.Message
                };
            }
        }
    }
}
