using Application.Blogs.DataTransferObject;
using Application.Blogs.Services;
using Domain.events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Blogs.Handler.Query
{
    public class GetAllBlogsByAuthorQuery : IRequest<Result<IEnumerable<BlogsResponse>>>
    {
        public Guid authorId { get; set; }
    }

    public class GetAllBlogsByAuthorQueryHandler : IRequestHandler<GetAllBlogsByAuthorQuery, Result<IEnumerable<BlogsResponse>>>
    {
        private readonly IBlogServices _blogService;
        public GetAllBlogsByAuthorQueryHandler(IBlogServices blogService)
        {
            _blogService = blogService;
        }
        public async Task<Result<IEnumerable<BlogsResponse>>> Handle(GetAllBlogsByAuthorQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _blogService.GetAllBlogsByAuthor(request.authorId);
                return new Result<IEnumerable<BlogsResponse>>
                {
                    IsSuccess = true,
                    Value = response,
                    ResponseCode = "200"
                };
            }
            catch (Exception ex)
            {
                return new Result<IEnumerable<BlogsResponse>>
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
