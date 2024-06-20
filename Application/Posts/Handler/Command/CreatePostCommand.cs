using Application.Posts.DataTransferObject;
using Application.Posts.Services;
using Domain.events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.Handler.Command
{
    public class CreatePostCommand : CreatePostRequest, IRequest<Result<PostsResponse>>
    {
    }

    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Result<PostsResponse>>
    {
        private readonly IPostServices _postService;
        public CreatePostCommandHandler(IPostServices postService)
        {
            _postService = postService;
        }
        public async Task<Result<PostsResponse>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _postService.AddPost(request);
                return new Result<PostsResponse>
                {
                    IsSuccess = true,
                    Value = response,
                    ResponseCode = "200"
                };
            }
            catch (Exception ex)
            {
                return new Result<PostsResponse>
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
