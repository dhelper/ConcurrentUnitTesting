using System.Threading;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperServerFixed;

namespace ServerTests
{
    [TestClass]
    public class ServerTests
    {
        [TestMethod]
        public void ArrivingMessagePublishedTest()
        {
            var fakeMessageProvider = A.Fake<IMessageProvider>();
            A.CallTo(() => fakeMessageProvider.GetNextMessage()).Returns("Hello!");

            var messageHandler = new MessageHandler(fakeMessageProvider);
            messageHandler.HandleNextMessage();

            Assert.AreEqual("Hello!", messageHandler.LastMessage);
        }
    }
}
