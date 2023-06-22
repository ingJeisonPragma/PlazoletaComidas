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
    public class CreateCustomerTest
    {
        protected UserServices userServices;
        protected Mock<IUserRepository> MockUserRepository = new();
        protected Mock<IUserServices> MockUserServices = new();

        public CreateCustomerTest()
        {
            this.userServices = new UserServices(this.MockUserRepository.Object);
        }

        [TestMethod]
        public void CreateCustomerSuccess()
        {
            int Id = 7;
            int documento = 1102434539;
            var nombre = "Carolina";
            var apellido = "Quintero";
            var celular = "+573137653881";
            var correo = "caro@example.com";
            var clave = "Caro845.";
            var idRol = 4;

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

            var returnsResultModk = this.MockUserRepository.Setup(x => x.Add(userOwnerEntity));
            var result = userServices.CreateCustomer(userDTO).Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.IsSuccess);
            Assert.AreEqual(response, result);
        }

        [TestMethod]
        public void CreateCustomerErrorDocument()
        {
            int Id = 7;
            int documento = 1102434539;
            var nombre = "Carolina";
            var apellido = "Quintero";
            var celular = "+573137653881";
            var correo = "caro@example.com";
            var clave = "Caro845.";
            var idRol = 4;

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

            this.MockUserRepository.Setup(x => x.GetDocument(1102434539)).ReturnsAsync(userOwnerEntity);

            var result = userServices.CreateCustomer(userDTO);

            var response = new DomainUserValidateException(new StandardResponse { IsSuccess = false, Message = "Ya existe un Cliente con este documento." });
            Assert.ThrowsExceptionAsync<DomainUserValidateException>(async () => await result).Equals(response);

            MockUserRepository.Verify(a => a.GetDocument(1102434539));

        }

        [TestMethod]
        public void CreateCustomerErrorEmail()
        {
            int Id = 7;
            int documento = 1102434539;
            var nombre = "Carolina";
            var apellido = "Quintero";
            var celular = "+573137653881";
            var correo = "caro@example.com";
            var clave = "Caro845.";
            var idRol = 4;

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

            this.MockUserRepository.Setup(x => x.GetEmail("caro@example.com")).ReturnsAsync(userOwnerEntity);

            var result = userServices.CreateCustomer(userDTO);

            var response = new DomainUserValidateException(new StandardResponse { IsSuccess = false, Message = "Ya existe un Usuario con este correo." });
            Assert.ThrowsExceptionAsync<DomainUserValidateException>(async () => await result).Equals(response);

            MockUserRepository.Verify(a => a.GetEmail("caro@example.com"));

        }

        [TestMethod]
        public void CreateCustomerError()
        {
            int Id = 7;
            int documento = 1102434539;
            var nombre = "Carolina";
            var apellido = "Quintero";
            var celular = "+573137653881";
            var correo = "caro@example.com";
            var clave = "Caro845.";
            var idRol = 4;

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

            var result = userServices.CreateCustomer(userDTO);

            var response = new DomainUserValidateException(new StandardResponse { IsSuccess = false, Message = "Error creando el Cliente." });
            Assert.ThrowsExceptionAsync<DomainUserValidateException>(async () => await result).Equals(response);
        }
    }
}
