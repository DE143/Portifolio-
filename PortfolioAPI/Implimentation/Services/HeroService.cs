using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Ifrastacture.DataBase;
using PortfolioAPI.Ifrastacture.Models;
using PortfolioAPI.Implimentation.Interfaces;
using PortfolioAPI.Infrastacture.DTOs;

namespace PortfolioAPI.Implimentation.Services
{
    public class HeroService : IHeroService
    {
        private readonly ContentDataContext _contentData;
        private readonly IHelperSrevice _helperService;
        public HeroService(ContentDataContext contentData,IHelperSrevice helperService)
        {
            _contentData = contentData;
            _helperService = helperService;
        }

        public async Task<ResponseData<Guid>> AddHero(HeroDto heroDto)
        {
            try
            {
                if (heroDto == null)
                {

                    return new ResponseData<Guid>
                    {
                        IsSuccess = false,
                        Message = "Please Fill The Form"

                    };
                }
                var photoPath = "";
                var videoPath = "";
                if (heroDto.Photo != null)
                {
                    var photoName = Guid.NewGuid();
                    photoPath = await _helperService.UploadFiles(heroDto.Photo, photoName.ToString(), "Photos");
                   
                } 
                if (heroDto.Video != null)
                {
                    var videoName = Guid.NewGuid();
                    videoPath = await _helperService.UploadFiles(heroDto.Video, videoName.ToString(), "Videos");
                   
                }

                var addData = new Hero
                {
                    ID = Guid.NewGuid(),
                    FullName=heroDto.FullName,
                    Position=heroDto.Position,
                    Cv=heroDto.Cv,
                    PhotoUrl=photoPath,
                    VideoUrl=videoPath,
                    Status="Not Approved",
                    IsActive=true
                };
                await _contentData.heroes.AddAsync(addData);
                await _contentData.SaveChangesAsync();

                return new ResponseData<Guid>
                {
                    IsSuccess=true,
                    Message="Successfully Added",
                    data=addData.ID

                };
            }catch(Exception ex)
            {
                return ExceptionHandler.HandleException<Guid>(ex);
            }
        }

        public async Task<ResponseData<Guid>> ApproveHero(Guid Id)
        {
            try
            {
                var getData = await _contentData.heroes.Where(x => x.IsActive == true && x.ID == Id).FirstOrDefaultAsync();
                if (getData == null)
                {
                    return new ResponseData<Guid>
                    {
                        IsSuccess = false,
                        Message = "Hero Not Found"
                    };
                }
                getData.Status = "Approved";
                await _contentData.SaveChangesAsync();

                return new ResponseData<Guid>
                {
                    IsSuccess = true,
                    Message = $"Successfully "
                };
            }catch(Exception ex)
            {
                return ExceptionHandler.HandleException<Guid>(ex);
            }
        }

        public async Task<ResponseData> DeleteHero(Guid Id)
        {
            try
            {
                var getData = await _contentData.heroes.Where(x => x.IsActive == true && x.ID == Id).FirstOrDefaultAsync();
                if (getData == null)
                {
                    return new ResponseData
                    {
                        IsSuccess = false,
                        Message = "Hero Not Found"
                    };
                }
                getData.IsActive=false;
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

        public async Task<ResponseData<List<HeroDto>>> GetAllHero()
        {
            try
            {
                var data = await _contentData.heroes.Where(x => x.IsActive == true).Select(x => new HeroDto
                {
                    ID=x.ID,
                    FullName=x.FullName,
                    Position=x.Position,
                    Cv=x.Cv,
                    PhotoUrl=x.PhotoUrl,
                    VideoUrl=x.VideoUrl,
                    IsActive=x.IsActive,
                    Status=x.Status
                }).ToListAsync();
                if (data == null)
                {
                    return new ResponseData<List<HeroDto>>
                    {
                        IsSuccess = false,
                        Message = "There Is No Data"
                    };
                }
                return new ResponseData<List<HeroDto>>
                {
                    IsSuccess = true,
                    Message = "Successfully Fetched",
                    data =data
                };
            }catch(Exception ex)
            {
                return ExceptionHandler.HandleException<List<HeroDto>>(ex);
            }
        }

        public async Task<ResponseData<List<HeroDto>>> GetAllApprovedHero()
        {
            try
            {
                var data = await _contentData.heroes.Where(x => x.IsActive == true&&x.Status=="Approved").Select(x => new HeroDto
                {
                    ID = x.ID,
                    FullName = x.FullName,
                    Position = x.Position,
                    Cv = x.Cv,
                    PhotoUrl = x.PhotoUrl,
                    VideoUrl = x.VideoUrl,
                    IsActive = x.IsActive,
                    Status = x.Status
                }).ToListAsync();
                if (data == null)
                {
                    return new ResponseData<List<HeroDto>>
                    {
                        IsSuccess = false,
                        Message = "There Is No Data"
                    };
                }
                return new ResponseData<List<HeroDto>>
                {
                    IsSuccess = true,
                    Message = "Successfully Fetched",
                    data = data
                };
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException<List<HeroDto>>(ex);
            }
        }

        public async Task<ResponseData<Guid>> UpdateHero(HeroDto heroDto)
        {
            try
            {
                var getData = await _contentData.heroes.Where(x => x.IsActive == true && x.ID == heroDto.ID).FirstOrDefaultAsync();
                if (getData == null)
                {
                    return new ResponseData<Guid>
                    {
                        IsSuccess = false,
                        Message = "There No Data Based On This Id"
                    };
                }
                var photoPath = "";
                var videoPath = "";
                if (heroDto.Photo != null)
                {
                    var photoName = Guid.NewGuid();
                    photoPath = await _helperService.UploadFiles(heroDto.Photo, photoName.ToString(), "Hero/Photos");

                }
                if (heroDto.Video != null)
                {
                    var videoName = Guid.NewGuid();
                    videoPath = await _helperService.UploadFiles(heroDto.Video, videoName.ToString(), "Hero/Videos");

                }

                getData.FullName = heroDto.FullName;
                getData.Position = heroDto.Position;
                getData.Cv = heroDto.Cv;
                getData.PhotoUrl = photoPath;
                getData.VideoUrl = videoPath;
                getData.Status = "Not Approved";
                getData.IsActive = true;
                await _contentData.SaveChangesAsync();
                return new ResponseData<Guid>
                {
                    IsSuccess = true,
                    Message = "Successfuly Updated"
                };
            }catch(Exception ex)
            {
                return ExceptionHandler.HandleException<Guid>(ex);
            }
        }
   
    }
}
