using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.Domain.Business.DTO
{
    public class TwilioRequestDTO
    {
        [Required(ErrorMessage = "El número del receptor es requerido.")]
        public string ToNumber { get; set; }

        [Required(ErrorMessage = "El mensaje de envió es requerido.")]
        public string msg { get; set; }
    }
}
