using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Business.DTO
{
    public class DishUpdateDTO
    {
        public int IdPlato { get; set; }

        [Required(ErrorMessage = "La Descripción es un campo obligatorio.")]
        [MaxLength(500, ErrorMessage = "La Descripción excede los 500 caracteres permitidos.")]
        public string Descripcion { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Precio { get; set; }
    }
}
