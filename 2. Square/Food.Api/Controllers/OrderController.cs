using Food.Domain.Business.DTO;
using Food.Domain.Business.DTO.Order;
using Food.Domain.Interface.Exceptions;
using Food.Domain.Interface.IServices;
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
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;
        private readonly IConfiguration _configuration;

        public OrderController(IOrderServices orderServices,
            IConfiguration configuration)
        {
            this._orderServices = orderServices;
            this._configuration = configuration;
        }

        [HttpGet]
        [Route("GetOrderPending")]
        [Authorize(Roles = "3")]
        public async Task<ActionResult> GetOrderPending(int page = 1, int take = 10)
        {
            StandardResponse response = new();
            try
            {
                var Token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                var infoUser = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(Token);

                int Employee = Convert.ToInt32(infoUser.Claims.First(claim => claim.Type == "IdUser").Value.ToString());

                response = await _orderServices.GetPending(Employee, page, take);
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

        [HttpGet]
        [Route("GetOrderPreparation")]
        [Authorize(Roles = "3")]
        public async Task<ActionResult> GetOrderPreparation(int page = 1, int take = 10)
        {
            StandardResponse response = new();
            try
            {
                var Token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                var infoUser = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(Token);

                int Employee = Convert.ToInt32(infoUser.Claims.First(claim => claim.Type == "IdUser").Value.ToString());

                response = await _orderServices.GetPending(Employee, page, take);
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

        [HttpPost]
        [Route("AddOrder")]
        public async Task<ActionResult> AddOrder([FromBody] OrderDTO orderDTO)
        {
            StandardResponse response = new();
            try
            {
                var Token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                var infoUser = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(Token);

                orderDTO.IdCliente = Convert.ToInt32(infoUser.Claims.First(claim => claim.Type == "IdUser").Value.ToString());

                response = await _orderServices.CreateOrder(orderDTO);
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
        [Route("AssingOrderEmployee")]
        [Authorize(Roles = "3")]
        public async Task<ActionResult> AssingOrderEmployee([FromBody] List<UpdateOrderDTO> updateOrders)
        {
            StandardResponse response = new();
            try
            {
                var Token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                var infoUser = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(Token);

                int IdEmployee = Convert.ToInt32(infoUser.Claims.First(claim => claim.Type == "IdUser").Value.ToString());

                response = await _orderServices.UpdatePreparationOrder(updateOrders, IdEmployee);
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
        [Route("DeliverOrder")]
        [Authorize(Roles = "3")]
        public async Task<ActionResult> DeliverOrder(int Order, string Pin)
        {
            StandardResponse response = new();
            try
            {
                var Token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                var infoUser = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(Token);

                int IdEmployee = Convert.ToInt32(infoUser.Claims.First(claim => claim.Type == "IdUser").Value.ToString());

                response = await _orderServices.UpdateDeliveryOrder(IdEmployee, Order, Pin);
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
        [Route("CancelOrder")]
        [Authorize(Roles = "4")]
        public async Task<ActionResult> CancelOrder(int Order)
        {
            StandardResponse response = new();
            try
            {
                var Token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                var infoUser = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(Token);

                int IdCustomer = Convert.ToInt32(infoUser.Claims.First(claim => claim.Type == "IdUser").Value.ToString());

                response = await _orderServices.UpdateCancelOrder(IdCustomer, Order);
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
        [Route("NotificationSMS")]
        [Authorize(Roles = "3")]
        public async Task<ActionResult> NotificationSMS([FromBody] List<UpdateOrderDTO> updateOrders)
        {
            StandardResponse response = new();
            try
            {
                var Token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                var infoUser = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(Token);
                _configuration["Tokens:AccessToken"] = Token;

                int IdEmployee = Convert.ToInt32(infoUser.Claims.First(claim => claim.Type == "IdUser").Value.ToString());

                response = await _orderServices.UpdateOrderOK(updateOrders, IdEmployee);
                return StatusCode(201, response);
            }
            catch (DomainValidateException ex)
            {
                return StatusCode(400, ex.Standard);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error inesperado al notificar al cliente: " + ex.Message;
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
        }
    }
}
