using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Business.DTO;
using User.Domain.Interface.IServices;
using User.Domain.Interface.IServices.IFoodProxy;
using Microsoft.Extensions.Options;
using User.Domain.Business.DTO.FoodProxyDTO;

namespace User.Domain.Services.Services.FoodProxy
{
    public class FoodProxyServices : IFoodProxyServices
    {
        private readonly IHttpPetitionServices _httpPetition;
        private readonly IConfiguration _configuration;

        public FoodProxyServices(IHttpPetitionServices httpPetition, IConfiguration configuration)
        {
            _httpPetition = httpPetition;
            _configuration = configuration;
        }
        public async Task<StandardResponse> GetAsync(string MethodName, List<dynamic> Header, List<dynamic> Values)
        {
            StandardRequest str = new()
            {
                RequestType = 1,
                HeaderParameters = Header,
                ValuesParameters = Values,
                URL = _configuration["Apis:urlFood"], //_bearer.Access_Token
                MethodName = MethodName,
                IsAuthorize = true,
                Token = _configuration["Tokens:AccessToken"],
                IsPragma = true,
            };

            return await _httpPetition.PetitionStandard(str);
        }

        public async Task<StandardResponse> PostAsync(string MethodName, object Body)
        {
            StandardRequest str = new()
            {
                RequestType = 2,
                ValueBody = Body,
                URL = _configuration["Apis:urlFood"],
                MethodName = MethodName,
                IsAuthorize = true,
                Token = _configuration["Tokens:AccessToken"],
                IsPragma = true,
            };

            return await _httpPetition.PetitionStandard(str);
        }

        public Task<StandardResponse> PutAsync(StandardRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
