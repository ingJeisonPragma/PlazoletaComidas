using Food.Domain.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Interface.IRepository
{
    public interface IOrderDishRepository
    {
        Task<List<OrderDishEntity>> GetOrderDish(int IdCustomer);
        Task<OrderDishEntity> AddOrderDish(OrderDishEntity order);
    }
}
