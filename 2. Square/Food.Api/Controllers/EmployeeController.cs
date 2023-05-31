using Food.Domain.Business.DTO;
using Food.Domain.Business.DTO.Order;
using Food.Domain.Business.UserProxyDTO;
using Food.Domain.Interface.Exceptions;
using Food.Domain.Interface.IServices;
using Food.Domain.Interface.IServices.IUserProxy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Data;
using System.IdentityModel.Tokens.Jwt;

namespace Food.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IEmployeeServices _employeeServices;

        public EmployeeController(IConfiguration configuration,
            IEmployeeServices employeeServices)
        {
            this._configuration = configuration;
            this._employeeServices = employeeServices;
        }

        /// <summary>
        /// Se encarga de crear el empleado y su relación con el Restaurante del propietario.
        /// Solo los usuarios Propietarios tienen permiso para hacer uso del EndPoint.
        /// </summary>
        /// <param name="user">Usa el UserDTO en la petición</param>
        /// <returns>Objeto StandardResponse</returns>
        /// <response code="200">Devuelve StandardResponse en el IsSuccess true todo fue correcto </response>
        /// <response code="400">Devuelve StandardResponse en el IsSuccess false y el error en el message</response>
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

                int Propietario = Convert.ToInt32(((JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(Token))
                    .Claims.First(claim => claim.Type == "IdUser").Value.ToString());

                response = await _employeeServices.CreateEmployee(user, Propietario);
                return StatusCode(201, response);
            }
            catch (DomainValidateException ex)
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
