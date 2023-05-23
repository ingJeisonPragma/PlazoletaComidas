using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Domain.Business.DTO.Token
{
    public class TokenRequest
    {
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El {0} es obligatorio.")]
        public string Password { get; set; }
    }
}
