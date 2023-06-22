using Food.Domain.Business.DTO;
using Food.Domain.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Interface.IRepository
{
    public interface IDishRepository
    {
        Task<DishEntity> GetById(int id);
        Task<List<DishEntity>> GetAll(int IdRestaurant, int page, int take);
        Task<DishEntity> Add(DishEntity dish);
        Task<DishEntity> DeleteById(DishEntity dish);
        Task<DishEntity> Update(DishEntity dish);
    }
}
