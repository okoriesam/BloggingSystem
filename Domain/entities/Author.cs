using Domain.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Domain.entities
{
    public class Author : BaseEntity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public ICollection<Blog>? Blogs { get; set; }
    }
}
