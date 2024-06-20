using Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Domain.events.repository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Blog> Blogs { get; }
        IGenericRepository<Author> Authors { get; }
        IGenericRepository<Post> Posts { get; }
        Task<int> SaveChangesAsync();
    }
}
