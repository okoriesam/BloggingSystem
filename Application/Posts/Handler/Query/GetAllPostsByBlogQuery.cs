using Application.Posts.DataTransferObject;
using Application.Posts.Services;
using Domain.events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.Handler.Query
{
    public class GetAllPostsByBlogQuery : IRequest<Result<IEnumerable<PostsResponse>>>
    {
        public Guid BlogId { get; set; }
    }

    public class GetAllPostsByBlogQueryHandlers : IRequestHandler<GetAllPostsByBlogQuery, Result<IEnumerable<PostsResponse>>>
    {
        private readonly IPostServices _postService;
        public GetAllPostsByBlogQueryHandlers(IPostServices postService)
        {
            _postService = postService;
        }
        public async Task<Result<IEnumerable<PostsResponse>>> Handle(GetAllPostsByBlogQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _postService.GetAllPostsByBlog(request.BlogId);
                return new Result<IEnumerable<PostsResponse>>
                {
                    IsSuccess = true,
                    Value = response,
                    ResponseCode = "200"
                };
            }
            catch (Exception ex)
            {
                return new Result<IEnumerable<PostsResponse>>
                {
                    IsSuccess = false,
                    Value = null,
                    ErrorMessage = ex.Message,
                    ResponseCode = "400"
                };
            }
        }
    }
}
