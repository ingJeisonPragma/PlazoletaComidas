using Food.Domain.Business.DTO;
using Food.Domain.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Interface.IRepository
{
    public interface IRestaurantRepository
    {
        Task<RestaurantEntity> GetById(int id);
        Task<PaginatedListDTO<RestaurantEntity>> GetAll(int page, int take);
        Task<RestaurantEntity> Add(RestaurantEntity restaurant);
        Task<RestaurantEntity> DeleteById(RestaurantEntity restaurant);
        Task<RestaurantEntity> UpdateById(RestaurantEntity restaurant);
    }
}
