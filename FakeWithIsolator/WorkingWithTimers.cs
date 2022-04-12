using System.Timers;
using NUnit.Framework;
using TypeMock.ArrangeActAssert;

namespace FakeWithIsolator
{
    [TestFixture]
    public class WorkingWithTimers
    {
        [Test, Isolated]
        public void ThisIsAGoodTest()
        {
            var fakeTimer = Isolate.Fake.Instance<Timer>();

            var cut = new ClassWithTimer(fakeTimer);

            var fakeEventArgs = Isolate.Fake.Instance<ElapsedEventArgs>();
            Isolate.Invoke.Event(() => fakeTimer.Elapsed += null, this, fakeEventArgs);

            Assert.IsTrue(cut.SomethingImportantHappened);
        }
    }

    public class ClassWithTimer
    {
        private readonly Timer _timer;

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
