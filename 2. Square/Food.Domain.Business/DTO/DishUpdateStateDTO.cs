using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Business.DTO
{
    public class DishUpdateStateDTO
    {
        public int IdPlato { get; set; }

        [Required]
        public bool Activo { get; set; }
    }
}
