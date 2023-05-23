using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Business.DTO;

namespace User.Domain.Interface.IServices.IFoodProxy
{
    public interface IFoodServices
    {
        Task<StandardResponse> ValidateRestaurantOwner(int Idrestaurante, int IdPropietario);
        Task<StandardResponse> CreateRestaurantOwner(int Restaurante, int Propietario);
    }
}
