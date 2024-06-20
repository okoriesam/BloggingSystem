using Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authors.DataTransferObject
{
    public class AuthorResponse
    {
        public Guid id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}
