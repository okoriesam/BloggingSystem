using Domain.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.entities
{
    public class Post : BaseEntity
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime DatePublished { get; set; }
        public Guid BlogId { get; set; }
        public Blog? Blog { get; set; }
    }
}
