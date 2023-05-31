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

        [HttpGet]
        [Route("GetAll")]
        //[Authorize(Roles = "1")]
        [AllowAnonymous] 
        public async Task<ActionResult<StandardResponse>> GetAll(int page, int take)
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
                response.Message = "Error inesperado al consultar los Restaurantes: " + ex.Message; ;
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

        [HttpPost]
        [Route("AddRestaurant")]
        [Authorize(Roles = "1")]
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
