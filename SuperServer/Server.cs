using System.Threading;

namespace SuperServer
{
    public class Server
    {
        private readonly IMessageProvider _messageProvider;
        private Thread _worker;
        private bool _isAlive = true;

        public Server(IMessageProvider messageProvider)
        {
            _messageProvider = messageProvider;
        }

        public void Start()
        {
            _worker = new Thread(() =>
            {
                while (_isAlive)
                {
                    Thread.Sleep(1000);

                    var msg = _messageProvider.GetNextMessage();

                    //Do stuff

                    LastMessage = msg;
                }
            });

            _worker.Start();
        }

        public string LastMessage { get; set; }
    }
}
