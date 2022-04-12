using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using SuperServer;

namespace SuperServerBadTests
{
    [TestFixture]
    public class ServerTests
    {
        [Test]
        public void ArrivingMessagePublishedTest()
        {
            var fakeMessageProvider = A.Fake<IMessageProvider>();
            A.CallTo(() => fakeMessageProvider.GetNextMessage()).Returns("Hello!");

            var server = new Server(fakeMessageProvider);
            server.Start();

            Thread.Sleep(2000);

            Assert.AreEqual("Hello!", server.LastMessage);
        }
        
        [Test]
        public async Task ArrivingMessagePublishedTest_Take2()
        {
            var fakeMessageProvider = A.Fake<IMessageProvider>();
            A.CallTo(() => fakeMessageProvider.GetNextMessage()).Returns("Hello!");

            var server = new Server(fakeMessageProvider);
            server.Start();

            await Task.Delay(2000);

            Assert.AreEqual("Hello!", server.LastMessage);
        }
    }
}
