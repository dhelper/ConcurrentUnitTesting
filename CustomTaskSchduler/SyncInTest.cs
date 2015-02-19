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
            A.CallTo(() => fakeMessageBus.GetNextMessage()).Returns(null);

            bool wasCalled = false;

            var client = new ClassToTest(fakeMessageBus);
            client.OnNewMessage += (sender, args) => wasCalled = true;

            client.Start();

            Assert.IsFalse(wasCalled);
        }

        [TestMethod]
        public void WorseTest()
        {
            var fakeMessageBus = A.Fake<IMessageBus>();
            A.CallTo(() => fakeMessageBus.GetNextMessage()).Returns(null);
            bool wasCalled = false;

            var client = new ClassToTest(fakeMessageBus);
            client.OnNewMessage += (sender, args) => wasCalled = true;

            client.Start();
            Thread.Sleep(10000);


            Assert.IsFalse(wasCalled);
        }

        [TestMethod]
        public void GoodTest()
        {
            var fakeMessageBus = A.Fake<IMessageBus>();
            A.CallTo(() => fakeMessageBus.GetNextMessage()).Returns(null);

            bool wasCalled = false;
            var currentThreadTaskScheduler = new CurrentThreadTaskScheduler();

            Task.Factory.StartNew(() =>
            {
                var client = new ClassToTest(fakeMessageBus);
                client.OnNewMessage += (sender, args) => wasCalled = true;

                client.Start();
            }, CancellationToken.None,
            TaskCreationOptions.None,
            currentThreadTaskScheduler);

            Assert.IsFalse(wasCalled);
        }
    }
}

#region Backup
/*var currentThreadTaskScheduler = new CurrentThreadTaskScheduler();

Task.Factory.StartNew(() =>
{
    var client = new ClassToTest(fakeMessageBus);
    client.OnNewMessage += (sender, args) => wasCalled = true;

    client.Start();
}, CancellationToken.None,
TaskCreationOptions.None,
currentThreadTaskScheduler);*/
#endregion