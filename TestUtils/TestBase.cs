using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestUtils
{
    public class TestBase
    {
        private Exception _exceptionFromOtherThread;

        [TestCleanup]
        public void CheckForException()
        {
            if (_exceptionFromOtherThread != null)
            {
                var exceptionFound = _exceptionFromOtherThread;

                _exceptionFromOtherThread = null;

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
                    _exceptionFromOtherThread = exc;
                }
            });

            t.Start();

            return t;
        }
    }
}
