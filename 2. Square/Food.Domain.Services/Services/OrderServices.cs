using Food.Domain.Business.DTO;
using Food.Domain.Interface.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Services.Services
{
    public class OrderServices : IOrderServices
    {
        public OrderServices()
        {
            
        }

        public async Task<StandardResponse> CreateOrder(OrderDTO order)
        {
            throw new NotImplementedException();
        }
    }
}
