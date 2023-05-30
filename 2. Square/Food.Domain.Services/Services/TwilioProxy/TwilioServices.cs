//using Food.Domain.Interface.IServices.ITwilioProxy;
using Food.Domain.Interface.IServices.ITwilioProxy;
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

namespace Food.Domain.Services.Services.TwilioProxy
{
    public class TwilioServices : ITwilioRestClient, ITwilioServices
    {
        private readonly IConfiguration _configuration;
        private readonly ITwilioRestClient _innerClient;

        public TwilioServices(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public Response Request(Request request) => _innerClient.Request(request);
        public Task<Response> RequestAsync(Request request) => _innerClient.RequestAsync(request);
        public string AccountSid => _innerClient.AccountSid;
        public string Region => _innerClient.Region;
        public Twilio.Http.HttpClient HttpClient => _innerClient.HttpClient;

        public async Task<bool> SendSMS(string To, string msg)
        {
            TwilioClient.Init(_configuration["Twilio:AccountSID"], _configuration["Twilio:AuthToken"]);
            var message = await MessageResource.CreateAsync(
                to: new Twilio.Types.PhoneNumber(To),
                from: new Twilio.Types.PhoneNumber("+13612648733"),
                body: msg
                );

            return true;
        }
    }
}
