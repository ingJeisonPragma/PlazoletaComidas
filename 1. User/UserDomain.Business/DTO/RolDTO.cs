using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace User.Domain.Business.DTO
{
    public class RolDTO
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [MaxLength(20)]
        public string Descripcion { get; set; }
    }
}
