using Food.Domain.Business.DTO;
using Food.Domain.Business.SMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Interface.IServices.IMessengerProxy
{
    public interface IMessengerProxyServices
    {
        Task<StandardResponse> PostAsync(string MethodName, SMSDTO userDTO);
    }
}
