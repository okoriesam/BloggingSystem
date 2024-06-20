using Application.Blogs.DataTransferObject;
using Application.Blogs.Services;
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
    public class BlogServices : GenericRepository<Blog>, IBlogServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BlogServices(ApplicationDbContext context, IMapper mapper, IUnitOfWork unitOfWork) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BlogsResponse> AddBlog(CreateBlogRequest request)
        {
            try
            {
                var newBlog = new Blog
                {
                    id = Guid.NewGuid(),
                    Name = request.Name,
                    Url = request.Url,
                    AuthorId = request.AuthorId,
                };

                await _unitOfWork.Blogs.AddAsync(newBlog);
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<BlogsResponse>(newBlog);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding a new blog.", ex);
            }
        }

        public async Task<IEnumerable<BlogsResponse>> GetAllBlogsByAuthor(Guid authorId)
        {
            try
            {
                if (authorId == null)
                    throw new Exception($"No Blog With Id {authorId} was Found");
                var blogs = await _context.Blogs.Where(b => b.AuthorId == authorId).ToListAsync();
                if (blogs == null || blogs.Count == 0)
                {
                    return Enumerable.Empty<BlogsResponse>();
                }
                return _mapper.Map<IEnumerable<BlogsResponse>>(blogs);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
