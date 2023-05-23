using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Domain.Interface.Entities
{
    public class UserEntity
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Documento")]
        [Column(Order = 2)]
        public int Documento { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Nombre")]
        [Column(Order = 3)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Apellido")]
        [Column(Order = 4)]
        public string Apellido { get; set; }

        [Required]
        [MaxLength(13)]
        [Display(Name = "Celular")]
        [Column(Order = 5)]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        public string Celular { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Correo")]
        [Column(Order = 6)]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }

        [Required]
        [MaxLength(30)]
        [Display(Name = "Clave")]
        [Column(Order = 7)]
        public string Clave { get; set; }

        public int IdRol { get; set; }
        [ForeignKey("IdRol")]
        public RolEntity Rol { get; set; }
    }
}
