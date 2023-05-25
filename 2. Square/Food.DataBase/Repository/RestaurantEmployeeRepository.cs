using Food.Domain.Interface.Entities;
using Food.Domain.Interface.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.DataBase.Repository
{
    public class RestaurantEmployeeRepository : IRestaurantEmployeeRepository
    {
        private readonly FoodDBContext _foodDBContext;

        public RestaurantEmployeeRepository(FoodDBContext foodDBContext)
        {
            this._foodDBContext = foodDBContext;
        }

        public async Task<RestaurantEmployeeEntity> Add(RestaurantEmployeeEntity restaurantEmployee)
        {
            _foodDBContext.Set<RestaurantEmployeeEntity>().Add(restaurantEmployee);
            await _foodDBContext.SaveChangesAsync();
            return restaurantEmployee;
        }

        public Task<RestaurantEmployeeEntity> DeleteById(RestaurantEmployeeEntity restaurant)
        {
            throw new NotImplementedException();
        }

        public Task<RestaurantEmployeeEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<RestaurantEmployeeEntity> GetById(int id)
        {
            return await _foodDBContext.RestaurantEmployees.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<RestaurantEmployeeEntity> GetRestaurantByEmployee(int idEmployee)
        {
            return await _foodDBContext.RestaurantEmployees.Where(x => x.IdPersona == idEmployee).FirstOrDefaultAsync();
        }

        public Task<RestaurantEmployeeEntity> UpdateById(RestaurantEmployeeEntity restaurant)
        {
            throw new NotImplementedException();
        }
    }
}
