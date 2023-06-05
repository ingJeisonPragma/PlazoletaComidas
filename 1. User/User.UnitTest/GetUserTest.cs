using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Business.DTO;
using User.Domain.Interface.Entities;
using User.Domain.Interface.Exceptions;
using User.Domain.Interface.IRepository;
using User.Domain.Interface.IServices;
using User.Domain.Services.Services;

namespace User.UnitTest
{
    [TestClass]
    public class GetUserTest
    {
        protected UserServices userServices;
        protected Mock<IUserRepository> MockUserRepository = new();
        protected Mock<IUserServices> MockUserServices = new();

        public GetUserTest()
        {
            this.userServices = new UserServices(this.MockUserRepository.Object);
        }

        [TestMethod]
        public void GetUserSuccess()
        {
            int Id = 1;
            int documento = 11456;
            var nombre = "Jeison";
            var apellido = "Cañas";
            var celular = "3137653881";
            var correo = "jeison@pragma.com.co";
            var clave = "Acceso1";
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
                Rol = new RolEntity()
            };

            var dato = this.MockUserRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(userOwnerEntity);
            var result = userServices.GetUser(1).Result;
            UserResponseDTO userResponse = new()
            {
                Id = Id,
                Documento = documento,
                Nombre = nombre,
                Apellido = apellido,
                Celular = celular,
                Correo = correo,
                IdRol = idRol
            };

            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.IsSuccess);
            var Owner = (UserResponseDTO)result.Result;
            Assert.AreEqual(userResponse.Id, Owner.Id);
            Assert.AreEqual(userResponse.IdRol, Owner.IdRol);
        }

        [TestMethod]
        public void GetUserException()
        {
            int Id = 1;
            int documento = 11456;
            var nombre = "Jeison";
            var apellido = "Cañas";
            var celular = "3137653881";
            var correo = "jeison@pragma.com.co";
            var clave = "Acceso1";
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
                Rol = new RolEntity()
            };

            this.MockUserRepository.Setup(x => x.GetById(1)).ReturnsAsync(userOwnerEntity);

            var result = userServices.GetUser(100);
            var response = new DomainUserValidateException(new StandardResponse { IsSuccess = false, Message = "No existe el Usuario." });
            Assert.ThrowsExceptionAsync<DomainUserValidateException>(async () => await result).Equals(response);
        }
    }
}
