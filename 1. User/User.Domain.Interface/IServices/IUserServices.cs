using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain;
using User.Domain.Business.DTO;

namespace User.Domain.Interface.IServices
{
    public interface IUserServices
    {
        Task<StandardResponse> CreateOwner(UserDTO user);
        Task<StandardResponse> GetUser(int Id);
        Task<UserResponseDTO?> GetValidateCredential(string user, string Pass);
        Task<StandardResponse> CreateEmployee(UserDTO user);
        Task<StandardResponse> CreateCustomer(UserDTO user);
    }
}
