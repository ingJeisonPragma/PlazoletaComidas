using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Business.DTO.OrderDish
{
    public class QueryOrderDishDTO
    {
        [Column(Order = 2)]
        public int IdPedido { get; set; }

        [Display(Name = "IdPlato")]
        [Column(Order = 3)]
        public int IdPlato { get; set; }

        public int NombrePlato { get; set; }

        [Display(Name = "Cantidad")]
        [Column(Order = 4)]
        public int Cantidad { get; set; }
    }
}
