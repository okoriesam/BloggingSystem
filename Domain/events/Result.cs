using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.events
{
    public class Result<T>
    {
        public T? Value { get; set; }
        public string? ErrorMessage { get; set; } = "";
        public bool HasError => this.ErrorMessage != "";
        public string ResponseCode { get; set; } = "";
        public DateTime RequestTime { get; set; } = DateTime.UtcNow;
        public DateTime? ResponseTime { get; set; } = DateTime.UtcNow;
        public bool? IsSuccess { get; set; } = false;
    }
}
