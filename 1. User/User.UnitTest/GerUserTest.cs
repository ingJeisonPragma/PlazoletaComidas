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
    public class GerUserTest
    {
        protected UserServices userServices;
        protected Mock<IUserRepository> MockUserRepository = new();
        protected Mock<IUserServices> MockUserServices = new();

        public GerUserTest()
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
            var result = userServices.GetUser(3).Result;
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
            this.MockUserRepository.Setup(x => x.GetById(It.IsAny<int>())).Throws(new DomainUserValidateException(new StandardResponse()));

            MockUserServices.Setup(u => u.GetUser(100)).Throws(new DomainUserValidateException(new StandardResponse()));

            var result = userServices.GetUser(100).Result;

            //Assert.IsNull(result);
            //var Owner = (UserResponseDTO)result.Result;
            MockUserRepository.Verify(m => m.GetById(100), Times.Once);
        }
    }
}
