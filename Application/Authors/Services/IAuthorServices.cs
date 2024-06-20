using Application.Authors.DataTransferObject;
using Domain.entities;
using Domain.events.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authors.Services
{
    public interface IAuthorServices : IGenericRepository<Author>
    {
        Task<AuthorResponse> CreateAuthor(CreateAuthorRequest request);
    }
}
