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
    public class OrderDishRepository : IOrderDishRepository
    {
        private readonly FoodDBContext _foodDBContext;

        public OrderDishRepository(FoodDBContext foodDBContext)
        {
            this._foodDBContext = foodDBContext;
        }

        public async Task<List<OrderDishEntity>> GetOrderDish(int IdOrder)
        {
            var orderEntities = await _foodDBContext.OrderDishes
                .Include(od => od.Dish)
                .Where(o => o.IdPedido == IdOrder).ToListAsync();
            return orderEntities;
        }

        public async Task<OrderDishEntity> AddOrderDish(OrderDishEntity order)
        {
            _foodDBContext.Set<OrderDishEntity>().Add(order);
            var dat = await _foodDBContext.SaveChangesAsync();
            return order;
        }
    }
}
