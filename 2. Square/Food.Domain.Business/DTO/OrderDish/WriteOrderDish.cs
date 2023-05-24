using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Business.DTO.OrderDish
{
    public class WriteOrderDish
    {
        [MaxLength(50)]
        [Column(Order = 2)]
        public int IdPedido { get; set; }

        [MaxLength(100)]
        [Column(Order = 3)]
        public int? IdPlato { get; set; }

        [Column(Order = 4)]
        public int Cantidad { get; set; }
    }
}
