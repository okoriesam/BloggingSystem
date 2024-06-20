using Application.Authors.DataTransferObject;
using Application.Blogs.DataTransferObject;
using Application.Posts.DataTransferObject;
using AutoMapper;
using Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.Automapper
{
    public class AuthorMappingProfile : Profile
    {
        public AuthorMappingProfile()
        {
            CreateMap<CreateAuthorRequest, Author>().ReverseMap();
            CreateMap<Author, AuthorResponse>().ReverseMap();

            CreateMap<CreateBlogRequest, Blog>().ReverseMap();
            CreateMap<Blog, BlogsResponse>().ReverseMap();

            CreateMap<CreatePostRequest, Post>().ReverseMap();
            CreateMap<Post, PostsResponse>().ReverseMap();
        }
    }
}
