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
            var messageQueue = new AsyncMesseageQueue();

            var manager = new MessageManager(messageQueue);

            manager.CreateNewMessage("a new message");

           Assert.AreEqual(1, messageQueue.Count);
        }

        [TestMethod]
        public void QueueRaisedNewMessageEventClientProcessEvent()
        {
            var messageQueue = new AsyncMesseageQueue();

            var client = new MessageClient(messageQueue);

            client.HandleNewMessage(null, new MessageEventArgs("A new message"));

            Assert.AreEqual("A new message", client.LastMessage);
        }
    }
}
