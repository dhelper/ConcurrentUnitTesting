using System;
using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SignaledPattern
{
    [TestClass]
    public class TestWithBusySignal
    {
        [TestMethod]
        public void TestUsingSignal()
        {
            var waitHandle = new ManualResetEventSlim(false);

            var fakeOtherClass = A.Fake<IOtherClass>();
            A.CallTo(() => fakeOtherClass.DoSomething(A<int>._)).Invokes(waitHandle.Set);

            var cut = new ClassWithAsyncOperation(fakeOtherClass);

            cut.RunAsync(2, 3);

            Assert.IsTrue(waitHandle.Wait(10000), "OtherClass.DoSomething was never called");
        }
    }

    public class ClassWithAsyncOperation
    {
        private IOtherClass _otherClass;

        public ClassWithAsyncOperation(IOtherClass otherClass)
        {
            _otherClass = otherClass;
        }

        public void RunAsync(int a, int b)
        {
            Task.Run(() =>
            {
                var sum = a + b;

                _otherClass.DoSomething(sum);
            });
        }
    }

    public interface IOtherClass
    {
        void DoSomething(int sum);
    }
}
