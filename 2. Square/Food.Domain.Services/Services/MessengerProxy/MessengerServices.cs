//using Food.Domain.Interface.IServices.ITwilioProxy;
using Food.Domain.Business.SMS;
using Food.Domain.Interface.IServices.IMessengerProxy;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Twilio;
using Twilio.Clients;
using Twilio.Http;
using Twilio.Rest.Api.V2010.Account;

namespace Food.Domain.Services.Services.MessengerProxy
{
    public class MessengerServices : IMessengerServices
    {
        private readonly IMessengerProxyServices _messengerProxyServices;

        public MessengerServices(
            IMessengerProxyServices messengerProxyServices)
        {
            this._messengerProxyServices = messengerProxyServices;
        }

        public async Task<bool> SendSMS(string To, string msg)
        {
            SMSDTO sms = new() { ToNumber = To, msg = msg };

            var response = await _messengerProxyServices.PostAsync("/api/SMS/SendSMS", sms);

            return response.IsSuccess;
        }
    }
}
