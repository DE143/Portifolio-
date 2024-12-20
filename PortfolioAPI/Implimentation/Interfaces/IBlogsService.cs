using PortfolioAPI.Infrastacture.DTOs;

namespace PortfolioAPI.Implimentation.Interfaces
{
    public interface IBlogsService
    {
        Task<ResponseData<List<BlogsGetDto>>> GetAllBlogs();
        Task<ResponseData<Guid>> AddBlogs(BlogsPostDto blogsPostDto );
        Task<ResponseData<List<BlogsGetDto>>> GetBlogById(Guid id);
        Task<ResponseData<Guid>> UpdateBlog(BlogsPostDto blogsPostDto);
        Task<ResponseData<BlogsPostDto>> ApproveBlog(Guid Id);
        Task<ResponseData> DeleteBlog(Guid Id);
        Task<ResponseData<List<BlogsGetDto>>> GetAllApprovedBlogs();
        Task<ResponseData<List<BlogsGetDto>>> GetApprovedBlogById(Guid id);
    }
}
