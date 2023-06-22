using Food.Domain.Business.DTO;
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
        Task<PaginatedListDTO<OrderEntity>> GetOrderState(int IdRstaurant, string State, int page, int take);
        Task<OrderEntity> GetById(int IdOrder);
        Task<OrderEntity> UpdateOrder(OrderEntity order);
    }
}
