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

            cut.DiffcultCalcAsync(2, 3);

            var wasCalled = waitHandle.Wait(10000);

            Assert.IsTrue(wasCalled, "OtherClass.DoSomething was never called");
            Assert.AreEqual(5, cut.Result);
        }
    }

    public class ClassWithAsyncOperation
    {
        private IOtherClass _otherClass;

        public ClassWithAsyncOperation(IOtherClass otherClass)
        {
            _otherClass = otherClass;
        }

        public void DiffcultCalcAsync(int a, int b)
        {
            Task.Run(() =>
            {
                Result = a + b;

                _otherClass.DoSomething(Result);
            });
        }

        public int Result { get; private set; }
    }

    public interface IOtherClass
    {
        void DoSomething(int sum);
    }
}
