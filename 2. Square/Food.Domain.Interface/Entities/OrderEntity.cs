using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Interface.Entities
{
    public class OrderEntity
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "IdCliente")]
        [Column(Order = 2)]
        public int IdCliente { get; set; }

        [Required]
        [Display(Name = "Fecha")]
        [Column(Order = 3)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "Estado")]
        [Column(Order = 4)]
        public string Estado { get; set; }

        [MaxLength(20)]
        public int? IdChef { get; set; }
        [ForeignKey("IdChef")]
        public RestaurantEmployeeEntity restaurantEmployee { get; set; }

        public int IdRestaurante { get; set; }
        [ForeignKey("IdRestaurante")]
        public RestaurantEntity restaurant { get; set; }

        public virtual List<OrderDishEntity>? OrderDishes { get; set; }
    }
}
