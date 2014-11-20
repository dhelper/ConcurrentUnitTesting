using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessageProcessing
{
    public class MessageManager
    {
        private readonly IAsyncMesseageQueue _messeageQueue;

        public MessageManager(IAsyncMesseageQueue messeageQueue)
        {
            _messeageQueue = messeageQueue;
        }

        public void CreateNewMessage(string message)
        {
            // Here Be Code!

            _messeageQueue.Enque(message);
        }
    }

    public class MessageClient
    {
        private readonly IAsyncMesseageQueue _messeageQueue;

        public MessageClient(IAsyncMesseageQueue messeageQueue)
        {
            _messeageQueue = messeageQueue;
            _messeageQueue.OnNewMessage += HandleNewMessage;
        }

        public string LastMessage { get; set; }

        private void HandleNewMessage(object sender, MessageEventArgs e)
        {
            // Here Be Code!

            LastMessage = e.Message;
        }
    }
}
