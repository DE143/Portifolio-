using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Ifrastacture.DataBase;
using PortfolioAPI.Ifrastacture.Models;
using PortfolioAPI.Implimentation.Interfaces;
using PortfolioAPI.Infrastacture.DTOs;
using PortfolioAPI.Infrastacture.Models;
namespace PortfolioAPI.Implimentation.Services
{
    public class BlogsService:IBlogsService
    {
        private readonly ContentDataContext _contentDataContext;
        private readonly IHelperSrevice _helperSrevice;
        public BlogsService(ContentDataContext contentDataContext, IHelperSrevice helperSrevice )
        {
            _contentDataContext = contentDataContext;
            _helperSrevice = helperSrevice;
        }

        public async Task<ResponseData<List<BlogsGetDto>>> GetAllBlogs()
        {
            try
            {
                var getData = await _contentDataContext.Blogs.Where(x => x.IsActive == true).Select(x => new BlogsGetDto
                {
                    IsActive=x.IsActive,
                    Id=x.Id,
                    ImageUrl=x.ImageUrl,
                    Date=x.Date,
                    Description=x.Description,
                    Status=x.Status

                }).ToListAsync();
                if (getData == null)
                {
                    return new ResponseData<List<BlogsGetDto>>
                    {
                        IsSuccess = false,
                        Message = "Tre is no data",
                        data = null
                    };
                }
                return new ResponseData<List<BlogsGetDto>>
                {
                    IsSuccess = true,
                    Message = "Successfully Fetched",
                    ErrorCode = null,
                    data=getData

                };
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException<List<BlogsGetDto>>(ex);
            }
        
        }


        public async Task<ResponseData<Guid>> AddBlogs( BlogsPostDto blogsPostDto)
        {
            try
            {
                if (blogsPostDto==null)
                {
                    return new ResponseData<Guid>
                    {
                        IsSuccess = false,
                        Message = "Fill The Form"
                    };

                }
                var filePath = "";
                if (blogsPostDto.Image!=null)
                {
                    var fileName =  Guid.NewGuid();
                    filePath = await _helperSrevice.UploadFiles(blogsPostDto.Image, fileName.ToString(), "Blogs");
                }

                var data = new Blogs
                {
                    Id=new Guid(),
                    Date=DateTime.Now,
                    Description=blogsPostDto.Description,
                    Status="Not Approved",
                    IsActive=true,
                    ImageUrl=filePath
                };
                 _contentDataContext.Blogs.Add(data);
                await _contentDataContext.SaveChangesAsync();


                return new ResponseData<Guid>
                {
                    IsSuccess = true,
                    Message = "Successfully added",
                    data=data.Id

                };
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException<Guid>(ex);
            }
        }
  
        public async Task<ResponseData<List<BlogsGetDto>>> GetBlogById(Guid id)
        {
            try
            {
                var getDate = await _contentDataContext.Blogs.Where(x => x.Id == id && x.IsActive == true).Select(x => new BlogsGetDto
                {
                    Id=x.Id,
                    ImageUrl=x.ImageUrl,
                    Description=x.Description,
                    Date=x.Date,
                    Status=x.Status,
                    IsActive=x.IsActive

                }).ToListAsync();
                if (getDate == null)
                {
                    return new ResponseData<List<BlogsGetDto>>
                    {
                        IsSuccess = false,
                        Message = "The Is No Data",
                        data = null
                    };
                }
                return new ResponseData<List<BlogsGetDto>>
                {
                    IsSuccess = true,
                    Message = "Successfully Fetched The Data",
                    data=getDate

                };
            }catch(Exception ex)
            {
                return ExceptionHandler.HandleException<List<BlogsGetDto>>(ex);
            }
        }
    
        public async Task<ResponseData<Guid>> UpdateBlog(BlogsPostDto blogsPostDto)
        {
            try
            {
                var getData = await _contentDataContext.Blogs.Where(x => x.Id == blogsPostDto.Id && x.IsActive == true).FirstOrDefaultAsync();
                if (getData == null)
                {
                    return new ResponseData<Guid>
                    {
                        IsSuccess = false,
                        Message = "There Is Not Data"
                    };
                }
                var filePath = "";
                if (blogsPostDto.Image != null)
                {
                    var fileName = Guid.NewGuid();
                    filePath = await _helperSrevice.UploadFiles(blogsPostDto.Image, fileName.ToString(), "Blogs");
                }
                getData.Date = blogsPostDto.Date;
                getData.Description = blogsPostDto.Description;
                getData.ImageUrl = filePath;
                getData.Status = "Not Approved";
                getData.IsActive = true;
                await _contentDataContext.SaveChangesAsync();
                return new ResponseData<Guid>
                {
                    IsSuccess = true,
                    Message = "Successfully Updated",
                    data=getData.Id
                };
            }catch(Exception ex)
            {
                return ExceptionHandler.HandleException<Guid>(ex);
            }

        }


        public async Task<ResponseData<BlogsPostDto>> ApproveBlog( Guid Id)
        {
            try
            {
                var getData = await _contentDataContext.Blogs.Where(x => x.Id == Id && x.IsActive == true).FirstOrDefaultAsync();
                if (getData == null)
                {
                    return new ResponseData<BlogsPostDto>
                    {
                        IsSuccess = false,
                        Message = "There Is Not Data"
                    };
                }
               
                getData.Status = "Approved";
                await _contentDataContext.SaveChangesAsync();
                return new ResponseData<BlogsPostDto>
                {
                    IsSuccess = true,
                    Message = "Successfully Updated",
                   // data = getData.Id
                };
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException<BlogsPostDto>(ex);
            }

        }


        public async Task<ResponseData<List<BlogsGetDto>>> GetAllApprovedBlogs()
        {
            try
            {
                var getData = await _contentDataContext.Blogs.Where(x => x.IsActive == true &&  x.Status=="Approved").Select(x => new BlogsGetDto
                {
                    IsActive = x.IsActive,
                    Id = x.Id,
                    ImageUrl = x.ImageUrl,
                    Date = x.Date,
                    Description = x.Description,
                    Status = x.Status

                }).ToListAsync();
                if (getData == null)
                {
                    return new ResponseData<List<BlogsGetDto>>
                    {
                        IsSuccess = false,
                        Message = "Tre is no data",
                        data = getData,
                        ErrorCode = null
                    };
                }
                return new ResponseData<List<BlogsGetDto>>
                {
                    IsSuccess = true,
                    Message = "Successfully Fetched",
                    ErrorCode = null,
                    data=getData



                };
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException<List<BlogsGetDto>>(ex);
            }

        }

        public async Task<ResponseData<List<BlogsGetDto>>> GetApprovedBlogById(Guid id)
        {
            try
            {
                var getDate = await _contentDataContext.Blogs.Where(x => x.Id == id && x.IsActive == true && x.Status=="Approved").Select(x => new BlogsGetDto
                {
                    Id = x.Id,
                    ImageUrl = x.ImageUrl,
                    Description = x.Description,
                    Date = x.Date,
                    Status = x.Status,
                    IsActive = x.IsActive

                }).ToListAsync();
                if (getDate == null)
                {
                    return new ResponseData<List<BlogsGetDto>>
                    {
                        IsSuccess = false,
                        Message = "The Is No Data",
                        data = null
                    };
                }
                return new ResponseData<List<BlogsGetDto>>
                {
                    IsSuccess = true,
                    Message = "Successfully Fetched The Data",
                    data = getDate

                };
            }
            catch (Exception ex)
            {
                return ExceptionHandler.HandleException<List<BlogsGetDto>>(ex);
            }
        }

        public async Task<ResponseData> DeleteBlog(Guid Id)
        {

            try
            {
                var getData = await _contentDataContext.Blogs.Where(x => x.Id == Id && x.IsActive == true).FirstOrDefaultAsync();
                if (getData == null)
                {
                    return new ResponseData
                    {
                        IsSuccess = false,
                        Message = "There Is Not Data"
                    };
                }

                getData.IsActive = false;
                await _contentDataContext.SaveChangesAsync();
                return new ResponseData
                   {
                    IsSuccess = true,
                    Message = "Successfully Deleted",
                    // data = getData.Id
                };
            }
            catch (Exception ex)
            {
                return new ResponseData
                {
                    IsSuccess=false,
                    Message=ex.Message
                };
            }

        }
    }
}
