using Application.Posts.DataTransferObject;
using Domain.entities;
using Domain.events.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.Services
{
    public interface IPostServices : IGenericRepository<Post>
    {
        Task<PostsResponse> AddPost(CreatePostRequest request);
        Task<IEnumerable<PostsResponse>> GetAllPostsByBlog(Guid blogId);
    }
}
