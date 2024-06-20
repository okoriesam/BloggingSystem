using Domain.entities;
using Domain.events.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Blog> _blogs;
        private IGenericRepository<Author> _authors;
        private IGenericRepository<Post> _posts;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Blog> Blogs => _blogs ??= new GenericRepository<Blog>(_context);
        public IGenericRepository<Author> Authors => _authors ??= new GenericRepository<Author>(_context);
        public IGenericRepository<Post> Posts => _posts ??= new GenericRepository<Post>(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
