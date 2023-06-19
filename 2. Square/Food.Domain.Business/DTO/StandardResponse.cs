﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Business.DTO
{
    public class StandardResponse
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; } = string.Empty;

        public object Result { get; set; } = new();
    }
}
