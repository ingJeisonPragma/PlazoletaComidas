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
    public class DishRepository : IDishRepository
    {
        private readonly FoodDBContext _foodDBContext;

        public DishRepository(FoodDBContext foodDBContext)
        {
            this._foodDBContext = foodDBContext;
        }
        public async Task<DishEntity> Add(DishEntity dish)
        {
            _foodDBContext.Set<DishEntity>().Add(dish);
            await _foodDBContext.SaveChangesAsync();
            return dish;
        }

        public Task<DishEntity> DeleteById(DishEntity dish)
        {
            throw new NotImplementedException();
        }

        public Task<DishEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<DishEntity> GetById(int id)
        {
            return await _foodDBContext.Dishes.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<DishEntity> Update(DishEntity dish)
        {
            _foodDBContext.Entry(dish).State = EntityState.Modified;
            await _foodDBContext.SaveChangesAsync();
            return dish;
        }
    }
}
