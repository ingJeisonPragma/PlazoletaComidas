using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Interface.Entities;

namespace User.Domain.Interface.IRepository
{
    public interface IUserRepository
    {
        Task<List<UserEntity>> GetAll();
        Task<UserEntity> GetById(int id);
        Task<UserEntity> GetDocument(int Document);
        Task<UserEntity> GetEmail(string Email);
        Task<UserEntity> Add(UserEntity entity);
        Task<UserEntity> Update(UserEntity entity);
        Task<UserEntity> Delete(UserEntity entity);
    }
}
