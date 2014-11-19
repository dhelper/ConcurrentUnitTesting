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
            _worker = new Thread(RunMessageLoop);
            _worker.Start();
        }

        public void Stop()
        {
            _isAlive = false;
            _worker.Join();
        }

        private void RunMessageLoop()
        {
            do
            {
                Thread.Sleep(1000);

                var msg = _messageProvider.GetNextMessage();

                //Do stuff

                LastMessage = msg;

            } while (_isAlive);
        }

        public string LastMessage { get; set; }
    }
}
