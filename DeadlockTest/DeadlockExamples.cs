using System;
using System.Threading;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestUtils;

namespace DeadlockTest
{
    [TestClass]
    public class DeadlockExamples : TestBase
    {
        [TestMethod, Timeout(5000)]
        public void CheckForDeadlock()
        {
            var fakeDependency1 = A.Fake<IDependency>();
            var fakeDependency2 = A.Fake<IDependency>();

            var waitHandle = new ManualResetEvent(false);

            var cut = new ClassWithDeadlock(fakeDependency1, fakeDependency2);

            A.CallTo(() => fakeDependency1.Call()).Invokes(() => waitHandle.WaitOne());
            A.CallTo(() => fakeDependency2.Call()).Invokes(() => waitHandle.WaitOne());

            var t1 = RunInOtherThread(() => cut.Call1Then2());
            var t2 = RunInOtherThread(() => cut.Call2Then1());

            waitHandle.Set();

            t1.Join();
            t2.Join();
        }
    }

    public class ClassWithDeadlock
    {
        private readonly IDependency _dependency1;
        private readonly IDependency _dependency2;
        private object _syncObject1 = new object();
        private object _syncObject2 = new object();


        public ClassWithDeadlock(IDependency dependency1, IDependency dependency2)
        {
            _dependency1 = dependency1;
            _dependency2 = dependency2;
        }

        public void Call1Then2()
        {
            lock (_syncObject1)
            {
                _dependency1.Call();
                lock (_syncObject2)
                {
                    _dependency2.Call();
                }
            }
        }

        public void Call2Then1()
        {
            lock (_syncObject2)
            {
                _dependency2.Call();
                lock (_syncObject1)
                {
                    _dependency1.Call();
                }
            }
        }
    }

    public interface IEmailClient
    {
    }

    public interface IDependency
    {
        bool UserExist(string userId);
        void Call();
    }
}
