using System;
using System.Threading;
using System.Threading.Tasks;

namespace CustomTaskSchduler
{
    public class ClassToTest
    {
        private readonly IMessageBus _messageBus;
        private CancellationTokenSource _cancellationTokenSource;
        public event EventHandler OnNewMessage;

        public ClassToTest(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public void Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            Task.Factory.StartNew(() =>
            {
                var message = _messageBus.GetNextMessage();

                // Do work

                if (OnNewMessage != null)
                {
                    OnNewMessage(this, EventArgs.Empty);
                }

            }, _cancellationTokenSource.Token, 
                TaskCreationOptions.None, 
                TaskScheduler.Current);
        }

        public void Stop()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource = null;
            }
        }
    }
}