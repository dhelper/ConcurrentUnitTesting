using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestUtils
{
    public class TestBase
    {
        private Exception _exceptionFromThread;

        [TestCleanup]
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
