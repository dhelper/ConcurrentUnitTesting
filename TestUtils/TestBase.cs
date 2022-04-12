using System;
using System.Threading;
using NUnit.Framework;

namespace TestUtils
{
    public class TestBase
    {
        private Exception _exceptionFromThread;

        [TearDown]
        public void CheckForException()
        {
            if (_exceptionFromThread != null)
            {
                var exceptionFound = _exceptionFromThread;

                _exceptionFromThread = null;

                Assert.Fail(exceptionFound.ToString());
            }
        }

        public Thread RunInOtherThread(Action action)
        {
            var t = new Thread(() =>
            {
                try
                {
                    action();
                }
                catch (Exception exc)
                {
                    _exceptionFromThread = exc;
                }
            });

            t.Start();

            return t;
        }
    }
}
