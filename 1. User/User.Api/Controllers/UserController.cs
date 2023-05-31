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
        /// Se encarga de crear los usuarios Propietarios usando el UserDTO.
        /// Solo los usuarios Admin tienen permiso para crearlo.
        /// </summary>
        /// <param name="user">Usa el UserDTO en la petición</param>
        /// <returns>Objeto StandardResponse</returns>
        /// <response code="200">Devuelve StandardResponse en el IsSuccess true todo fue correcto </response>
        /// <response code="400">Devuelve StandardResponse en el IsSuccess false y el error en el message</response>
        [HttpPost]
        [Route("AddOwner")]
        [Authorize(Roles = "1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StandardResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(StandardResponse))]
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

        /// <summary>
        /// Se encarga de crear los usuarios Empleado usando el UserDTO.
        /// Solo los usuarios Propietarios tienen permiso para crearlo.
        /// Es usado desde el Microservicio de Plazoleta para crear empleado y la relación con restaurante.
        /// </summary>
        /// <param name="user">Usa el UserDTO en la petición</param>
        /// <returns>Objeto StandardResponse</returns>
        /// <response code="200">Devuelve StandardResponse en el IsSuccess true todo fue correcto o false se hubo un error en el message
        /// Incluye en el param result el UserResponseDTO con los datos del empleado creado sin la clave.</response>
        /// <response code="400">Devuelve StandardResponse con el error en el message</response>
        [HttpPost]
        [Route("AddEmployee")]
        [Authorize(Roles = "2")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StandardResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(StandardResponse))]
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

        /// <summary>
        /// Se encarga de crear los usuarios Empleado usando el UserDTO.
        /// </summary>
        /// <param name="user">Usa el UserDTO en la petición</param>
        /// <returns>Objeto StandardResponse</returns>
        /// <response code="200">Devuelve StandardResponse en el IsSuccess true todo fue correcto </response>
        /// <response code="400">Devuelve StandardResponse en el IsSuccess false y el error en el message</response>
        [HttpPost]
        [Route("AddCustomer")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StandardResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(StandardResponse))]
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
