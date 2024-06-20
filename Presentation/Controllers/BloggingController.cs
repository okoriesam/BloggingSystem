using Application.Authors.Handlers.Command;
using Application.Blogs.Handler.Command;
using Application.Blogs.Handler.Query;
using Application.Posts.Handler.Command;
using Application.Posts.Handler.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloggingController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BloggingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("createAuthor")]
        public async Task<IActionResult> CreateAuthor(CreateAuthorCommand command)
        {
            var response = await _mediator.Send(command);
            if(response!= null)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response.ErrorMessage);
            }
             
        }

        [HttpPost("createBlogs")]
        public async Task<IActionResult> CreateBlogs(CreateBlogCommand command)
        {
            var response = await _mediator.Send(command);
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response.ErrorMessage);
            }
        }

        [HttpPost("createPost")]
        public async Task<IActionResult> CreatePost(CreatePostCommand command)
        {
            var response = await _mediator.Send(command);
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response.ErrorMessage);
            }
        }

        [HttpGet($"posts/{{blogId}}")]
        public async Task<IActionResult> GetAllPostsByBlog(Guid blogId)
        {
            var query = new GetAllPostsByBlogQuery
            {
                BlogId = blogId
            };
            var result = await _mediator.Send(query);
            if (result.IsSuccess == true)
            {
                return Ok(result.Value);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }

        [HttpGet($"blog/{{authorId}}")]
        public async Task<IActionResult> GetAllBlogsByAuthor(Guid authorId)
        {
            var query = new GetAllBlogsByAuthorQuery
            {
                authorId = authorId
            };
            var result = await _mediator.Send(query);
            if (result.IsSuccess == true)
            {
                return Ok(result.Value);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }
    }
}

