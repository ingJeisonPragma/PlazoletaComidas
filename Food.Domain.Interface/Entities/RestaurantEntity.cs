using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Interface.Entities
{
    public class RestaurantEntity
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nit")]
        [Column(Order = 2)]
        public int Nit { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Nombre")]
        [Column(Order = 3)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Direccion")]
        [Column(Order = 4)]
        public string Direccion { get; set; }

        [Column(Order = 5)]
        public int? IdPropietario { get; set; }

        [Required]
        [MaxLength(13)]
        [Display(Name = "Telefono")]
        [Column(Order = 6)]
        public string Telefono { get; set; }

        [Required]
        [MaxLength(1000)]
        [Display(Name = "urlLogo")]
        [Column(Order = 7)]
        public string urlLogo { get; set; }

        public virtual OrderEntity Order { get; set; }
        public virtual List<DishEntity> Dishes { get; set; }
    }
}
