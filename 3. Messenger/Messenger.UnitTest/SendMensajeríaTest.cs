using Messenger.Domain.Business.DTO;
using Messenger.Domain.Services.Exceptions;
using Messenger.Domain.Services.IServices;
using Messenger.Domain.Services.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using Twilio.Clients;

namespace Messenger.UnitTest
{
    [TestClass]
    public class SendMensajeríaTest
    {
        protected MessengerServices messengerServices;
        protected Mock<IConfiguration> mockconfiguration = new();
        protected Mock<IMessengerServices> MockMessengerServices = new();

        public SendMensajeríaTest()
        {
            this.messengerServices = new MessengerServices(this.mockconfiguration.Object);
        }

        [TestMethod]
        public void PostSendsmsSuccess()
        {
            TwilioRequestDTO requestDTO = new()
            {
                ToNumber = "+573137653881",
                msg = "Código de validación de Pedido N° 5812",
            };

            string account = "ACc57f618e74ec4bbcaf4058783ddeee19";
            string auth = "7de489a29d126693a25d89e1f157b145";
            string numb = "+13612648733";

            
            var dato = mockconfiguration.Setup(c => c["Twilio:AccountSID"]).Returns(account);
            var dato2 = mockconfiguration.Setup(c => c["Twilio:AuthToken"]).Returns(auth);
            var dato3 = mockconfiguration.Setup(c => c["Twilio:FromNumber"]).Returns(numb);

            var result = messengerServices.SendSMS(requestDTO).Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.IsSuccess);
        }

        [TestMethod]
        public void PostSendsmsError()
        {
            TwilioRequestDTO requestDTO = new()
            {
                ToNumber = "+573126960190",
                msg = "Código de validación de Pedido N° 5910",
            };

            string account = "ACc57f618e74ec4bbcaf4058783ddeee19";
            string auth = "7de489a29d126";
            string numb = "+13612648733";

            var dato = mockconfiguration.Setup(c => c["Twilio:AccountSID"]).Returns(account);
            var dato2 = mockconfiguration.Setup(c => c["Twilio:AuthToken"]).Returns(auth);
            var dato3 = mockconfiguration.Setup(c => c["Twilio:FromNumber"]).Returns(numb);

            var result = messengerServices.SendSMS(requestDTO);
            var response = new SmsException(new StandardResponse { IsSuccess = false, Message = "Error enviando el mensaje." });
            Assert.ThrowsExceptionAsync<SmsException>(async () => await result).Equals(response);
        }
    }
}