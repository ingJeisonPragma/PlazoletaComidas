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
    public class UserOwnerDTO : UserDTO
    {
        //public int Id { get; set; }

        //[Required]
        //[Range(1, int.MaxValue, ErrorMessage = "El Documento debe ser un número mayor a 0")]
        //[Display(Name = "Documento")]
        //public int Documento { get; set; }

        //[Required(ErrorMessage = "El Nombre es un campo obligatorio.")]
        //[MaxLength(50, ErrorMessage = "El Nombre excede los 50 caracteres permitidos.")]
        //[Display(Name = "Nombre")]
        //public string Nombre { get; set; }

        //[Required(ErrorMessage = "El Apellido es un campo obligatorio.")]
        //[MaxLength(50, ErrorMessage = "El Apellido excede los 50 caracteres permitidos")]
        //[Display(Name = "Apellido")]
        //public string Apellido { get; set; }

        //[Required(ErrorMessage = "El Celular es un campo obligatorio.")]
        //[MinLength(10, ErrorMessage = "La cantidad minima de caracteres del celular es 10.")]
        //[MaxLength(13, ErrorMessage = "El Celular excede los 13 caracteres permitidos.")]
        //[Display(Name = "Celular")]
        //[RegularExpression("^[+-]?\\d+(\\.\\d+)?$", ErrorMessage = "Ingresar un número de Celular válido.")]
        //public string Celular { get; set; }

        //[Required(ErrorMessage = "El Correo es un campo obligatorio.")]
        //[MaxLength(100, ErrorMessage = "El Correo excede los 100 caracteres permitidos.")]
        //[Display(Name = "Correo")]
        //[DataType(DataType.EmailAddress)]
        //[RegularExpression("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*", ErrorMessage = "Ingresar un correo válido.")]
        //public string Correo { get; set; }

        //[Required(ErrorMessage = "La Clave es un campo obligatorio.")]
        //[MaxLength(30)]
        //[Display(Name = "Clave")]
        //public string Clave { get; set; }

        //public int IdRol { get; set; }
        public string? NombreRol { get; set; }
    }
}
