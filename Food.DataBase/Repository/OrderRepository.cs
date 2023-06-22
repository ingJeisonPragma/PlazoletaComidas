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
    public class OrderRepository : IOrderRepository
    {
        private readonly FoodDBContext _foodDBContext;

        public OrderRepository(FoodDBContext foodDBContext)
        {
            this._foodDBContext = foodDBContext;
        }

        public async Task<PaginatedListDTO<OrderEntity>> GetOrderState(int IdRstaurant, string State, int page, int take)
        {
            var orderEntities = await _foodDBContext.Orders
                //.Include(o => o.restaurant)
                .Where(o => o.Estado == State && o.IdRestaurante == IdRstaurant)
                .GetPagedAsync(page, take);
            return orderEntities;
        }

        public async Task<List<OrderEntity>> ValidateOrderCustomer(int IdCustomer)
        {
            var orderEntities = await _foodDBContext.Orders.Where(o => o.IdCliente == IdCustomer).ToListAsync();
            return orderEntities;
        }

        public async Task<OrderEntity> AddOrder(OrderEntity order)
        {
            _foodDBContext.Set<OrderEntity>().Add(order);
            var dat = await _foodDBContext.SaveChangesAsync();
            return order;
        }

        public async Task<OrderEntity> GetById(int IdOrder)
        {
            var orderEntity = await _foodDBContext.Orders.Where(o => o.Id == IdOrder).FirstOrDefaultAsync();
            return orderEntity;
        }

        public async Task<OrderEntity> UpdateOrder(OrderEntity order)
        {
            _foodDBContext.Entry(order).State = EntityState.Modified;
            var dat = await _foodDBContext.SaveChangesAsync();
            return order;
        }
    }
}
