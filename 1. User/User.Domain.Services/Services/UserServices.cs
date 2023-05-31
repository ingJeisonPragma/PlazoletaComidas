using BCrypt.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Business.DTO;
using User.Domain.Interface.Exceptions;
using User.Domain.Interface.IRepository;
using User.Domain.Interface.IServices;
using User.Domain.Interface.Mapper;

namespace User.Domain.Services.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<StandardResponse> CreateOwner(UserDTO user)
        {
            var getOwner = await _userRepository.GetDocument(user.Documento);

            if (getOwner != null)
            {
                if (getOwner.IdRol == user.IdRol)
                    throw new DomainUserValidateException(new StandardResponse { IsSuccess = false, Message = "Ya existe un Propietario con este documento." });
            }

            //Validar si hay más usuarios con ese correo
            var getUser = await _userRepository.GetEmail(user.Correo);

            if (getUser != null)
                throw new DomainUserValidateException(new StandardResponse { IsSuccess = false, Message = "Ya existe un Usuario con este correo." });

            user.IdRol = 2;
            var userEntity = UserEmployeeMapper.MapEntity(user);
            userEntity.Clave = Encript(userEntity.Clave);
            var result = await _userRepository.Add(userEntity);

            if (result != null)
                return new StandardResponse { IsSuccess = true, Message = "Se creo correctamente el Propietario." };
            else
                return new StandardResponse { IsSuccess = false, Message = "Error creando el Propietario." };
        }

        public async Task<StandardResponse> CreateEmployee(UserDTO user)
        {
            var getEmployee = await _userRepository.GetDocument(user.Documento);

            if (getEmployee != null)
            {
                if (getEmployee.IdRol == user.IdRol)
                    throw new DomainUserValidateException(new StandardResponse { IsSuccess = false, Message = "Ya existe un Empleado con este documento." });
            }

            //Validar si hay más usuarios con ese correo
            var getUser = await _userRepository.GetEmail(user.Correo);

            if (getUser != null)
                throw new DomainUserValidateException(new StandardResponse { IsSuccess = false, Message = "Ya existe un Usuario con este correo." });

            //Buscar restaurante y Empleado    
            //var resultValidateRestaurant = await _foodServices.ValidateRestaurantOwner(user.IdRestaurante, user.IdPropietario);

            //if (!resultValidateRestaurant.IsSuccess)
            //    throw new DomainUserValidateException(new StandardResponse()
            //    {
            //        IsSuccess = false,
            //        Message = JsonConvert.DeserializeObject<StandardResponse>(resultValidateRestaurant.Result.ToString()).Message
            //    });

            //Mappear y guardar el Empleado
            var userEntity = UserEmployeeMapper.MapEntity(user);
            userEntity.IdRol = 3;
            userEntity.Clave = Encript(userEntity.Clave);
            var result = await _userRepository.Add(userEntity);

            var userMap = UserEmployeeMapper.MapDTO(result);

            //Agregar restaurante y Empleado    
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
            return new StandardResponse { IsSuccess = true, Message = "Se creo correctamente el Empleado.", Result = userMap };
        }

        public async Task<StandardResponse> CreateCustomer(UserDTO user)
        {
            var getEmployee = await _userRepository.GetDocument(user.Documento);

            if (getEmployee != null)
            {
                if (getEmployee.IdRol == user.IdRol)
                    throw new DomainUserValidateException(new StandardResponse { IsSuccess = false, Message = "Ya existe un Cliente con este documento." });
            }

            //Validar si hay más usuarios con ese correo
            var getUser = await _userRepository.GetEmail(user.Correo);

            if (getUser != null)
                throw new DomainUserValidateException(new StandardResponse { IsSuccess = false, Message = "Ya existe un Usuario con este correo." });

            user.IdRol = 4;
            var userEntity = UserEmployeeMapper.MapEntity(user);
            userEntity.Clave = Encript(userEntity.Clave);
            var result = await _userRepository.Add(userEntity);

            if (result != null)
                return new StandardResponse { IsSuccess = true, Message = "Se creo correctamente el Cliente." };
            else
                throw new DomainUserValidateException(new StandardResponse { IsSuccess = false, Message = "Error creando el Cliente." });
        }

        public async Task<StandardResponse> GetUser(int Id)
        {
            var getUser = await _userRepository.GetById(Id);

            if (getUser == null)
                throw new DomainUserValidateException(new StandardResponse { IsSuccess = false, Message = "No existe el Usuario." });

            var userDto = UserEmployeeMapper.MapDTO(getUser);

            return new StandardResponse { IsSuccess = true, Message = "El Usuario existe.", Result = userDto };
        }

        public async Task<UserResponseDTO?> GetValidateCredential(string user, string Pass)
        {
            var getUser = await _userRepository.GetEmail(user);

            if (getUser != null)
            {
                if (!DesEncript(Pass, getUser.Clave))
                    throw new DomainUserValidateException(new StandardResponse { IsSuccess = false, Message = "Usuario o contraseña incorrectos." });
            }
            else
                throw new DomainUserValidateException(new StandardResponse { IsSuccess = false, Message = "Usuario o contraseña incorrectos." });

            var userDto = UserEmployeeMapper.MapDTO(getUser);

            return userDto;
        }

        private static string Encript(string Pass)
        {
            string PassWord = BCrypt.Net.BCrypt.EnhancedHashPassword(Pass, HashType.SHA512);
            return PassWord;
        }

        private static bool DesEncript(string PasswordLogin, string PasswordBD)
        {
            bool Validate = BCrypt.Net.BCrypt.EnhancedVerify(PasswordLogin, PasswordBD, HashType.SHA512);
            return Validate;
        }
    }
}