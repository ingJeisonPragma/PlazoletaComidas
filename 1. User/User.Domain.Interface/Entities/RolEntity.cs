using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Domain.Interface.Entities
{
    public class RolEntity
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        [Display(Name = "Nombre")]
        [Column(Order = 2)]
        public string Nombre { get; set; }

        [MaxLength(100)]
        [Display(Name = "Descripcion")]
        [Column(Order = 3)]
        public string Descripcion { get; set; }
    }
}
