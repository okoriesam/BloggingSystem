using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.DataTransferObject
{
    public class CreatePostRequest
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime DatePublished { get; set; }
        public Guid BlogId { get; set; }
    }
}
