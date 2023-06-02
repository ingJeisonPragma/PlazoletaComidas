using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Business.SMS
{
    public class SMSDTO
    {
        public string ToNumber { get; set; }
        public string msg { get; set; }
    }
}
