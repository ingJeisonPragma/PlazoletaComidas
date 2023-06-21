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
    public class CreateOwnerTest
    {
        protected UserServices userServices;
        protected Mock<IUserRepository> MockUserRepository = new();
        protected Mock<IUserServices> MockUserServices = new();

        public CreateOwnerTest()
        {
            this.userServices = new UserServices(this.MockUserRepository.Object);
        }

        [TestMethod]
        public void CreateUserSuccess()
        {
            int Id = 16;
            int documento = 1149;
            var nombre = "Felipe";
            var apellido = "Murillo";
            var celular = "+573137653881";
            var correo = "felipe@pragma.com.co";
            var clave = "Acceso10";
            var idRol = 2;

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

            UserDTO userDTO = new()
            {
                Id = Id,
                Documento = documento,
                Nombre = nombre,
                Apellido = apellido,
                Celular = celular,
                Correo = correo,
                Clave = clave,
                IdRol = idRol
            };

            var response = new StandardResponse { IsSuccess = true, Message = "Se creo correctamente el Propietario." };

            UserEntity userentity = null;
            this.MockUserRepository.Setup(x => x.GetDocument(1149)).ReturnsAsync(userentity);
            this.MockUserRepository.Setup(x => x.GetEmail("felipe@pragma.com.co")).ReturnsAsync(userentity);

            var returnsResultModk = this.MockUserRepository.Setup(x => x.Add(userOwnerEntity)).ReturnsAsync(userOwnerEntity);

            var result = userServices.CreateOwner(userDTO).Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.IsSuccess);
            Assert.AreEqual(response, result);
        }

        [TestMethod]
        public void CreateOwnerErrorDocument()
        {
            int Id = 2;
            int documento = 1128467821;
            var nombre = "Carlos";
            var apellido = "Perez";
            var celular = "+573137653881";
            var correo = "carlos@pragma.com.co";
            var clave = "Acceso2";
            var idRol = 2;

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

            UserDTO userDTO = new()
            {
                Id = Id,
                Documento = documento,
                Nombre = nombre,
                Apellido = apellido,
                Celular = celular,
                Correo = correo,
                Clave = clave,
                IdRol = idRol
            };

            this.MockUserRepository.Setup(x => x.GetDocument(1128467821)).ReturnsAsync(userOwnerEntity);

            var result = userServices.CreateOwner(userDTO);

            var response = new DomainUserValidateException(new StandardResponse { IsSuccess = false, Message = "Ya existe un Propietario con este documento." });
            Assert.ThrowsExceptionAsync<DomainUserValidateException>(async () => await result).Equals(response);

            MockUserRepository.Verify(a => a.GetDocument(1128467821));

        }

        [TestMethod]
        public void CreateOwnerErrorEmail()
        {
            int Id = 2;
            int documento = 1128467821;
            var nombre = "Carlos";
            var apellido = "Perez";
            var celular = "+573137653881";
            var correo = "carlos@pragma.com.co";
            var clave = "Acceso2";
            var idRol = 2;

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

            UserDTO userDTO = new()
            {
                Id = Id,
                Documento = documento,
                Nombre = nombre,
                Apellido = apellido,
                Celular = celular,
                Correo = correo,
                Clave = clave,
                IdRol = idRol
            };

            this.MockUserRepository.Setup(x => x.GetEmail("carlos@pragma.com.co")).ReturnsAsync(userOwnerEntity);

            var result = userServices.CreateOwner(userDTO);

            var response = new DomainUserValidateException(new StandardResponse { IsSuccess = false, Message = "Ya existe un Usuario con este correo." });
            Assert.ThrowsExceptionAsync<DomainUserValidateException>(async () => await result).Equals(response);

            MockUserRepository.Verify(a => a.GetEmail("carlos@pragma.com.co"));

        }

        [TestMethod]
        public void CreateOwnerError()
        {
            int Id = 16;
            int documento = 1149;
            var nombre = "Felipe";
            var apellido = "Murillo";
            var celular = "+573137653881";
            var correo = "felipe@pragma.com.co";
            var clave = "Acceso10";
            var idRol = 2;

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

            UserDTO userDTO = new()
            {
                Id = Id,
                Documento = documento,
                Nombre = nombre,
                Apellido = apellido,
                Celular = celular,
                Correo = correo,
                Clave = clave,
                IdRol = idRol
            };

            UserEntity userentity = null;
            var returnsResultModkGet = this.MockUserRepository.Setup(x => x.GetDocument(1149)).ReturnsAsync(userentity);
            var returnsResultModkGetEmail = this.MockUserRepository.Setup(x => x.GetEmail("felipe@pragma.com.co")).ReturnsAsync(userentity);

            var returnsResultModk = this.MockUserRepository.Setup(x => x.Add(userOwnerEntity)).ReturnsAsync(userOwnerEntity);

            var result = userServices.CreateOwner(userDTO);

            var response = new DomainUserValidateException(new StandardResponse { IsSuccess = false, Message = "Error creando el Propietario." });
            Assert.ThrowsExceptionAsync<DomainUserValidateException>(async () => await result).Equals(response);
        }
    }
}
