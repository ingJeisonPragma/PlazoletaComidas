using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Business.DTO;

namespace User.Domain.Interface.Exceptions
{
    public class DomainUserValidateException : Exception
    {
        public StandardResponse Standard { get; }
        public DomainUserValidateException(StandardResponse standard) : base("")
        {
            Standard = standard;
        }
    }
}
