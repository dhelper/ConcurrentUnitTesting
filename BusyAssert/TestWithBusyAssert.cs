using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusyAssert
{
    [TestClass]
    public class TestWithBusyAssert
    {
        [TestMethod]
        public void DifficultCalculationTest()
        {
            var cut = new ClassWithAsyncOperation();

            cut.RunAsync(2, 3);

            AssertHelper.BusyAssert(() => cut.Result == 5, 10, 50, "Calculation failed");
        }
    }

    public class ClassWithAsyncOperation
    {
        public void RunAsync(int a, int b)
        {
            Task.Run(() =>
            {
                var sum = a + b;

                Result = sum;
            });
        }

        public int Result { get; private set; }
    }

    class AssertHelper
    {
        public static void BusyAssert(Func<bool> condition, int numberOfRetries, int timeBetweenRetryMs, string errorMessage)
        {
            int iterationNum = 0;
            while (!condition())
            {
                iterationNum++;

                if (iterationNum == numberOfRetries)
                {
                    Assert.Fail(errorMessage);
                }
                
                Thread.Sleep(timeBetweenRetryMs);
            } 
        }
    }
}
