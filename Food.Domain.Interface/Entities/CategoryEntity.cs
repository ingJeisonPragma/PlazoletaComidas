using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Interface.Entities
{
    public class CategoryEntity
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        [Column(Order = 2)]
        public string Nombre { get; set; }

        [MaxLength(100)]
        [Column(Order = 3)]
        public string Descripcion { get; set; }

        public virtual List<DishEntity> Dishes { get; set; }
    }
}
