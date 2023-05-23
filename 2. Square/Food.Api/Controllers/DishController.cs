using Food.Domain.Business.DTO;
using Food.Domain.Interface.Exceptions;
using Food.Domain.Interface.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace Food.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DishController : ControllerBase
    {
        private readonly IDishServices _dishServices;

        public DishController(IDishServices dishServices)
        {
            this._dishServices = dishServices;
        }

        [HttpPost]
        [Route("AddDish")]
        [Authorize(Roles = "2")]
        public async Task<ActionResult> AddRestaurant([FromBody] DishDTO dishDTO)
        {
            StandardResponse response = new();
            try
            {
                response = await _dishServices.CreateDish(dishDTO);
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

        [HttpPut]
        [Route("UpdateDish")]
        [Authorize(Roles = "2")]
        public async Task<ActionResult> UpdateDish([FromBody] DishUpdateDTO dishDTO)
        {
            StandardResponse response = new();
            try
            {
                response = await _dishServices.UpdateDish(dishDTO);
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

        [HttpPut]
        [Route("UpdateDishState")]
        [Authorize(Roles = "2")]
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
