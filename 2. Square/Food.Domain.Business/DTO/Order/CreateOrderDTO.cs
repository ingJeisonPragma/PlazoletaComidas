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
    public class CreateOrderDTO
    {
        [Required]
        [Column(Order = 1)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Id del cliente es obligatorio.")]
        [Display(Name = "IdCliente")]
        [Column(Order = 2)]
        public int IdCliente { get; set; }

        [Required]
        [Display(Name = "Fecha")]
        [Column(Order = 3)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        [MaxLength(20)]
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        public int? IdChef { get; set; } = 0;

        [Required(ErrorMessage = "El Id del Restaurante es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El {0} es obligatorio.")]
        public int IdRestaurante { get; set; }

        [Required(ErrorMessage = "La lista de platos es necesaria para crear el pedido.")]
        public List<WriteOrderDishDTO> orderDishes { get; set; }
    }
}
