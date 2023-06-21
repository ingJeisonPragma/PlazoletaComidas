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
    public class CreateEmployeeTest
    {
        protected UserServices userServices;
        protected Mock<IUserRepository> MockUserRepository = new();
        protected Mock<IUserServices> MockUserServices = new();

        public CreateEmployeeTest()
        {
            this.userServices = new UserServices(this.MockUserRepository.Object);
        }

        [TestMethod]
        public void CreateEmployeeSuccess()
        {
            int Id = 3;
            int documento = 1147;
            var nombre = "Sandra";
            var apellido = "Martin";
            var celular = "+573137653881";
            var correo = "Sandra@example.com";
            var clave = "Acceso3";
            var idRol = 3;

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

            var response = new StandardResponse { IsSuccess = true, Message = "Se creo correctamente el Empleado." };

            UserEntity userentity = null;
            this.MockUserRepository.Setup(x => x.GetDocument(1147)).ReturnsAsync(userentity);
            this.MockUserRepository.Setup(x => x.GetEmail("Sandra@example.com")).ReturnsAsync(userentity);

            var returnsResultModk = this.MockUserRepository.Setup(x => x.Add(userOwnerEntity)).ReturnsAsync(userOwnerEntity);

            var result = userServices.CreateEmployee(userDTO).Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.IsSuccess);
            //Assert.AreEqual(response, result);
        }

        [TestMethod]
        public void CreateEmployeeErrorDocument()
        {
            int Id = 3;
            int documento = 1147;
            var nombre = "Sandra";
            var apellido = "Martin";
            var celular = "+573137653881";
            var correo = "Sandra@example.com";
            var clave = "Acceso3";
            var idRol = 3;

            UserEntity userEmpEntity = new()
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

            this.MockUserRepository.Setup(x => x.GetDocument(1147)).ReturnsAsync(userEmpEntity);

            var result = userServices.CreateEmployee(userDTO);

            var response = new DomainUserValidateException(new StandardResponse { IsSuccess = false, Message = "Ya existe un Empleado con este documento." });
            Assert.ThrowsExceptionAsync<DomainUserValidateException>(async () => await result).Equals(response);

            MockUserRepository.Verify(a => a.GetDocument(1147));

        }

        [TestMethod]
        public void CreateEmployeeErrorEmail()
        {
            int Id = 3;
            int documento = 1147;
            var nombre = "Sandra";
            var apellido = "Martin";
            var celular = "+573137653881";
            var correo = "Sandra@example.com";
            var clave = "Acceso3";
            var idRol = 3;

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

            this.MockUserRepository.Setup(x => x.GetEmail("Sandra@example.com")).ReturnsAsync(userOwnerEntity);

            var result = userServices.CreateEmployee(userDTO);

            var response = new DomainUserValidateException(new StandardResponse { IsSuccess = false, Message = "Ya existe un Usuario con este correo." });
            Assert.ThrowsExceptionAsync<DomainUserValidateException>(async () => await result).Equals(response);

            MockUserRepository.Verify(a => a.GetEmail("Sandra@example.com"));

        }
    }
}
