using System;
using System.Collections.Generic;

namespace MessageProcessing
{
    public interface IAsyncMesseageQueue
    {
        event EventHandler<MessageEventArgs> OnNewMessage;

        void Enqueue(string message);
    }

    public class AsyncMesseageQueue : IAsyncMesseageQueue
    {
        private readonly Queue<string> _innerQueue = new Queue<string>();

        public event EventHandler<MessageEventArgs> OnNewMessage;
        public void Enqueue(string message)
        {
            _innerQueue.Enqueue(message);
        }

        public int Count
        {
            get { return _innerQueue.Count; }
        }

        // still missing message passing logic but won't be needed in demo
        // If you're reading this -- good for you
    }

    public class MessageEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public MessageEventArgs(string message)
        {
            Message = message;
        }
    }
}
