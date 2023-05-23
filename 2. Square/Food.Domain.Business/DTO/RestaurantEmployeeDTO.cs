using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Business.DTO
{
    public class RestaurantEmployeeDTO
    {
        [Column(Order = 1)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Id de la Persona es obligatorio.")]
        [Column(Order = 2)]
        public int IdPersona { get; set; }

        [Required(ErrorMessage = "El Id del restaurante es obligatorio.")]
        [Column(Order = 3)]
        public int IdRestaurante { get; set; }
    }
}
