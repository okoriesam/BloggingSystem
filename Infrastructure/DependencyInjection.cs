using Application.Authors.Services;
using Application.Blogs.Services;
using Application.Posts.Services;
using Domain.events.repository;
using Infrastructure.Automapper;
using Infrastructure.Implementation;
using Infrastructure.Persistence.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
          
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IPostServices, PostServices>();
            services.AddTransient<IAuthorServices, AuthorServices>();
            services.AddTransient<IBlogServices, BlogServices>();
          
            services.AddAutoMapper(typeof(AuthorMappingProfile));
            return services;
        }
    }
}
