using MessageProcessing;
using NUnit.Framework;

namespace MessageProcessorTests
{
    [TestFixture]
    public class MessageProcessingTests
    {
        [Test]
        public void AddNewMessageProcessedMessageInQueue()
        {
            var messageQueue = new AsyncMesseageQueue();

            var manager = new MessageManager(messageQueue);

            manager.CreateNewMessage("a new message");

           Assert.AreEqual(1, messageQueue.Count);
        }

        [Test]
        public void QueueRaisedNewMessageEventClientProcessEvent()
        {
            var messageQueue = new AsyncMesseageQueue();

            var client = new MessageClient(messageQueue);

            client.HandleNewMessage(null, new MessageEventArgs("A new message"));

            Assert.AreEqual("A new message", client.LastMessage);
        }
    }
}
