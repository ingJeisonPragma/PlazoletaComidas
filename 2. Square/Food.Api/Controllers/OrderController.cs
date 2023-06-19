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
        private StandardResponse response;

        public OrderController(IOrderServices orderServices,
            IConfiguration configuration)
        {
            _orderServices = orderServices;
            _configuration = configuration;
            response = new();
        }

        /// <summary>
        /// Se encarga de listas todos los pedidos en estado Pendiente de un restaurante al que pertenece el Empleado.
        /// Solo los usuarios Empleados tienen permiso para hacerlo.
        /// </summary>
        /// <param name="page">Indica el número de la pagina, por defecto es 1</param>
        /// <param name="take">Indica el número de datos por pagina, por defecto es 10</param>
        /// <returns></returns>
        /// <response code="200">Devuelve StandardResponse en el param result del PaginatedListDTO con la lista de Ordenes pendientes</response>
        /// <response code="400">Devuelve StandardResponse con el error en el message</response>
        [HttpGet]
        [Route("GetOrderPending")]
        [Authorize(Roles = "3")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StandardResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(StandardResponse))]
        public async Task<ActionResult> GetOrderPending(int page = 1, int take = 10)
        {
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

        /// <summary>
        /// Se encarga de listas todos los pedidos en estado Preaparación de un restaurante al que pertenece el Empleado.
        /// Solo los usuarios Empleados tienen permiso para hacerlo.
        /// </summary>
        /// <param name="page">Indica el número de la pagina, por defecto es 1</param>
        /// <param name="take">Indica el número de datos por pagina, por defecto es 10</param>
        /// <returns></returns>
        /// <response code="200">Devuelve StandardResponse en el param result del PaginatedListDTO con la lista de Ordenes pendientes</response>
        /// <response code="400">Devuelve StandardResponse con el error en el message</response>
        [HttpGet]
        [Route("GetOrderPreparation")]
        [Authorize(Roles = "3")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StandardResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(StandardResponse))]
        public async Task<ActionResult> GetOrderPreparation(int page = 1, int take = 10)
        {
            try
            {
                var Token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                var infoUser = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(Token);

                int Employee = Convert.ToInt32(infoUser.Claims.First(claim => claim.Type == "IdUser").Value.ToString());

                response = await _orderServices.GetPreparation(Employee, page, take);
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
        /// Se encarga de crear la orden de un Cliente con los platos de un solo restaurante.
        /// Solo los Clientes tienen permiso para hacerlo.
        /// </summary>
        /// <param name="orderDTO">Usa OrderDTO para realizar la petición.</param>
        /// <returns></returns>
        /// <response code="200">Devuelve StandardResponse en el IsSuccess true todo fue correcto </response>
        /// <response code="400">Devuelve StandardResponse en el IsSuccess false y el error en el message</response>
        [HttpPost]
        [Route("AddOrder")]
        [Authorize(Roles = "4")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StandardResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(StandardResponse))]
        public async Task<ActionResult> AddOrder([FromBody] OrderDTO orderDTO)
        {
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

        /// <summary>
        /// Se encarga de asignar de asignarle uno o varios pedidos a un empleado.
        /// Solo los usuarios Empleados tienen permiso para hacerlo.
        /// </summary>
        /// <param name="updateOrders">Usa una lista de UpdateOrderDTO indicando el número del pedido.</param>
        /// <returns></returns>
        /// <response code="200">Devuelve StandardResponse en el IsSuccess true todo fue correcto </response>
        /// <response code="400">Devuelve StandardResponse en el IsSuccess false y el error en el message</response>
        [HttpPut]
        [Route("AssingOrderEmployee")]
        [Authorize(Roles = "3")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StandardResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(StandardResponse))]
        public async Task<ActionResult> AssingOrderEmployee([FromBody] List<UpdateOrderDTO> updateOrders)
        {
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

        /// <summary>
        /// 
        /// Solo los usuarios Empleados tienen permiso para hacerlo.
        /// </summary>
        /// <param name="updateOrders"></param>
        /// <returns></returns>
        /// <response code="200">Devuelve StandardResponse en el IsSuccess true todo fue correcto </response>
        /// <response code="400">Devuelve StandardResponse en el IsSuccess false y el error en el message</response>
        [HttpPut]
        [Route("NotificationSMS")]
        [Authorize(Roles = "3")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StandardResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(StandardResponse))]
        public async Task<ActionResult> NotificationSMS([FromBody] List<UpdateOrderDTO> updateOrders)
        {
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

        /// <summary>
        /// Se encarga de validar el Pin enviado al Cliente por SMS y realizar la entrega del pedido.
        /// Solo los usuarios Empleados tienen permiso para hacerlo.
        /// </summary>
        /// <param name="Order">Indica el número del pedido del cliente.</param>
        /// <param name="Pin">Indica el Pin recibido por el cliente por SMS.</param>
        /// <returns></returns>
        /// <response code="200">Devuelve StandardResponse en el IsSuccess true todo fue correcto </response>
        /// <response code="400">Devuelve StandardResponse en el IsSuccess false y el error en el message</response>
        [HttpPut]
        [Route("DeliverOrder")]
        [Authorize(Roles = "3")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StandardResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(StandardResponse))]
        public async Task<ActionResult> DeliverOrder(int Order, string Pin)
        {
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

        /// <summary>
        /// Este proceso se encarga de Cancelar el pedido del cliente siempre que este aún se encuentre en estado Pendiente.
        /// Solo los Clientes tienen permiso para hacerlo.
        /// </summary>
        /// <param name="Order">Indica el número del pedido del cliente que desea cancelar.</param>
        /// <returns></returns>
        /// <response code="200">Devuelve StandardResponse en el IsSuccess true todo fue correcto </response>
        /// <response code="400">Devuelve StandardResponse en el IsSuccess false y el error en el message</response>
        [HttpPut]
        [Route("CancelOrder")]
        [Authorize(Roles = "4")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StandardResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(StandardResponse))]
        public async Task<ActionResult> CancelOrder(int Order)
        {
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
    }
}
