using Food.Domain.Business.DTO;
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
        Task<StandardResponse> PostAsync(StandardRequest request);
        Task<StandardResponse> PutAsync(StandardRequest request);
    }
}
