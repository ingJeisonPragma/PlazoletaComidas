using Messenger.Domain.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Domain.Services.IServices
{
    public interface IMessengerServices
    {
        Task<StandardResponse> SendSMS(TwilioRequestDTO requestDTO);
    }
}
