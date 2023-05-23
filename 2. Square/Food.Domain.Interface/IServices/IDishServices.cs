using Food.Domain.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Interface.IServices
{
    public interface IDishServices
    {
        Task<StandardResponse> CreateDish(DishDTO dish);
        Task<StandardResponse> UpdateDish(DishUpdateDTO dish);
        Task<StandardResponse> UpdateDishState(DishUpdateStateDTO dish, int IdPropietario);
    }
}
