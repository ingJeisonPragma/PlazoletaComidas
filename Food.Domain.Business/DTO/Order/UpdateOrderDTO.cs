using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Food.Domain.Business.DTO.OrderDish;

namespace Food.Domain.Business.DTO.Order
{
    public class UpdateOrderDTO
    {
        public int IdPedido { get; set; }
        //public int IdChef { get; set; }
    }
}
