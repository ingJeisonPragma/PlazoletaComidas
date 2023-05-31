using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using User.Domain.Business.DTO;
using User.Domain.Interface.Exceptions;
using User.Domain.Interface.IServices;

namespace User.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _ownerServices;
        private readonly IConfiguration _configuration;

        public UserController(IUserServices ownerServices,
            IConfiguration configuration)
        {
            this._ownerServices = ownerServices;
            this._configuration = configuration;
        }

        /// <summary>
        /// Obtiene la información del los usuarios por su Id.
        /// </summary>
        ///// <remarks>Este es usado para obtener los datos del usuario sin la clave.</remarks>
        /// <param name="Id">Id del usuario</param>
        /// <returns>Objeto StandardResponse</returns>
        /// <response code="200">Devuelve UserResponseDTO en el result del StandardResponse</response>
        /// <response code="400">Devuelve StandardResponse con el error en el message</response>
        [HttpGet]
        [Route("GetUser")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StandardResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(StandardResponse))]
        //[Authorize(Roles = "1")]
        public async Task<ActionResult<StandardResponse>> GetUser(int Id)
        {
            StandardResponse response = new();
            try
            {
                response = await _ownerServices.GetUser(Id);
                return StatusCode(201, response);
            }
            catch (DomainUserValidateException ex)
            {
                return StatusCode(400, ex.Standard);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error inesperado al consultar Propietario: " + ex.Message; ;
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
        }

        /// <summary>
        /// Se encarga de crear los usuarios Propietarios usando el UserDTO
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Objeto StandardResponse</returns>
        /// <response code="200">Devuelve UserResponseDTO en el result del StandardResponse</response>
        /// <response code="400">Devuelve StandardResponse con el error en el message</response>
        [HttpPost]
        [Route("AddOwner")]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<StandardResponse>> AddOwner([FromBody] UserDTO user)
        {
            StandardResponse response = new();
            try
            {
                response = await _ownerServices.CreateOwner(user);
                return StatusCode(201, response);
            }
            catch (DomainUserValidateException ex)
            {
                return StatusCode(400, ex.Standard);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error inesperado al crear Propietario: " + ex.Message;
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
        }

        [HttpPost]
        [Route("AddEmployee")]
        [Authorize(Roles = "2")]
        public async Task<ActionResult<StandardResponse>> AddEmployee([FromBody] UserDTO user)
        {
            StandardResponse response = new();
            try
            {
                var Token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                _configuration["Tokens:AccessToken"] = Token;

                response = await _ownerServices.CreateEmployee(user);
                return StatusCode(201, response);
            }
            catch (DomainUserValidateException ex)
            {
                return StatusCode(400, ex.Standard);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error inesperado al crear Propietario: " + ex.Message;
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
        }

        [HttpPost]
        [Route("AddCustomer")]
        [AllowAnonymous]
        public async Task<ActionResult<StandardResponse>> AddCustomer([FromBody] UserDTO user)
        {
            StandardResponse response = new();
            try
            {
                response = await _ownerServices.CreateCustomer(user);
                return StatusCode(201, response);
            }
            catch (DomainUserValidateException ex)
            {
                return StatusCode(400, ex.Standard);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error inesperado al crear Propietario: " + ex.Message;
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
        }
    }
}
