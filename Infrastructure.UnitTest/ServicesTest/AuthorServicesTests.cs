using Application.Authors.DataTransferObject;
using AutoMapper;
using Domain.entities;
using Domain.events.repository;
using Infrastructure.Implementation;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.UnitTest.ServicesTest
{
    [TestFixture]
    public class AuthorServicesTests : IDisposable
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IMapper _mapper;
        private AuthorServices _authorServices;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // Add your AutoMapper profiles here
                cfg.CreateMap<Author, AuthorResponse>();
            });
            _mapper = config.CreateMapper();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _context = new ApplicationDbContext(options);

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(u => u.Authors).Returns(new GenericRepository<Author>(_context));

            _authorServices = new AuthorServices(_context, _mapper, _mockUnitOfWork.Object);
        }

        [Test]
        public async Task CreateAuthor_ShouldAddNewAuthor()
        {
            // Arrange
            var request = new CreateAuthorRequest
            {
                Name = "John Doe",
                Email = "john.doe@example.com"
            };

            // Act
            var response = await _authorServices.CreateAuthor(request);

            // Assert
            Assert.NotNull(response);
            Assert.AreEqual("John Doe", response.Name);
            Assert.AreEqual("john.doe@example.com", response.Email);
        }

        [Test]
        public void CreateAuthor_ShouldThrowException_WhenErrorOccurs()
        {
            // Arrange
            var request = new CreateAuthorRequest
            {
                Name = "Jane Doe",
                Email = "jane.doe@example.com"
            };
            _mockUnitOfWork.Setup(u => u.Authors.AddAsync(It.IsAny<Author>())).Throws(new Exception("Database error"));

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await _authorServices.CreateAuthor(request));
            Assert.AreEqual("An error occurred while adding a new author.", ex.Message);
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
