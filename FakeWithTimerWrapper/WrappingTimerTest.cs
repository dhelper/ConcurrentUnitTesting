using System;
using System.Timers;
using FakeItEasy;
using NUnit.Framework;

namespace FakeWithTimerWrapper
{
    [TestFixture]
    public class WrappingTimerTest
    {
        [Test]
        public void ThisIsAGoodTest()
        {
            var fakeTimer = A.Fake<ITimer>();

            var cut = new ClassWithTimer(fakeTimer);

            fakeTimer.Elapsed += Raise.WithEmpty();

            Assert.IsTrue(cut.SomethingImportantHappened);
        }
    }

    public interface ITimer
    { 
        event EventHandler Elapsed;
        void Start();
        void Stop();
    }
    
    public class WrappedTimer : ITimer
    {
        private readonly Timer _timer;

        public event EventHandler Elapsed;
        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public WrappedTimer(Timer timer)
        {
            _timer = timer;
            _timer.AutoReset = false;
            _timer.Elapsed += OnTimerElapsed;
        }
        
        

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                Elapsed?.Invoke(sender, e);
            }
            finally
            {
                _timer.Start();
            }
        }
    }
    public class ClassWithTimer
    {
        private readonly ITimer _timer;

        public ClassWithTimer(ITimer timer)
        {
            _timer = timer;
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();
        }

        private void OnTimerElapsed(object sender, EventArgs e)
        {
            SomethingImportantHappened = true;
        }

        public bool SomethingImportantHappened { get; private set; }
    }

}