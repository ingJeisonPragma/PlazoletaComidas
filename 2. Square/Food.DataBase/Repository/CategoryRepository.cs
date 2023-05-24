using Food.DataBase.Paginate;
using Food.Domain.Business.DTO;
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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly FoodDBContext _foodDBContext;

        public CategoryRepository(FoodDBContext foodDBContext)
        {
            this._foodDBContext = foodDBContext;
        }
        public async Task<CategoryEntity> Add(CategoryEntity dish)
        {
            _foodDBContext.Set<CategoryEntity>().Add(dish);
            await _foodDBContext.SaveChangesAsync();
            return dish;
        }

        public Task<CategoryEntity> DeleteById(CategoryEntity Category)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryEntity> Update(CategoryEntity Category)
        {
            _foodDBContext.Entry(Category).State = EntityState.Modified;
            await _foodDBContext.SaveChangesAsync();
            return Category;
        }

        public async Task<List<CategoryEntity>> GetAll(int IdRestaurant)
        {
            var collection = await _foodDBContext.Categories
                .Include(c => c.Dishes.Where(c => c.Activo == true && c.IdRestaurant == IdRestaurant)).ToListAsync();
            return collection;
        }

        public async Task<CategoryEntity> GetById(int id)
        {
            return await _foodDBContext.Categories.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
