using Food.Domain.Business.DTO;
using Food.Domain.Business.UserProxyDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Interface.IServices.IUserProxy
{
    public interface IUserProxyServices
    {
        Task<StandardResponse> GetAsync(string MethodName, List<dynamic> Header, List<dynamic> Values);
        Task<StandardResponse> PostAsync(string MethodName, UserDTO userDTO);
        Task<StandardResponse> PutAsync(StandardRequest request);
    }
}
