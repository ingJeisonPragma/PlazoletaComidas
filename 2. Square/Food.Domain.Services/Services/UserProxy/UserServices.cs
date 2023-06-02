using Food.Domain.Business.DTO;
using Food.Domain.Business.UserProxyDTO;
using Food.Domain.Interface.Exceptions;
using Food.Domain.Interface.IServices;
using Food.Domain.Interface.IServices.IUserProxy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Services.Services.UserProxy
{
    public class UserServices : IUserServices
    {
        private readonly IUserProxyServices _userProxy;

        public UserServices(IUserProxyServices userProxy)
        {
            this._userProxy = userProxy;
        }

        //Se encarga en el micro de User que el IdPropietario si corresponda al Rol
        public async Task<StandardResponse> ValidateUserOwner(int IdPropietario)
        {
            var response = await ValidateUser(IdPropietario);

            if (response.IsSuccess)
            {
                var user = JsonConvert.DeserializeObject<UserDTO>(response.Result.ToString());
                if (user.IdRol != 2)
                    return new StandardResponse() { IsSuccess = false, Message = "El usuario no tiene el Rol de Propietario." };
            }

            return response;
        }

        public async Task<UserDTO?> ValidateUserCustomer(int IdCustomer)
        {
            var response = await ValidateUser(IdCustomer);

            if (response.IsSuccess)
            {
                var user = JsonConvert.DeserializeObject<UserDTO>(response.Result.ToString());
                return user;
            }

            return null;
        }

        private async Task<StandardResponse> ValidateUser(int IdUser)
        {
            List<dynamic> Header = new() { "Id" };
            List<dynamic> Value = new() { IdUser };

            var response = await _userProxy.GetAsync("/api/User/GetUser", Header, Value);

            return response;
        }
    }
}
