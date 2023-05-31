using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json.Serialization;
using User.Domain.Business.DTO;
using User.Domain.Business.DTO.Token;
using User.Domain.Interface.Exceptions;
using User.Domain.Interface.IServices;

namespace User.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class LoginController : ControllerBase
    {
        private readonly IUserServices _ownerServices;
        private readonly ILoginServices _loginServices;

        public LoginController(IUserServices ownerServices, ILoginServices loginServices)
        {
            this._ownerServices = ownerServices;
            this._loginServices = loginServices;
        }

        /// <summary>
        /// Metodo que autentica el usuario y genera un token para permitir 
        /// utilizar los otros EndPoint de las API que requieren autorización.
        /// </summary>
        /// <param name="TokenRequest">Se compone del usuario y la contraseña del usuario.</param>
        /// <returns>TokenResponse</returns>
        [HttpPost]
        [Route("CreateToken")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StandardResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(StandardResponse))]
        public async Task<ActionResult> CreateToken([FromBody] TokenRequest model)
        {
            try
            {
                //Valida usuario y contraseña
                var user = await _ownerServices.GetValidateCredential(model.UserName, model.Password);

                //Crear Token
                var response = new StandardResponse()
                {
                    IsSuccess = true,
                    Message = "Token creado con éxito",
                    Result = _loginServices.CreateToken(user),
                };
                return StatusCode(200, response);
            }
            catch (DomainUserValidateException ex)
            {
                return StatusCode(400, ex.Standard);
            }
            catch (Exception ex)
            {
                var response = new StandardResponse()
                {
                    IsSuccess = false,
                    Message = "Error creando el token " + ex.Message,
                };
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
        }
    }
}
