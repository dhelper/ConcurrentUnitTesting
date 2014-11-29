using MessageProcessing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessageProceesorTests
{
    [TestClass]
    public class MessageProcessingTests
    {
        [TestMethod]
        public void AddNewMessageProcessedMessageInQueue()
        {
            var messeageQueue = new AsyncMesseageQueue();

            var manager = new MessageManager(messeageQueue);

            manager.CreateNewMessage("a new message");

           Assert.AreEqual(1, messeageQueue.Count);
        }

        [TestMethod]
        public void QueueRaisedNewMessageEventClientProcessEvent()
        {
            var messeageQueue = new AsyncMesseageQueue();

            var client = new MessageClient(messeageQueue);

            client.HandleNewMessage(null, new MessageEventArgs("A new message"));

            Assert.AreEqual("A new message", client.LastMessage);
        }
    }
}
