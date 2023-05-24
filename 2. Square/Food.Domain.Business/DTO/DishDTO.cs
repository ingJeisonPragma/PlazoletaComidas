using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Business.DTO
{
    public class DishDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre es un campo obligatorio.")]
        [MaxLength(100, ErrorMessage = "El Nombre excede los 100 caracteres permitidos.")]
        [Display(Name = "Nombre")]
        [CustomValidation(typeof(NameValidation), "NameValidate")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La Categoria es un campo obligatorio.")]
        [Display(Name = "Categoria")]
        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "La Descripción es un campo obligatorio.")]
        [MaxLength(500, ErrorMessage = "La Descripción excede los 500 caracteres permitidos.")]
        public string Descripcion { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El Id del restaurante es obligatorio.")]
        public int IdRestaurant { get; set; }

        //[Required(ErrorMessage = "El requerido el código del propietario.")]
        //public int IdPropietario { get; set; }

        [Required(ErrorMessage = "La Url de la imagen en un campo obligatorio.")]
        [MaxLength(1000, ErrorMessage = "La Url excede los 1000 caracteres permitidos.")]
        [Display(Name = "Imagen")]
        public string urlImagen { get; set; }
        public bool Activo { get; set; }
    }
}
