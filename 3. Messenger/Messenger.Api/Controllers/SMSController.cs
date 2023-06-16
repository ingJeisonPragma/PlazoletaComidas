using Messenger.Domain.Business.DTO;
using Messenger.Domain.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Messenger.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SMSController : ControllerBase
    {
        private readonly IMessengerServices _messengerServices;

        public SMSController(IMessengerServices messengerServices)
        {
            this._messengerServices = messengerServices;
        }

        /// <summary>
        /// Metodo de validación del Elastic.
        /// </summary>
        /// <returns></returns>
        /// /// <response code="200">Devuelve siempre este estado</response>
        [HttpGet]
        [Route("HealthCheck")]
        [AllowAnonymous]
        public async Task<ActionResult> HealthCheck()
        {
            return Ok();
        }

        /// <summary>
        /// Se encarga del envió de mensajes de texto
        /// </summary>
        /// <param name="twilioRequest">Usa el modelo TwilioRequestDTO que recibe el número del recpetor y el mensaje a enviar.</param>
        /// <returns></returns>
        /// <response code="200">Devuelve StandardResponse en el param result del PaginatedListDTO con la lista de restaurantes</response>
        /// <response code="400">Devuelve StandardResponse con el error en el message</response>
        [HttpPost]
        [Route("SendSMS")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StandardResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(StandardResponse))]
        public async Task<ActionResult> SendSMS(TwilioRequestDTO twilioRequest)
        {
            StandardResponse response = new();
            try
            {
                response = await _messengerServices.SendSMS(twilioRequest);
                if (response.IsSuccess)
                    return StatusCode(200, response);
                else 
                    return StatusCode(400, response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error inesperado al enviar el SMS al Cliente: " + ex.Message; ;
                return BadRequest(response);
            }
        }
    }
}
