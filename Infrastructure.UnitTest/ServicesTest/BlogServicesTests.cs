using Application.Blogs.DataTransferObject;
using AutoMapper;
using Domain.entities;
using Domain.events.repository;
using Infrastructure.Implementation;
using Infrastructure.Persistence.Repository;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.UnitTest.ServicesTest
{
    [TestFixture]
    public class BlogServicesTests : IDisposable
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IMapper _mapper;
        private BlogServices _blogServices;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Blog, BlogsResponse>();
            });
            _mapper = config.CreateMapper();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(u => u.Blogs).Returns(new GenericRepository<Blog>(_context));

            _blogServices = new BlogServices(_context, _mapper, _mockUnitOfWork.Object);
        }

        [Test]
        public async Task AddBlog_ShouldAddNewBlog()
        {
            // Arrange
            var request = new CreateBlogRequest
            {
                Name = "Tech Blog",
                Url = "http://techblog.com",
                AuthorId = Guid.NewGuid()
            };

            // Act
            var response = await _blogServices.AddBlog(request);

            // Assert
            Assert.NotNull(response);
            Assert.AreEqual("Tech Blog", response.Name);
            Assert.AreEqual("http://techblog.com", response.Url);
            Assert.AreEqual(request.AuthorId, response.AuthorId);
        }

        [Test]
        public void AddBlog_ShouldThrowException_WhenErrorOccurs()
        {
            // Arrange
            var request = new CreateBlogRequest
            {
                Name = "Tech Blog",
                Url = "http://techblog.com",
                AuthorId = Guid.NewGuid()
            };
            _mockUnitOfWork.Setup(u => u.Blogs.AddAsync(It.IsAny<Blog>())).Throws(new Exception("Database error"));

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await _blogServices.AddBlog(request));
            Assert.AreEqual("An error occurred while adding a new blog.", ex.Message);
        }

        [Test]
        public async Task GetAllBlogsByAuthor_ShouldReturnBlogs()
        {
            // Arrange
            var authorId = Guid.NewGuid();
            var blogs = new List<Blog>
            {
                new Blog { id = Guid.NewGuid(), Name = "Blog1", Url = "http://blog1.com", AuthorId = authorId },
                new Blog { id = Guid.NewGuid(), Name = "Blog2", Url = "http://blog2.com", AuthorId = authorId }
            };
            await _context.Blogs.AddRangeAsync(blogs);
            await _context.SaveChangesAsync();

            // Act
            var response = await _blogServices.GetAllBlogsByAuthor(authorId);

            // Assert
            Assert.NotNull(response);
            Assert.AreEqual(2, response.Count());
        }

        [Test]
        public async Task GetAllBlogsByAuthor_ShouldReturnEmpty_WhenNoBlogsFound()
        {
            // Arrange
            var authorId = Guid.NewGuid();

            // Act
            var response = await _blogServices.GetAllBlogsByAuthor(authorId);

            // Assert
            Assert.NotNull(response);
            Assert.AreEqual(0, response.Count());
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
