using Food.Domain.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Interface.IServices
{
    public interface IRestaurantServices
    {
        Task<StandardResponse> GetByIdRestaurant(int Id);
        Task<StandardResponse> CreateRestaurant(RestaurantDTO restaurant);
        Task<StandardResponse> GetValidateRestaurantOwner(int IdRestaurante, int IdPropietario);

        Task<StandardResponse> GetListRestaurant(int page, int take);
    }
}
