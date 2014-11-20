using System;
using FakeItEasy;
using MessageProcessing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessageProceesor
{
    [TestClass]
    public class MessageProcessingTests
    {
        [TestMethod]
        public void AddNewMessageProcessedMessageInQueue()
        {
            var fakeMessageQueue = A.Fake<IAsyncMesseageQueue>();

            var manager = new MessageManager(fakeMessageQueue);

            manager.CreateNewMessage("a new message");

            A.CallTo(() => fakeMessageQueue.Enque("a new message")).MustHaveHappened();
        }

        [TestMethod]
        public void QueueRaisedNewMessageEventClientProcessEvent()
        {
            var fakeMessageQueue = A.Fake<IAsyncMesseageQueue>();

            var client = new MessageClient(fakeMessageQueue);

            fakeMessageQueue.OnNewMessage += Raise.With(new MessageEventArgs("A new message"));

            Assert.AreEqual("A new message", client.LastMessage);
        }
    }
}
