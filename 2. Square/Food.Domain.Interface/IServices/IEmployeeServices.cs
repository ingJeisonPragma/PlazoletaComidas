using Food.Domain.Business.DTO;
using Food.Domain.Business.UserProxyDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Interface.IServices
{
    public interface IEmployeeServices
    {
        Task<StandardResponse> CreateEmployee(UserDTO user, int IdPropietario);
    }
}
