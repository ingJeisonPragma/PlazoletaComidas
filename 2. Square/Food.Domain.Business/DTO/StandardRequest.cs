﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Business.DTO
{
    public class StandardRequest
    {
        /// <summary>
        /// (Requerido) Código 1 (GET), 2 (POST), 3 (PUT) ó 4 (DELETE).
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int RequestType { get; set; }

        /// <summary>
        /// (Requerido) Url para realizar la petición.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string URL { get; set; } = string.Empty;

        /// <summary>
        /// (Requerido) Extension Api con el metodo a ejecutar.
        /// </summary>
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string MethodName { get; set; } = string.Empty;

        /// <summary>
        /// Lista de parametros para el RequestType GET
        /// </summary>
        public List<dynamic> HeaderParameters { get; set; } = new();

        /// <summary>
        /// Lista de valores de parametros para el RequestType GET.
        /// </summary>
        public List<dynamic> ValuesParameters { get; set; } = new();

        /// <summary>
        /// Tipo de data ("Json", "x-www-form-urlencoded", "none")
        /// </summary>
        public string TypeBody { get; set; } = string.Empty;

        /// <summary>
        /// Cuerpo de la petición.
        /// </summary>
        public object ValueBody { get; set; } = new();

        /// <summary>
        /// El Controller requiere Authorize Scheme ("Bearer", "Basic")
        /// </summary>
        public string TypeAuthorize { get; set; } = string.Empty;

        /// <summary>
        /// Enviar True si el Controller requiere Authorize
        /// </summary>
        public bool IsAuthorize { get; set; }

        /// <summary>
        /// Código de acceso cuando se requiere un token de acceso.
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Código de acceso cuando se requiere un token de acceso.
        /// </summary>
        public bool IsPragma { get; set; }

        /// <summary>
        /// Indica si el API requiere parametros de Encabezado
        /// </summary>
        public bool IsHeader { get; set; }

        /// <summary>
        /// Lista de Keys para el Header del Request
        /// </summary>
        public List<dynamic> RequestHeader { get; set; } = new();

        /// <summary>
        /// Lista de valores para el Header del Request
        /// </summary>
        public List<dynamic> RequestValues { get; set; } = new();
    }
}
