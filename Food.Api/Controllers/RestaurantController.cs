using Food.Domain.Business.DTO;
using Food.Domain.Interface.Exceptions;
using Food.Domain.Interface.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Food.Api.Controllers
{
    /// <summary>
    /// Controlador encargado de la Administración del Restaurante.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantServices _restaurantServices;
        private readonly IConfiguration _configuration;

        public RestaurantController(IRestaurantServices restaurantServices,
            IConfiguration configuration)
        {
            this._restaurantServices = restaurantServices;
            this._configuration = configuration;
        }

        /// <summary>
        /// Se encarga de listar todos los restaurantes con el número de paginas y la cantidad de registros.
        /// </summary>
        /// <param name="page">Indica el número de la pagina, por defecto es 1</param>
        /// <param name="take">Indica el número de datos por pagina, por defecto es 10</param>
        /// <returns></returns>
        /// <response code="200">Devuelve StandardResponse</response>
        /// <response code="400">Devuelve StandardResponse con el error en el message</response>
        [HttpGet]
        [Route("GetAll")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StandardResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(StandardResponse))]
        public async Task<ActionResult<StandardResponse>> GetAll(int page = 1, int take = 10)
        {
            StandardResponse response = new();
            try
            {
                response = await _restaurantServices.GetListRestaurant(page, take);
                return StatusCode(201, response);
            }
            catch (DomainValidateException ex)
            {
                return StatusCode(400, ex.Standard);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error inesperado al consultar los Restaurantes: " + ex.Message;
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
        }

        //[HttpGet]
        //[Route("GetRestaurantOwner")]
        //[Authorize(Roles = "2")]
        //public async Task<ActionResult> GetRestaurantOwner(int IdRestaurante, int IdPropietario)
        //{
        //    StandardResponse response = new();
        //    try
        //    {
        //        response = await _restaurantServices.GetValidateRestaurantOwner(IdRestaurante, IdPropietario);
        //        return StatusCode(200, response);
        //    }
        //    catch (DomainValidateException ex)
        //    {
        //        return StatusCode(400, ex.Standard);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.IsSuccess = false;
        //        response.Message = "Error inesperado al validar la realación de restaurante y Propietario: " + ex.Message;
        //        return StatusCode(StatusCodes.Status400BadRequest, response);
        //    }

        //}

        /// <summary>
        /// Es usado para crear los restaurantes de un Propietario.
        /// Solo el usuario Admin puede realizar este proceso.
        /// </summary>
        /// <param name="restaurant">Usa el RestaurantDTO para realizar la petición.</param>
        /// <returns></returns>
        /// <response code="200">Devuelve StandardResponse en el IsSuccess true todo fue correcto </response>
        /// <response code="400">Devuelve StandardResponse en el IsSuccess false y el error en el message</response>
        [HttpPost]
        [Route("AddRestaurant")]
        [Authorize(Roles = "1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StandardResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(StandardResponse))]
        public async Task<ActionResult> AddRestaurant([FromBody] RestaurantDTO restaurant)
        {
            StandardResponse response = new();
            try
            {
                var Token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                _configuration["Tokens:AccessToken"] = Token;

                response = await _restaurantServices.CreateRestaurant(restaurant);
                return StatusCode(201, response);
            }
            catch (DomainValidateException ex)
            {
                return StatusCode(400, ex.Standard);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error inesperado al crear Restaurante: " + ex.Message;
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
        }
    }
}
