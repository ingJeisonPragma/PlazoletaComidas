using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Domain.Business.DTO
{
    public class StandardResponse
    {
        public bool IsSuccess { get; set; }

        public string? Message { get; set; }

        public object? Result { get; set; }
    }
}
