using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Interface.Entities;
using User.Domain.Interface.IRepository;

namespace User.DataBase.Repository
{
    public class userRepository : IUserRepository
    {
        private readonly UserDBContext _userDBContext;

        public userRepository(UserDBContext userDBContext)
        {
            _userDBContext = userDBContext;
        }

        public async Task<UserEntity> Add(UserEntity entity)
        {
            _userDBContext.Set<UserEntity>().Add(entity);
            await _userDBContext.SaveChangesAsync();
            return entity;
        }

        public async Task<UserEntity> Delete(UserEntity entity)
        {
            _userDBContext.Set<UserEntity>().Remove(entity);
            await _userDBContext.SaveChangesAsync();
            return entity;
        }

        public Task<List<UserEntity>> GetAll()
        {
            return _userDBContext.Users
                .Include(r => r.Rol)
                .ToListAsync();
        }

        public async Task<UserEntity> GetById(int id)
        {
            return (await GetAll()).Where(x => x.Id == id).FirstOrDefault();
        }

        public async Task<UserEntity> GetDocument(int Document)
        {
            return (await GetAll()).Where(x => x.Documento == Document).FirstOrDefault();
        }

        public async Task<UserEntity> GetEmail(string Email)
        {
            return (await GetAll()).Where(x => x.Correo.ToLower() == Email.ToLower()).FirstOrDefault();
        }

        public Task<UserEntity> Update(UserEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
