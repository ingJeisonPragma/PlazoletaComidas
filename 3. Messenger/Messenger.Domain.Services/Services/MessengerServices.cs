using Messenger.Domain.Business.DTO;
using Messenger.Domain.Services.Exceptions;
using Messenger.Domain.Services.IServices;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Clients;
using Twilio.Http;
using Twilio.Rest.Api.V2010.Account;

namespace Messenger.Domain.Services.Services
{
    public class MessengerServices : ITwilioRestClient, IMessengerServices
    {
        private readonly IConfiguration _configuration;
        private readonly ITwilioRestClient _innerClient;

        public MessengerServices(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public Response Request(Request request) => _innerClient.Request(request);
        public Task<Response> RequestAsync(Request request) => _innerClient.RequestAsync(request);
        public string AccountSid => _innerClient.AccountSid;
        public string Region => _innerClient.Region;
        public Twilio.Http.HttpClient HttpClient => _innerClient.HttpClient;

        public async Task<StandardResponse> SendSMS(TwilioRequestDTO requestDTO)
        {
            try
            {
                TwilioClient.Init(_configuration["Twilio:AccountSID"], _configuration["Twilio:AuthToken"]);
                var message = await MessageResource.CreateAsync(
                    to: new Twilio.Types.PhoneNumber(requestDTO.ToNumber),
                    from: new Twilio.Types.PhoneNumber(_configuration["Twilio:FromNumber"]),
                    body: requestDTO.msg
                    );
            }
            catch
            {
                throw new SmsException(new StandardResponse { IsSuccess = false, Message = "Error enviando el mensaje."});
            }

            return new StandardResponse() { IsSuccess = true, Message = "SMS enviado con exito."};
        }
    }
}
