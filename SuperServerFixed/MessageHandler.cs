using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperServerFixed
{
    public class MessageHandler
    {
        private readonly IMessageProvider _messageProvider;

        public MessageHandler(IMessageProvider messageProvider)
        {
            _messageProvider = messageProvider;
        }

        public string LastMessage { get; set; }

        public void HandleNextMessage()
        {
            var msg = _messageProvider.GetNextMessage();

            //Do stuff

            LastMessage = msg;
        }
    }
}
