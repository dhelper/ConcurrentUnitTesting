using FakeItEasy;
using NUnit.Framework;
using SuperServerFixed;

namespace ServerTests
{
    [TestFixture]
    public class ServerTests
    {
        [Test]
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
