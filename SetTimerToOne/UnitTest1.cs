using System;
using System.Threading;
using System.Timers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Timer = System.Timers.Timer;

namespace SetTimerToOne
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ThisIsABadTest()
        {
            var timer = new Timer(1);

            var cut = new ClassWithTimer(timer);

            Thread.Sleep(1000);

            Assert.IsTrue(cut.SomethingImportantHappened);
        }
    }

    public class ClassWithTimer
    {
        private Timer _timer;

        public ClassWithTimer(Timer timer)
        {
            _timer = timer;
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            SomethingImportantHappened = true;
        }

        public bool SomethingImportantHappened { get; private set; }
    }
}
