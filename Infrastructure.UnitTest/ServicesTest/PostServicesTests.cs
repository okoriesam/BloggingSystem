using Application.Posts.DataTransferObject;
using AutoMapper;
using Domain.entities;
using Domain.events.repository;
using Infrastructure.Implementation;
using Infrastructure.Persistence.Repository;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitTest.ServicesTest
{
    [TestFixture]
    public class PostServicesTests : IDisposable
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IMapper _mapper;
        private PostServices _postServices;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // Add your AutoMapper profiles here
                cfg.CreateMap<Post, PostsResponse>();
            });
            _mapper = config.CreateMapper();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(u => u.Posts).Returns(new GenericRepository<Post>(_context));

            _postServices = new PostServices(_context, _mapper, _mockUnitOfWork.Object);
        }

        [Test]
        public async Task AddPost_ShouldAddNewPost()
        {
            // Arrange
            var request = new CreatePostRequest
            {
                Title = "New Post",
                Content = "This is a new post",
                BlogId = Guid.NewGuid()
            };

            // Act
            var response = await _postServices.AddPost(request);

            // Assert
            Assert.NotNull(response);
            Assert.AreEqual("New Post", response.Title);
            Assert.AreEqual("This is a new post", response.Content);
            Assert.AreEqual(request.BlogId, response.BlogId);
        }

        [Test]
        public void AddPost_ShouldThrowException_WhenErrorOccurs()
        {
            // Arrange
            var request = new CreatePostRequest
            {
                Title = "New Post",
                Content = "This is a new post",
                BlogId = Guid.NewGuid()
            };
            _mockUnitOfWork.Setup(u => u.Posts.AddAsync(It.IsAny<Post>())).Throws(new Exception("Database error"));

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await _postServices.AddPost(request));
            Assert.AreEqual("An error occurred while adding a new post.", ex.Message);
        }

        [Test]
        public async Task GetAllPostsByBlog_ShouldReturnPosts()
        {
            // Arrange
            var blogId = Guid.NewGuid();
            var posts = new List<Post>
            {
                new Post { id = Guid.NewGuid(), Title = "Post 1", Content = "Content 1", BlogId = blogId },
                new Post { id = Guid.NewGuid(), Title = "Post 2", Content = "Content 2", BlogId = blogId }
            };
            await _context.Posts.AddRangeAsync(posts);
            await _context.SaveChangesAsync();

            // Act
            var response = await _postServices.GetAllPostsByBlog(blogId);

            // Assert
            Assert.NotNull(response);
            Assert.AreEqual(2, response.Count());
        }

        [Test]
        public void GetAllPostsByBlog_ShouldThrowException_WhenBlogIdIsEmpty()
        {
            // Arrange
            var blogId = Guid.Empty;

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await _postServices.GetAllPostsByBlog(blogId));
            Assert.AreEqual("Invalid BlogId Passed Or Found", ex.Message);
        }

        [Test]
        public void GetAllPostsByBlog_ShouldThrowException_WhenNoPostsFound()
        {
            // Arrange
            var blogId = Guid.NewGuid();

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await _postServices.GetAllPostsByBlog(blogId));
            Assert.AreEqual("No data", ex.Message);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
