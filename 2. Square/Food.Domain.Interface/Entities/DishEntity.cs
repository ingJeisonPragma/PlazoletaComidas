using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Interface.Entities
{
    public class DishEntity
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        [Column(Order = 2)]
        public string Nombre { get; set; }

        [Column(Order = 3)]
        public int IdCategoria { get; set; }
        [ForeignKey("IdCategoria")]
        public CategoryEntity Category { get; set; }

        [MaxLength(500)]
        [Column(Order = 4)]
        public string Descripcion { get; set; }

        [Column(Order = 5)]
        public int Precio { get; set; }

        [Column(Order = 6)]
        public int IdRestaurant { get; set; }
        [ForeignKey("IdRestaurant")]
        public RestaurantEntity Restaurant { get; set; }

        [Column(Order = 7)]
        [MaxLength(1000)]
        public string urlImagen { get; set; }
        public bool Activo { get; set; }

        public virtual List<OrderEntity> Orders { get; set; }
    }
}
