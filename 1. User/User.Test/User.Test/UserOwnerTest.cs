using Moq;
using Newtonsoft.Json;
using User.Domain.Business.DTO;
using User.Domain.Interface.Entities;
using User.Domain.Interface.IRepository;
using User.Domain.Interface.IServices.IFoodProxy;
using User.Domain.Services.Services;

namespace User.Test
{
    [TestClass]
    public class UserOwnerTest
    {
        protected UserServices userServices;
        protected Mock<IUserRepository> MockUserRepository = new Mock<IUserRepository>();
        protected Mock<IFoodServices> MockFoodServices= new Mock<IFoodServices>();
        public UserOwnerTest() { 
            this.userServices = new UserServices(this.MockUserRepository.Object, this.MockFoodServices.Object);
        }
        //private IUserRepository _userRepository { get { return new Mock<IUserRepository>().Object; } }
        //private IFoodServices _FoodServices { get { return new Mock<IFoodServices>().Object; } }

        [TestMethod]
        public void GetUserSuccess()
        {
            int documento = 1128436325;
            var nombre = "Jeison";
            var apellido = "Cañas";
            var celular = "3137653881";
            var correo = "jeison@pragma.com.co";
            var clave = "Acceso1";
            var idRol = 1;

            UserEntity userOwnerEntity = new()
            {
                Documento = documento,
                Nombre = nombre,
                Apellido = apellido,
                Celular = celular,
                Correo = correo,
                Clave = clave,
                IdRol = idRol,
                Rol = new RolEntity()
            };

            this.MockUserRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(userOwnerEntity);

            UserOwnerDTO userOwner = new()
            {
                Documento = documento,
                Nombre = nombre,
                Apellido = apellido,
                Celular = celular,
                Correo = correo,
                Clave = clave,
                IdRol = idRol
            };

            var result = userServices.GetUser(1).Result;

            Assert.IsNotNull(result);

            //var Owner = JsonConvert.DeserializeObject<UserOwnerDTO>(result.Result.ToString());

            Assert.AreEqual(true, result.IsSuccess);
           
        }
    }
}