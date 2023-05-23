using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Business.DTO;

namespace User.Domain.Interface.IServices.IFoodProxy
{
    public interface IFoodProxyServices
    {
        Task<StandardResponse> GetAsync(string MethodName, List<dynamic> Header, List<dynamic> Values);
        Task<StandardResponse> PostAsync(string MethodName, object Body);
        Task<StandardResponse> PutAsync(StandardRequest request);
    }
}
