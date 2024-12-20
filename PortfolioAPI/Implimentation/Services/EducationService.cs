using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Ifrastacture.DataBase;
using PortfolioAPI.Ifrastacture.Models;
using PortfolioAPI.Implimentation.Interfaces;
using PortfolioAPI.Infrastacture.DTOs;

namespace PortfolioAPI.Implimentation.Services
{
    public class EducationService : IEducationService
    {
        private readonly ContentDataContext _contentDataContext;
        public EducationService(ContentDataContext contentDataContext)
        {
            _contentDataContext = contentDataContext;
            
        }
        public async Task<ResponseData<Guid>> AddEducation(EducationGetDto educationGetDto)
        {
            try
            {
                if (educationGetDto == null)
                {
                    return new ResponseData<Guid>
                    {
                        IsSuccess = false,
                        Message = "Please fill the form"

                    };
                }
                var data = new Education
                {
                    Id= Guid.NewGuid(),
                    SchoolName=educationGetDto.SchoolName,
                    Grade=educationGetDto.Grade,
                    Description=educationGetDto.Description,
                    FromDate=educationGetDto.FromDate,
                    ToDate=educationGetDto.ToDate,
                    Status="Not Approved",
                    IsActive=true

                };
                await _contentDataContext.educations.AddAsync(data);
                await _contentDataContext.SaveChangesAsync();

                return new ResponseData<Guid>
                {
                    IsSuccess = true,
                    Message = "Successfully Added",
                    data=data.Id

                };
            }catch(Exception ex)
            {
                return ExceptionHandler.HandleException<Guid>(ex);
            }
        }

        public async Task<ResponseData<Guid>> ApproveEducation(EducationGetDto educationGetDto)
        {
            try
            {
                var getData = await _contentDataContext.educations.Where(x => x.Id == educationGetDto.Id && x.IsActive == true).FirstOrDefaultAsync();
                if (getData == null)
                {
                    return new ResponseData<Guid>
                    {
                        IsSuccess = false,
                        Message = "The Is No Data Found"
                    };
                }
                getData.Status = "Approved";
                await _contentDataContext.SaveChangesAsync();

                return new ResponseData<Guid>
                {
                    IsSuccess = true,
                    Message = "Successfully Approved The Data",
                    data=getData.Id
                };
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException<Guid>(ex);
            }

        }

        public async Task<ResponseData> DeleteEducation(Guid Id)
        {
            try
            {
                var getData = await _contentDataContext.educations.Where(x => x.Id == Id && x.IsActive == true).FirstOrDefaultAsync();
                if (getData == null)
                {
                    return new ResponseData
                    {
                        IsSuccess = false,
                        Message = "The Is No Data Found"
                    };
                }
                getData.IsActive = false;
                await _contentDataContext.SaveChangesAsync();

                return new ResponseData
                {
                    IsSuccess = true,
                    Message = "Successfully Deleted The Data"
                };
            }catch(Exception ex)
            {
                return new ResponseData
                {
                    IsSuccess=false,
                    Message=ex.Message
                };
            }
        
        }

        public async Task<ResponseData<List<EducationGetDto>>> GetAllApprovedEducation()
        {
            try
            {
                var getData = await _contentDataContext.educations.Where(x => x.IsActive == true&& x.Status=="Approved").Select(x => new EducationGetDto
                {
                    Id=x.Id,
                    SchoolName=x.SchoolName,
                    Grade=x.Grade,
                    FromDate=x.FromDate,
                    ToDate=x.ToDate,
                    Description=x.Description,
                    Status=x.Status,
                    IsActive=x.IsActive
                }).ToListAsync();
                if (getData == null)
                {
                    return new ResponseData<List<EducationGetDto>>
                    {
                        IsSuccess=false,
                        Message="There Is Not Data"

                    };

                }

                return new ResponseData<List<EducationGetDto>>
                {
                    IsSuccess=true,
                    Message="Successfully Fetched",
                    data=getData
                    
                };
            }catch(Exception ex)
            {
                return ExceptionHandler.HandleException<List<EducationGetDto>>(ex);
            }
      
        }

        public async Task<ResponseData<List<EducationGetDto>>> GetAllEducation()
        {

            try
            {
                var getData = await _contentDataContext.educations.Where(x => x.IsActive == true).Select(x => new EducationGetDto
                {
                    Id = x.Id,
                    SchoolName = x.SchoolName,
                    Grade = x.Grade,
                    FromDate = x.FromDate,
                    ToDate = x.ToDate,
                    Description = x.Description,
                    Status = x.Status,
                    IsActive = x.IsActive
                }).ToListAsync();
                if (getData == null)
                {
                    return new ResponseData<List<EducationGetDto>>
                    {
                        IsSuccess = false,
                        Message = "There Is Not Data"

                    };

                }

                return new ResponseData<List<EducationGetDto>>
                {
                    IsSuccess = true,
                    Message = "Successfully Fetched",
                    data = getData

                };
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException<List<EducationGetDto>>(ex);
            }

        }

        public async Task<ResponseData<Guid>> UpdateEducation(EducationGetDto educationGetDto)
        {
            try
            {
                var getData = await _contentDataContext.educations.Where(x => x.Id == educationGetDto.Id && x.IsActive == true).FirstOrDefaultAsync();
                if (getData == null)
                {
                    return new ResponseData<Guid>
                    {
                        IsSuccess = false,
                        Message = "Data is not found"
                    };
                }
                getData.IsActive = educationGetDto.IsActive;
                getData.SchoolName = educationGetDto.SchoolName;
                getData.Grade = educationGetDto.Grade;
                getData.FromDate = educationGetDto.FromDate;
                getData.ToDate = educationGetDto.ToDate;
                getData.Description = educationGetDto.Description;
                getData.Status = "Not Approved";
                await _contentDataContext.SaveChangesAsync();
                return new ResponseData<Guid>
                {
                    IsSuccess = true,
                    Message = "Successfully Updated"
                };
            }catch(Exception ex)
            {
                return ExceptionHandler.HandleException<Guid>(ex);
            }
        }
    }
}
