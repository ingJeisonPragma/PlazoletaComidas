using Food.Domain.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Interface.IServices
{
    public interface IHttpPetitionServices
    {
        /// <summary>
        /// Metodo encargado de realizar las peticiones a las API
        /// </summary>
        /// <param name="standard">Modelo estandar con las variables para realizar peticiones</param>
        /// <returns>Devuelve en modelo standardResponse con la respuesta del API en el valueBody</returns>
        Task<StandardResponse> PetitionStandard(StandardRequest standard);
    }
}
