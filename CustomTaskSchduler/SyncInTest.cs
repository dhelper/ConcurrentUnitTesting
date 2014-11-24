using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CustomTaskSchduler
{
    [TestClass]
    public class SyncInTest
    {
        [TestMethod]
        public void BadTest()
        {
            var fakeMessageBus = A.Fake<IMessageBus>();
            bool wasCalled = false;

            var client = new ClassToTest(fakeMessageBus);
            client.OnNewMessage += (sender, args) => wasCalled = true;

            client.Start();

            Assert.IsTrue(wasCalled);
        }

        [TestMethod]
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

        [TestMethod]
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