using Application.Authors.DataTransferObject;
using Application.Authors.Services;
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

namespace Infrastructure.Implementation
{
    public class AuthorServices : GenericRepository<Author>, IAuthorServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AuthorServices(ApplicationDbContext context, IMapper mapper, IUnitOfWork unitOfWork) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthorResponse> CreateAuthor(CreateAuthorRequest request)
        {
            try
            {
                var newAuthor = new Author
                {
                    id = Guid.NewGuid(),
                    Name = request.Name,
                    Email = request.Email,
                };
                await _unitOfWork.Authors.AddAsync(newAuthor);
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<AuthorResponse>(newAuthor);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding a new author.", ex);
            }
        }
    }
}
