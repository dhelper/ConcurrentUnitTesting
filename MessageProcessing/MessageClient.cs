namespace MessageProcessing
{
    public class MessageClient
    {
        private readonly IAsyncMesseageQueue _messeageQueue;

        public MessageClient(IAsyncMesseageQueue messeageQueue)
        {
            _messeageQueue = messeageQueue;
            _messeageQueue.OnNewMessage += HandleNewMessage;
        }

        public string LastMessage { get; set; }

        internal void HandleNewMessage(object sender, MessageEventArgs e)
        {
            // Here Be Code!

            LastMessage = e.Message;
        }
    }
}