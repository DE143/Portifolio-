using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PortfolioAPI.Implimentation.Interfaces;
using PortfolioAPI.Infrastacture.DTOs;
using System.Net;

namespace PortfolioAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogsService _blogsService;
        public BlogsController( IBlogsService blogsService)
        {
            _blogsService = blogsService;
            
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseData<BlogsGetDto>),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllBlogs()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _blogsService.GetAllBlogs());
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [HttpPost]
        [ProducesResponseType(typeof(ResponseData<Guid>),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddBlog([FromForm] BlogsPostDto blogsPostDto)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _blogsService.AddBlogs(blogsPostDto));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseData<BlogsGetDto>),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBlogById(Guid id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _blogsService.GetBlogById(id));
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseData<Guid>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateBlog( [FromForm] BlogsPostDto blogsPostDto)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _blogsService.UpdateBlog(blogsPostDto));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseData<Guid>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ApproveBlog(Guid Id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _blogsService.ApproveBlog(Id));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [HttpGet]
        [ProducesResponseType(typeof(ResponseData<BlogsGetDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllApprovedBlogs()
        {
            if (ModelState.IsValid)
            {
                return Ok(await _blogsService.GetAllApprovedBlogs());
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [HttpGet]
        [ProducesResponseType(typeof(ResponseData<BlogsGetDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetApprovedBlogById(Guid id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _blogsService.GetApprovedBlogById(id));
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpDelete]
        [ProducesResponseType(typeof(ResponseData), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBlog(Guid Id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _blogsService.DeleteBlog(Id));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }



    }
}
