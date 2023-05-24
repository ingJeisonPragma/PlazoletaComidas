using Food.Domain.Business.DTO;
using Food.Domain.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Interface.IRepository
{
    public interface ICategoryRepository
    {
        Task<CategoryEntity> GetById(int id);
        Task<List<CategoryEntity>> GetAll(int IdRestaurant);
        Task<CategoryEntity> Add(CategoryEntity Category);
        Task<CategoryEntity> DeleteById(CategoryEntity Category);
        Task<CategoryEntity> Update(CategoryEntity Category);
    }
}
