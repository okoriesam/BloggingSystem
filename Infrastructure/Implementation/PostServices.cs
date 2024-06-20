using Application.Posts.DataTransferObject;
using Application.Posts.Services;
using AutoMapper;
using Domain.entities;
using Domain.events.repository;
using Infrastructure.Persistence.Repository;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implementation
{
    public class PostServices : GenericRepository<Post>, IPostServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public PostServices(ApplicationDbContext context, IMapper mapper, IUnitOfWork unitOfWork) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PostsResponse> AddPost(CreatePostRequest request)
        {
            try
            {
                var newPost = new Post
                {
                    id = Guid.NewGuid(),
                    Title = request.Title,
                    Content = request.Content,
                    BlogId = request.BlogId,
                };
                await _context.Posts.AddAsync(newPost);
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<PostsResponse>(newPost);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding a new post.", ex);
            }
        }

        public async Task<IEnumerable<PostsResponse>> GetAllPostsByBlog(Guid blogId)
        {
           
            try
            {
                if (blogId == Guid.Empty)
                    throw new Exception("Invalid BlogId Passed Or Found");

                var posts = await _unitOfWork.Posts.GetAllAsync(p => p.BlogId == blogId, include: p => p.Include(post => post.Blog));

                if (posts == null || !posts.Any())
                {
                    throw new Exception("No data");
                }

                return _mapper.Map<IEnumerable<PostsResponse>>(posts);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
