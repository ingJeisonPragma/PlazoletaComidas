using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Interface.Entities
{
    public class OrderDishEntity
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        [Column(Order = 2)]
        public int IdPedido { get; set; }
        [ForeignKey("IdPedido")]
        public OrderEntity Order { get; set; }

        [MaxLength(100)]
        [Column(Order = 3)]
        public int? IdPlato { get; set; }
        [ForeignKey("IdPlato")]
        public DishEntity Dish { get; set; }

        [Column(Order = 4)]
        public int Cantidad { get; set; }
    }
}
