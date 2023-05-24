using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Business.DTO.OrderDish
{
    public class WriteOrderDishDTO
    {
        [Column(Order = 2)]
        public int IdPedido { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El {0} es obligatorio.")]
        [Required(ErrorMessage = "El Id del plato seleccionado es obligatorio.")]
        [Display(Name = "IdPlato")]
        [Column(Order = 3)]
        public int IdPlato { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La {0} a pedir del plato es obligatorio.")]
        [Required(ErrorMessage = "Se requiere la cantidad por cada plato solicitado.")]
        [Display(Name = "Cantidad")]
        [Column(Order = 4)]
        public int Cantidad { get; set; }
    }
}
