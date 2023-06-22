using Food.Domain.Business.DTO;
using Food.Domain.Business.SMS;
using Food.Domain.Interface.IServices;
using Food.Domain.Interface.IServices.IMessengerProxy;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Services.Services.MessengerProxy
{
    public class MessengerProxyServices : IMessengerProxyServices
    {
        private readonly IHttpPetitionServices _httpPetition;
        private readonly IConfiguration _configuration;

        public MessengerProxyServices(IHttpPetitionServices httpPetition, IConfiguration configuration)
        {
            this._httpPetition = httpPetition;
            this._configuration = configuration;
        }

        public async Task<StandardResponse> PostAsync(string MethodName, SMSDTO userDTO)
        {
            StandardRequest str = new()
            {
                RequestType = 2,
                URL = _configuration["Apis:urlSMS"],
                ValueBody = userDTO,
                MethodName = MethodName,
                IsAuthorize = true,
                Token = _configuration["Tokens:AccessToken"],
                IsPragma = true,
            };

            return await _httpPetition.PetitionStandard(str);
        }
    }
}
