using Application.Blogs.DataTransferObject;
using Domain.entities;
using Domain.events.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Blogs.Services
{
    public interface IBlogServices : IGenericRepository<Blog>
    {
        Task<BlogsResponse> AddBlog(CreateBlogRequest request);
        Task<IEnumerable<BlogsResponse>> GetAllBlogsByAuthor(Guid authorId);
    }
}
