using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Domain.Business.DTO.FoodProxyDTO
{
    public class FoodRestauranteEmployeeDTO
    {
        [Column(Order = 1)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Id de la Persona es obligatorio.")]
        [Column(Order = 2)]
        [Range(1, int.MaxValue, ErrorMessage = "El Id del Propietario debe ser mayor a 0")]
        public int IdPersona { get; set; }

        [Required(ErrorMessage = "El Id del restaurante es obligatorio.")]
        [Column(Order = 3)]
        [Range(1, int.MaxValue, ErrorMessage = "El Id del Propietario debe ser mayor a 0")]
        public int IdRestaurante { get; set; }
    }
}
