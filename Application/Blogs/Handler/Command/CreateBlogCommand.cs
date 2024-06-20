using Application.Blogs.DataTransferObject;
using Application.Blogs.Services;
using Domain.events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Blogs.Handler.Command
{
    public class CreateBlogCommand : CreateBlogRequest, IRequest<Result<BlogsResponse>>
    {
    }

    public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, Result<BlogsResponse>>
    {
        private readonly IBlogServices _blogService;
        public CreateBlogCommandHandler(IBlogServices blogService)
        {
            _blogService = blogService;
        }
        public async Task<Result<BlogsResponse>> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _blogService.AddBlog(request);
                return new Result<BlogsResponse>
                {
                    IsSuccess = true,
                    Value = response,
                    ResponseCode = "200"
                };
            }
            catch (Exception ex)
            {
                return new Result<BlogsResponse>
                {
                    IsSuccess = false,
                    Value = null,
                    ResponseCode = "400",
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
