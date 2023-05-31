using Food.Domain.Business.DTO;
using Food.Domain.Interface.Exceptions;
using Food.Domain.Interface.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Food.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RestaurantEmployeeController : ControllerBase
    {
        private readonly IRestaurantEmployeeServices _employeeServices;

        public RestaurantEmployeeController(IRestaurantEmployeeServices employeeServices)
        {
            this._employeeServices = employeeServices;
        }

        /// <summary>
        /// Se encarga de relacionar un empleado con un restaurante, teniendo en cuenta el Empleado ya tiene estar creado y el restaurante ser del Propietario.
        /// Solo los usuarios Propietarios tienen permiso para hacerlo. 
        /// </summary>
        /// <param name="restaurant">Usa RestaurantEmployeeDTO para la asociación del empleado al restaurante.</param>
        /// <returns></returns>
        /// <response code="200">Devuelve StandardResponse en el IsSuccess true todo fue correcto </response>
        /// <response code="400">Devuelve StandardResponse en el IsSuccess false y el error en el message</response>
        [HttpPost]
        [Route("AddRestaurantEmployee")]
        [Authorize(Roles = "2")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StandardResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(StandardResponse))]
        public async Task<ActionResult> AddRestaurantEmployee([FromBody] RestaurantEmployeeDTO restaurant)
        {
            StandardResponse response = new();
            try
            {
                response = await _employeeServices.CreateRestauranteEmpl(restaurant);
                return StatusCode(201, response);
            }
            catch (DomainValidateException ex)
            {
                return StatusCode(400, ex.Standard);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error inesperado ocurrio en Restaurante y Empleado: " + ex.Message;
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }

        }
    }
}
