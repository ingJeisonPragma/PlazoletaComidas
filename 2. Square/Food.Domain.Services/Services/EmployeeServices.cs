using Food.Domain.Business.DTO;
using Food.Domain.Business.UserProxyDTO;
using Food.Domain.Interface.Exceptions;
using Food.Domain.Interface.IServices;
using Food.Domain.Interface.IServices.IUserProxy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Services.Services
{
    public  class EmployeeServices : IEmployeeServices
    {
        private readonly IUserProxyServices _userProxy;
        private readonly IRestaurantServices _restaurantServices;
        private readonly IRestaurantEmployeeServices _restaurantEmployeeServices;

        public EmployeeServices(IUserProxyServices userProxy, 
            IRestaurantServices restaurantServices,
            IRestaurantEmployeeServices restaurantEmployeeServices)
        {
            this._userProxy = userProxy;
            this._restaurantServices = restaurantServices;
            this._restaurantEmployeeServices = restaurantEmployeeServices;
        }

        public async Task<StandardResponse> CreateEmployee(UserDTO user, int IdPropietario)
        {
            //Validar restaurante y propietario
            var dato = await _restaurantServices.GetValidateRestaurantOwner(user.IdRestaurante, IdPropietario);

            //Crear Empleado
            var responseUser = await _userProxy.PostAsync("/api/User/AddEmployee", user);

            if (!responseUser.IsSuccess)
                throw new DomainValidateException(new StandardResponse
                {
                    IsSuccess = false,
                    Message = JsonConvert.DeserializeObject<StandardResponse>(responseUser.Result.ToString()).Message
                });

            UserDTO userDTO = JsonConvert.DeserializeObject<UserDTO>(responseUser.Result.ToString());

            //Crear relación empleado y restaurante
            RestaurantEmployeeDTO employeeDTO = new()
            {
                IdPersona = userDTO.Id,
                IdRestaurante = user.IdRestaurante
            };

            var emp = await _restaurantEmployeeServices.CreateRestauranteEmpl(
                new RestaurantEmployeeDTO()
                {
                    IdPersona = userDTO.Id,
                    IdRestaurante = user.IdRestaurante
                });



            //var getEmployee = await _userRepository.GetOwnerDocument(user.Documento);

            //if (getEmployee != null)
            //{
            //    if (user.IdRol == user.IdRol)
            //        throw new DomainUserValidateException(new StandardResponse { IsSuccess = false, Message = "Ya existe un Empleado con este documento." });
            //    if (getEmployee.Correo.ToLower() == user.Correo.ToLower())
            //        throw new DomainUserValidateException(new StandardResponse { IsSuccess = false, Message = "Ya existe un Usuario con este correo." });
            //}

            ////Buscar restaurante y Empleado    
            //var resultValidateRestaurant = await _foodServices.ValidateRestaurantOwner(user.IdRestaurante, user.IdPropietario);

            //if (!resultValidateRestaurant.IsSuccess)
            //    throw new DomainUserValidateException(new StandardResponse()
            //    {
            //        IsSuccess = false,
            //        Message = JsonConvert.DeserializeObject<StandardResponse>(resultValidateRestaurant.Result.ToString()).Message
            //    });

            ////Mappear y guardar el Empleado
            //var userEntity = UserEmployeeMapper.MapEntity(user);
            //userEntity.IdRol = 3;
            //userEntity.Clave = Encript(userEntity.Clave);
            //var result = await _userRepository.Add(userEntity);

            ////Agregar restaurante y Empleado    
            //var resultAddRestaurant = await _foodServices.CreateRestaurantOwner(user.IdRestaurante, result.Id);

            //if (!resultAddRestaurant.IsSuccess)
            //{
            //    await _userRepository.Delete(result);
            //    throw new DomainUserValidateException(new StandardResponse
            //    {
            //        IsSuccess = false,
            //        Message = "Error creando el Empleado: " +
            //        JsonConvert.DeserializeObject<StandardResponse>(resultAddRestaurant.Result.ToString()).Message
            //    });
            //}
            //else
            return new StandardResponse { IsSuccess = true, Message = "Se creo correctamente el Empleado." };
        }
    }
}
