﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authors.DataTransferObject
{
    public class CreateAuthorRequest
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}
