using Application.Authors.DataTransferObject;
using Application.Blogs.DataTransferObject;
using Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.DataTransferObject
{
    public class PostsResponse
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime DatePublished { get; set; }
        public Guid BlogId { get; set; }
    }
}
