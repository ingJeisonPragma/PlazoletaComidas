using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Interface.IServices.IMessengerProxy
{
    public interface IMessengerServices
    {
        Task<bool> SendSMS(string To, string msg);
    }
}
