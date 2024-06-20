using Domain.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.entities
{
    public class Blog : BaseEntity
    {
        public string? Name { get; set; }
        public string? Url { get; set; }
        public Guid AuthorId { get; set; }
        public Author? Author { get; set; }
        public ICollection<Post>? Posts { get; set; }
    }
}
