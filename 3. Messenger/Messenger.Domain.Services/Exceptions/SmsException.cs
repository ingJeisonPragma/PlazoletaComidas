using Messenger.Domain.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Domain.Services.Exceptions
{
    public class SmsException : Exception
    {
        public StandardResponse Standard { get; }
        public SmsException(StandardResponse standard) : base("")
        {
            Standard = standard;
        }
    }
}
