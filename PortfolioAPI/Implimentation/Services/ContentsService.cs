using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Ifrastacture.DataBase;
using PortfolioAPI.Ifrastacture.Models;
using PortfolioAPI.Implimentation.Interfaces;
using PortfolioAPI.Infrastacture.DTOs;

namespace PortfolioAPI.Implimentation.Services
{
    public class ContentsService : IContentsService
    {
        private readonly ContentDataContext _contentDataContext;
        private readonly IHelperSrevice _helperSrevice;
        public ContentsService(ContentDataContext contentDataContext, IHelperSrevice helperSrevice)
        {
            _contentDataContext = contentDataContext;
            _helperSrevice= helperSrevice;
        }
        public async Task<ResponseData<Guid>> AddAboutUs(AboutUsPostDto aboutUs)
        {
            try
            {
                if (aboutUs == null)
                {
                    return new ResponseData<Guid>
                    {
                        data = Guid.Empty,
                        Message="There is No Data You Inserted",
                        IsSuccess = false

                    };
                }
                var filePath = "";
                if (aboutUs.Photo != null)
                {
                    var fileName = Guid.NewGuid();
                    filePath = await _helperSrevice.UploadFiles(aboutUs.Photo, fileName.ToString(), "AboutUs");
                }
                var addAbout = new About
                {
                    Id = Guid.NewGuid(),
                    FullName = aboutUs.FullName,
                    Description = aboutUs.Description,
                    Title = aboutUs.Title,
                    PhotoUrl = filePath,
                    Degree = aboutUs.Degree,
                    PhoneNumber = aboutUs.PhoneNumber,
                    Email = aboutUs.Email,
                    Address = aboutUs.Address,
                    BirthDay = aboutUs.BirthDay,
                    Experiance = aboutUs.Experiance,
                    Freelance = aboutUs.Freelance,
                    Status = "Not Approved",
                    IsActive = true,


                    };

                    _contentDataContext.Add(addAbout);
                    _contentDataContext.SaveChangesAsync();

                    return new ResponseData<Guid>
                    {
                        data = addAbout.Id,
                        Message="You Are Successfully Added It!!!",
                        IsSuccess = true
                        
                    };
                

                
            }
            catch (Exception ex)
            {
                  return ExceptionHandler.HandleException<Guid>(ex);
            }
        }

        public async Task<ResponseData<List<AboutUsDto>>> GetAboutUsById(Guid id)
        {
            try
            {
              var getAbout=  await _contentDataContext.abouts.Where(x=> x.Id==id && x.IsActive==true).Select(y=> new AboutUsDto
                {
                    Id=y.Id,
                    FullName=y.FullName,
                    Description=y.Description,
                    Title = y.Title,
                    PhotoUrl=y.PhotoUrl,
                    Degree=y.Degree,
                    PhoneNumber=y.PhoneNumber,
                    Email = y.Email,
                    Address = y.Address,
                    BirthDay = y.BirthDay,
                    Experiance = y.Experiance,
                    Freelance=y.Freelance,
                    Status=y.Status,
                    IsActive=y.IsActive
                    
                }).ToListAsync();
                return new ResponseData<List<AboutUsDto>>
                {
                    data=getAbout,
                    Message="SuccessFully Fetched",
                    IsSuccess = true
                };

            }
            catch (Exception ex) 
            { 
                return ExceptionHandler.HandleException<List<AboutUsDto>>(ex);
            }
        }

        public async Task<ResponseData<int>> DeleteAboutUs(Guid id)
        {
            try
            {
                var deletedUser= await _contentDataContext.abouts.Where(x => x.Id==id && x.IsActive==true).FirstOrDefaultAsync();
                if (deletedUser==null)
                {
                    return new ResponseData<int>
                    {

                        Message="Data Not Found",
                        IsSuccess = false,
                    };
                }
                deletedUser.IsActive = false;
                await _contentDataContext.SaveChangesAsync();
                return new ResponseData<int>
                {
                    IsSuccess = true,
                    Message = "You Are Successfully Deleted The Data"
                };
            }
            catch (Exception ex) 
            { 
                return ExceptionHandler.HandleException<int>(ex);
            }
        }

        public async Task<ResponseData<AboutUsDto>> ApproveAboutUs(Guid id)
        {
            try
            {
                var getData= await _contentDataContext.abouts.Where(x => x.Id==id && x.IsActive==true).FirstOrDefaultAsync();
                if (getData == null)
                {
                    return new ResponseData<AboutUsDto>
                    {
                        data=null, IsSuccess = false,Message="data not found"
                    };
                }
                getData.Status = "Approved";
                await _contentDataContext.SaveChangesAsync();
                return new ResponseData<AboutUsDto>
                {
                    //data=getData,
                    IsSuccess = false,
                    Message="Successfully Updated"
                };
            }
            catch (Exception ex)
            { 
                return ExceptionHandler.HandleException<AboutUsDto>(ex);
            }
        }

        public async Task<ResponseData<List<AboutUsDto>>> GetAllAboutUs()
        {
            try
            {
                var allData= await _contentDataContext.abouts.Where(x=>x.IsActive==true).Select(x=> new AboutUsDto
                {
                    Id=x.Id,
                    FullName=x.FullName,
                    Degree=x.Degree,
                    Description=x.Description,
                    Title=x.Title,
                    PhoneNumber=x.PhoneNumber,
                    PhotoUrl=x.PhotoUrl,
                    Email=x.Email,
                    Address=x.Address,
                    BirthDay=x.BirthDay,
                    Experiance=x.Experiance,
                    Status=x.Status,
                    Freelance=x.Freelance,
                    IsActive=x.IsActive
                }).ToListAsync();
                if (!allData.Any())
                {
                    return new ResponseData<List<AboutUsDto>>
                    {
                        data=null,
                        IsSuccess = false,
                        Message="There is No Data"
                    };
                }

                return new ResponseData<List<AboutUsDto>>
                {
                    data=allData,
                    IsSuccess = false,
                    Message="Successfully Fetched"
                };


            }
            catch (Exception ex) 
            { 
                return ExceptionHandler.HandleException<List<AboutUsDto>>(ex);
            }
        }

        public async Task<ResponseData<Guid>> UpdateAboutUs(UpdateAboutUsDto aboutUs)
        {
            try
            {
                var getData = await _contentDataContext.abouts.Where(x=> x.Id == aboutUs.Id && x.IsActive==true).FirstOrDefaultAsync();
                if (getData==null)
                {
                    return new ResponseData<Guid>
                    {
                        data =Guid.Empty,
                        IsSuccess = false,
                        Message = "There is not data"
                    };
                }
                var filePath = "";
                if (aboutUs.Photo != null)
                {
                    var fileName = Guid.NewGuid();
                    filePath = await _helperSrevice.UploadFiles(aboutUs.Photo, fileName.ToString(), "AboutUs");
                }

                getData.Address = aboutUs.Address;
                getData.PhoneNumber = aboutUs.PhoneNumber;
                getData.Status = "Not Approved";
                getData.Degree = aboutUs.Degree;
                getData.Description = aboutUs.Description;
                getData.Freelance = aboutUs.Freelance;
                getData.FullName = aboutUs.FullName;
                getData.BirthDay = aboutUs.BirthDay;
                getData.Email = aboutUs.Email;
                getData.Experiance = aboutUs.Experiance;
                getData.Title = aboutUs.Title;
                getData.PhotoUrl = filePath;
                getData.IsActive = true;
                await _contentDataContext.SaveChangesAsync();
                return new ResponseData<Guid> 
                {
                    data= getData.Id,IsSuccess = true,Message="Successfully Updated"
                };
            }
            catch (Exception ex)
            { 
                return ExceptionHandler.HandleException<Guid>(ex);
            }
        }
    }
}
