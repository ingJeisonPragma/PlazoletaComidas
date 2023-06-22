using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.DataBase;
using User.Domain.Business.DTO;
using User.Domain.Interface.Entities;
using User.Domain.Interface.Exceptions;
using User.Domain.Interface.IRepository;
using User.Domain.Interface.IServices;
using User.Domain.Services.Services;

namespace User.UnitTest
{
    [TestClass]
    public class GetCredentialTest
    {
        protected UserServices userServices;
        protected Mock<IUserRepository> MockUserRepository = new();
        protected Mock<IUserServices> MockUserServices = new();

        public GetCredentialTest()
        {
            this.userServices = new UserServices(this.MockUserRepository.Object);
        }

        [TestMethod]
        public void ValidateLoginSuccess()
        {
            int Id = 1;
            int documento = 1128436325;
            var nombre = "Jeison";
            var apellido = "Cañas";
            var celular = "+573137653881";
            var correo = "jeison@pragma.com.co";
            var clave = "$2a$11$tUNeYBtFJSAE0Dz319GIo.BGeAur3NKQMZ02Q9pRIXp4lRO5kt4Di";
            var idRol = 1;

            UserEntity userOwnerEntity = new()
            {
                Id = Id,
                Documento = documento,
                Nombre = nombre,
                Apellido = apellido,
                Celular = celular,
                Correo = correo,
                Clave = clave,
                IdRol = idRol,
                Rol = null
            };

            var returnsResultModk = this.MockUserRepository.Setup(x => x.GetEmail("jeison@pragma.com.co")).ReturnsAsync(userOwnerEntity);
            UserResponseDTO result = userServices.GetValidateCredential("jeison@pragma.com.co", "Acceso1").Result;

            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void ValidateErrorEmail()
        {
            int Id = 1;
            int documento = 1128436325;
            var nombre = "Jeison";
            var apellido = "Cañas";
            var celular = "+573137653881";
            var correo = "jeison@pragma.com.co";
            var clave = "$2a$11$tUNeYBtFJSAE0Dz319GIo.BGeAur3NKQMZ02Q9pRIXp4lRO5kt4Di";
            var idRol = 1;

            UserEntity userOwnerEntity = new()
            {
                Id = Id,
                Documento = documento,
                Nombre = nombre,
                Apellido = apellido,
                Celular = celular,
                Correo = correo,
                Clave = clave,
                IdRol = idRol,
                Rol = null
            };

            UserResponseDTO userDTO = new()
            {
                Id = Id,
                Documento = documento,
                Nombre = nombre,
                Apellido = apellido,
                Celular = celular,
                Correo = correo,
                IdRol = idRol
            };

            var response = new DomainUserValidateException(new StandardResponse { IsSuccess = false, Message = "Usuario o contraseña incorrectos." });

            var returnsResultModk = this.MockUserRepository.Setup(x => x.GetEmail("jeison@pragma.com.co")).ReturnsAsync(userOwnerEntity);
            var result = userServices.GetValidateCredential("jeison01@pragma.com.co", "ClaveError");

            Assert.ThrowsExceptionAsync<DomainUserValidateException>(async () => await result).Equals(response);

            MockUserRepository.Verify(a => a.GetEmail("jeison01@pragma.com.co"));

        }

        [TestMethod]
        public void ValidateErrorPassword()
        {
            int Id = 1;
            int documento = 1128436325;
            var nombre = "Jeison";
            var apellido = "Cañas";
            var celular = "+573137653881";
            var correo = "jeison@pragma.com.co";
            var clave = "$2a$11$tUNeYBtFJSAE0Dz319GIo.BGeAur3NKQMZ02Q9pRIXp4lRO5kt4Di";
            var idRol = 1;

            UserEntity userOwnerEntity = new()
            {
                Id = Id,
                Documento = documento,
                Nombre = nombre,
                Apellido = apellido,
                Celular = celular,
                Correo = correo,
                Clave = clave,
                IdRol = idRol,
                Rol = null
            };

            UserResponseDTO userDTO = new()
            {
                Id = Id,
                Documento = documento,
                Nombre = nombre,
                Apellido = apellido,
                Celular = celular,
                Correo = correo,
                IdRol = idRol
            };

            var response = new DomainUserValidateException( new StandardResponse { IsSuccess = false, Message = "Usuario o contraseña incorrectos." });

            var returnsResultModk = this.MockUserRepository.Setup(x => x.GetEmail(userDTO.Correo)).ReturnsAsync(userOwnerEntity);
            var result = userServices.GetValidateCredential("jeison@pragma.com.co", "ClaveError");


            Assert.ThrowsExceptionAsync<DomainUserValidateException>(async () => await result).Equals(response);
        }
    }
}
