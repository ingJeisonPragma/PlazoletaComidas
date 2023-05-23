using Food.Domain.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Interface.Exceptions
{
    public class DomainValidateException : Exception
    {
        public StandardResponse Standard { get; }
        public DomainValidateException(StandardResponse standard) : base("")
        {
            Standard = standard;
        }
    }
}
