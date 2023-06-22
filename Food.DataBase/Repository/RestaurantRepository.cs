using Food.Domain.Interface.Entities;
using Food.Domain.Interface.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Food.DataBase.Paginate;
using System.Reflection.Metadata.Ecma335;
using Food.Domain.Business.DTO;

namespace Food.DataBase.Repository
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly FoodDBContext _foodDBContext;

        public RestaurantRepository(FoodDBContext foodDBContext)
        {
            this._foodDBContext = foodDBContext;
        }

        public async Task<RestaurantEntity> Add(RestaurantEntity restaurant)
        {
            _foodDBContext.Set<RestaurantEntity>().Add(restaurant);
            await _foodDBContext.SaveChangesAsync();
            return restaurant;
        }

        public Task<RestaurantEntity> DeleteById(RestaurantEntity restaurant)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginatedListDTO<RestaurantEntity>> GetAll(int page, int take)
        {
            var collection = await _foodDBContext.Restaurants.GetPagedAsync(page, take);
            return collection;
        }

        public async Task<RestaurantEntity> GetById(int id)
        {
            return await _foodDBContext.Restaurants.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<RestaurantEntity> UpdateById(RestaurantEntity restaurant)
        {
            throw new NotImplementedException();
        }
    }
}
