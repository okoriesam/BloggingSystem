using Application.Authors.DataTransferObject;
using Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Blogs.DataTransferObject
{
    public class BlogsResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
        public Guid AuthorId { get; set; }
    }
}
