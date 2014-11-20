using System.Threading;

namespace SuperServerFixed
{
    public class Server
    {
        private Thread _worker;
        private bool _isAlive = true;
        private readonly MessageHandler _messageHandler;

        public Server(IMessageProvider messageProvider)
        {
            _messageHandler = new MessageHandler(messageProvider);
        }

        public void Start()
        {
            _worker = new Thread(() =>
            {
                while (_isAlive)
                {
                    Thread.Sleep(1000);

                    _messageHandler.HandleNextMessage();
                }
            });

            _worker.Start();
        }

        public string LastMessage { get { return _messageHandler.LastMessage; } }

        public void Stop()
        {
            _isAlive = false;
            _worker.Join();
        }
    }
}
