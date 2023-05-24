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
    }
}
