using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Food.Domain.Business.DTO
{
    public class RestaurantDTO
    {
        public int Id { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [Display(Name = "Nit")]
        [Column(Order = 2)]
        public int Nit { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Nombre")]
        [Column(Order = 3)]
        [CustomValidation(typeof(NameValidation), "NameValidate")]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Direccion")]
        [Column(Order = 4)]
        public string Direccion { get; set; }

        [Required]
        [Column(Order = 5)]
        public int? IdPropietario { get; set; }

        [Display(Name = "Teléfono")]
        [Column(Order = 6)]
        [Required(ErrorMessage = "El {0} es un campo obligatorio.")]
        [MinLength(10, ErrorMessage = "La cantidad minima de caracteres es del {0} es {1}")]
        [MaxLength(13, ErrorMessage = "El {0} excede los {1} caracteres permitidos")]
        [RegularExpression("^[+-]?\\d+(\\.\\d+)?$", ErrorMessage = "Ingresar un número de Teléfono válido")]
        public string Telefono { get; set; }

        [Required]
        [MaxLength(1000)]
        [Display(Name = "urlLogo")]
        [Column(Order = 7)]
        public string urlLogo { get; set; }

        //public virtual OrderEntity Order { get; set; }
        //public virtual List<DishEntity> Dishes { get; set; }
    }

    public class NameValidation
    {
        public static ValidationResult NameValidate(string NameCompare)
        {
            return int.TryParse(NameCompare, out int _) ? new ValidationResult("El nombre no puede ser solo de caracteres númericos.") : ValidationResult.Success;
        }
    }
}
