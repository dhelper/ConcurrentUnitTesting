using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;

namespace CustomTaskSchduler
{
    [TestFixture]
    public class SyncInTest
    {
        [Test]
        public void BadTest()
        {
            var fakeMessageBus = A.Fake<IMessageBus>();
            bool wasCalled = false;

            var client = new ClassToTest(fakeMessageBus);
            client.OnNewMessage += (sender, args) => wasCalled = true;

            client.Start();

            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void WorseTest()
        {
            var fakeMessageBus = A.Fake<IMessageBus>();
            bool wasCalled = false;

            var client = new ClassToTest(fakeMessageBus);
            client.OnNewMessage += (sender, args) => wasCalled = true;

            client.Start();
            Thread.Sleep(10000);


            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void GoodTest()
        {
            var fakeMessageBus = A.Fake<IMessageBus>();
            bool wasCalled = false;

            Task.Factory.StartNew(() =>
            {
                var client = new ClassToTest(fakeMessageBus);
                client.OnNewMessage += (sender, args) => wasCalled = true;

                client.Start();
            }, CancellationToken.None,
            TaskCreationOptions.None,
            new CurrentThreadTaskScheduler());

            Assert.IsTrue(wasCalled);
        }
    }
}