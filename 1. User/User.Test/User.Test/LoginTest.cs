using Moq;
using Newtonsoft.Json;
using User.Domain.Business.DTO;
using User.Domain.Interface.Entities;
using User.Domain.Interface.IRepository;
using User.Domain.Interface.IServices;
using User.Domain.Interface.IServices.IFoodProxy;
using User.Domain.Services.Services;
using User.Test.Config;

namespace User.Test
{
    [TestClass]
    public class LoginTest
    {
        private IUserRepository _userRepository { get { return new Mock<IUserRepository>().Object; } }
        private IFoodServices _FoodServices { get { return new Mock<IFoodServices>().Object; } }

        [TestMethod]
        public async void TryValidateCreateOwner()
        {
            var context = ApplicationDbContextInMemory.Get();

            int documento = 1128436325;
            var nombre = "Jeison";
            var apellido = "Cañas";
            var celular = "3137653881";
            var correo = "jeison@pragma.com.co";
            var clave = "Acceso1";
            var idRol = 1;

            //context.Users.Add(new UserEntity
            //{
            //    Documento = documento,
            //    Nombre = nombre,
            //    Apellido = apellido,
            //    Celular = celular,
            //    Correo = correo,
            //    Clave = clave,
            //    IdRol = idRol
            //});

            //context.SaveChanges();

            var userServices = new UserServices(_userRepository, _FoodServices);

            var dato = await userServices.CreateOwner(new UserOwnerDTO
            {
                Documento = documento,
                Nombre = nombre,
                Apellido = apellido,
                Celular = celular,
                Correo = correo,
                Clave = clave,
                IdRol = idRol
            });

            var des = JsonConvert.DeserializeObject<UserOwnerDTO>(dato.Result.ToString());

            Assert.AreEqual(true, dato.IsSuccess);
        }
    }
}