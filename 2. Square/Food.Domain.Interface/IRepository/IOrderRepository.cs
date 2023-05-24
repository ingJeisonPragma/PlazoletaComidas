using Food.Domain.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Interface.IRepository
{
    public interface IOrderRepository
    {
        Task<List<OrderEntity>> ValidateOrderCustomer(int IdCustomer);
        Task<OrderEntity> AddOrder(OrderEntity order);
    }
}
