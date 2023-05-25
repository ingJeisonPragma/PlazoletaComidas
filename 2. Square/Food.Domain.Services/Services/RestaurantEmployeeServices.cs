using Food.Domain.Business.DTO;
using Food.Domain.Interface.Exceptions;
using Food.Domain.Interface.IRepository;
using Food.Domain.Interface.IServices;
using Food.Domain.Interface.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Services.Services
{
    public class RestaurantEmployeeServices : IRestaurantEmployeeServices
    {
        private readonly IRestaurantEmployeeRepository _employeeRepository;

        public RestaurantEmployeeServices(IRestaurantEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }
        public async Task<StandardResponse> CreateRestauranteEmpl(RestaurantEmployeeDTO restaurantEmployee)
        {
            var restaurantEntity = RestaurantEmployeeMapper.MapEntity(restaurantEmployee);
            var result = await _employeeRepository.Add(restaurantEntity);

            if (result != null)
                return new StandardResponse { IsSuccess = true, Message = "Se creo correctamente la relación entre Restaurante y Empleado." };
            else
                throw new DomainValidateException(new StandardResponse { IsSuccess = false, Message = "Error creando la realación Restaurante y Empleado." });
        }

        public async Task<StandardResponse> GetRestaurantEmployee(int IdEmployee)
        {
            //Buscar los restaurantes del Empleado
            var result = await _employeeRepository.GetRestaurantByEmployee(IdEmployee);

            var restaurantDto = RestaurantEmployeeMapper.MapDTO(result);

            if (restaurantDto != null)
                return new StandardResponse { IsSuccess = true, Message = "Restaurante por empleado.", Result = restaurantDto };
            else
                throw new DomainValidateException(new StandardResponse { IsSuccess = false, Message = "No se encontraron restaurantes asociados al empleado." });
        }
    }
}
