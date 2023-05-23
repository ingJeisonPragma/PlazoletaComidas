using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Business.DTO;
using User.Domain.Business.DTO.FoodProxyDTO;
using User.Domain.Interface.IServices.IFoodProxy;

namespace User.Domain.Services.Services.FoodProxy
{
    public class FoodServices : IFoodServices
    {
        private readonly IFoodProxyServices _foodProxy;

        public FoodServices(IFoodProxyServices foodProxy)
        {
            this._foodProxy = foodProxy;
        }

        public async Task<StandardResponse> ValidateRestaurantOwner(int Restaurante, int Propietario)
        {
            List<dynamic> Header = new() { "IdRestaurante", "IdPropietario" };
            List<dynamic> Value = new() { Restaurante, Propietario };
            var response = await _foodProxy.GetAsync("/api/Restaurant/GetRestaurantOwner", Header, Value);

            return response;
        }

        public async Task<StandardResponse> CreateRestaurantOwner(int Restaurante, int Propietario)
        {
            FoodRestauranteEmployeeDTO foodRestauranteEmployee = new() { IdPersona = Propietario, IdRestaurante = Restaurante };
            var response = await _foodProxy.PostAsync("/api/RestaurantEmployee/AddRestaurantEmployee", foodRestauranteEmployee);

            return response;
        }
    }
}
