using Food.Domain.Business.DTO;
using Food.Domain.Interface.Exceptions;
using Food.Domain.Interface.IServices;
using Food.Domain.Services.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Food.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DishController : ControllerBase
    {
        private readonly IDishServices _dishServices;
        private readonly IConfiguration _configuration;

        public DishController(IDishServices dishServices,
            IConfiguration configuration)
        {
            this._dishServices = dishServices;
            this._configuration = configuration;
        }

        /// <summary>
        /// Se encarga de buscar los platos agrupados por categorias de acuerdo al Id del restaurante
        /// </summary>
        /// <param name="IdRestaurant">Id del restaurante al que pertencen los platos.</param>
        /// <returns></returns>
        /// <response code="200">Devuelve StandardResponse en el IsSuccess true todo fue correcto </response>
        /// <response code="400">Devuelve StandardResponse en el IsSuccess false y el error en el message</response>
        [HttpGet]
        [Route("GetDishByCategory")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StandardResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(StandardResponse))]
        public async Task<ActionResult<StandardResponse>> GetDishByCategory(int IdRestaurant)
        {
            StandardResponse response = new();
            try
            {
                response = await _dishServices.GetDishByCategory(IdRestaurant);
                return StatusCode(201, response);
            }
            catch (DomainValidateException ex)
            {
                return StatusCode(400, ex.Standard);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error inesperado al consultar los Productos: " + ex.Message; ;
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
        }

        /// <summary>
        /// Se encarga de crear los platos que van a estar relacionados a un Restaurante.
        /// Solo los usuarios Propietarios tienen permiso para crearlo.
        /// </summary>
        /// <param name="dishDTO">Usa el DishDTO para la creación del Plato</param>
        /// <returns></returns>
        /// <response code="200">Devuelve StandardResponse en el IsSuccess true todo fue correcto </response>
        /// <response code="400">Devuelve StandardResponse en el IsSuccess false y el error en el message</response>
        [HttpPost]
        [Route("AddDish")]
        [Authorize(Roles = "2")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StandardResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(StandardResponse))]
        public async Task<ActionResult> AddDish([FromBody] DishDTO dishDTO)
        {
            StandardResponse response = new();
            try
            {
                var Token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                var infoUser = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(Token);
                _configuration["Tokens:AccessToken"] = Token;

                int Propietario = Convert.ToInt32(infoUser.Claims.First(claim => claim.Type == "IdUser").Value.ToString());

                response = await _dishServices.CreateDish(dishDTO, Propietario);
                return StatusCode(201, response);
            }
            catch (DomainValidateException ex)
            {
                return StatusCode(400, ex.Standard);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error inesperado al crear Plato: " + ex.Message;
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
        }

        /// <summary>
        /// Se encarga de actualizar la descripción y el precio del plato por cada restaurante.
        /// Solo los usuarios Propietarios tienen permiso para hacerlo.
        /// </summary>
        /// <param name="dishUpdateDTO">Usa el DishUpdateDTO para la actualización de descripción y precio.</param>
        /// <returns></returns>
        /// <response code="200">Devuelve StandardResponse en el IsSuccess true todo fue correcto </response>
        /// <response code="400">Devuelve StandardResponse en el IsSuccess false y el error en el message</response>
        [HttpPut]
        [Route("UpdateDish")]
        [Authorize(Roles = "2")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StandardResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(StandardResponse))]
        public async Task<ActionResult> UpdateDish([FromBody] DishUpdateDTO dishUpdateDTO)
        {
            StandardResponse response = new();
            try
            {
                response = await _dishServices.UpdateDish(dishUpdateDTO);
                return StatusCode(201, response);
            }
            catch (DomainValidateException ex)
            {
                return StatusCode(400, ex.Standard);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error inesperado al actualizar el Plato: " + ex.Message;
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
        }

        /// <summary>
        /// Se encarga de Activar o Inactivar los platos en el restaurante.
        /// </summary>
        /// <param name="dishDTO">Usa el DishUpdateStateDTO para la actualización del plato, Id y Estado</param>
        /// <returns></returns>
        /// <response code="200">Devuelve StandardResponse en el IsSuccess true todo fue correcto </response>
        /// <response code="400">Devuelve StandardResponse en el IsSuccess false y el error en el message</response>
        [HttpPut]
        [Route("UpdateDishState")]
        [Authorize(Roles = "2")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StandardResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(StandardResponse))]
        public async Task<ActionResult> UpdateDishState([FromBody] DishUpdateStateDTO dishDTO)
        {
            StandardResponse response = new();
            try
            {
                var Token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                var infoUser = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(Token);
                int IdPropietario = Convert.ToInt32(infoUser.Claims.First(claim => claim.Type == "IdUser").Value);

                response = await _dishServices.UpdateDishState(dishDTO, IdPropietario);
                return StatusCode(200, response);
            }
            catch (DomainValidateException ex)
            {
                return StatusCode(400, ex.Standard);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error inesperado al actualizar el Plato: " + ex.Message;
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
        }
    }
}
