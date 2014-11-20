using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MessageProcessing
{
    public interface IAsyncMesseageQueue
    {
        event EventHandler<MessageEventArgs> OnNewMessage;

        void Enque(string message);
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
