using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Business.DTO
{
    public class CategoryDTO
    {
        [Column(Order = 1)]
        public int Id { get; set; }

        [Column(Order = 2)]
        public string Nombre { get; set; } = string.Empty;

        [Column(Order = 3)]
        public string Descripcion { get; set; } = string.Empty;

        public virtual List<DishCategoryDTO> Dishes { get; set; } = new();
    }
}
