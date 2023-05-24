using Food.Domain.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Interface.IServices
{
    public interface IOrderServices
    {
        Task<StandardResponse> CreateOrder(OrderDTO order);
        Task<StandardResponse> GetPending(int IdEmployee, int page, int take);
    }
}
