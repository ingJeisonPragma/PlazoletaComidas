using Food.Domain.Business.DTO;
using Food.Domain.Business.UserProxyDTO;
using Food.Domain.Interface.IServices;
using Food.Domain.Interface.IServices.IUserProxy;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Services.Services.UserProxy
{
    public class UserProxyServices : IUserProxyServices
    {
        private readonly IHttpPetitionServices _httpPetition;
        private readonly IConfiguration _configuration;

        public UserProxyServices(IHttpPetitionServices httpPetition, IConfiguration configuration)
        {
            this._httpPetition = httpPetition;
            this._configuration = configuration;
        }
        public async Task<StandardResponse> GetAsync(string MethodName, List<dynamic> Header, List<dynamic> Values)
        {
            StandardRequest str = new()
            {
                RequestType = 1,
                HeaderParameters = Header,
                ValuesParameters = Values,
                URL = _configuration["Apis:urlUser"],
                //ValueBody = request,
                MethodName = MethodName,
                IsAuthorize = true,
                Token = _configuration["Tokens:AccessToken"],
                IsPragma = true,
            };

            return await _httpPetition.PetitionStandard(str);
        }

        public async Task<StandardResponse> PostAsync(string MethodName, UserDTO userDTO)
        {
            StandardRequest str = new()
            {
                RequestType = 2,
                URL = _configuration["Apis:urlUser"],
                ValueBody = userDTO,
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
