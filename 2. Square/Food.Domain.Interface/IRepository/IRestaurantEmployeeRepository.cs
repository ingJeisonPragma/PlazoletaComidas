using Food.Domain.Business.DTO;
using Food.Domain.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Interface.IRepository
{
    public interface IRestaurantEmployeeRepository
    {
        Task<RestaurantEmployeeEntity> GetById(int id);
        Task<RestaurantEmployeeEntity> GetAll();
        Task<RestaurantEmployeeEntity> Add(RestaurantEmployeeEntity restaurant);
        Task<RestaurantEmployeeEntity> DeleteById(RestaurantEmployeeEntity restaurant);
        Task<RestaurantEmployeeEntity> UpdateById(RestaurantEmployeeEntity restaurant);
    }
}
