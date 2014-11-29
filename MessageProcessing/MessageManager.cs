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

            _messeageQueue.Enqueue(message);
        }
    }
}
