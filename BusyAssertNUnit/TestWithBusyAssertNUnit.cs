using System.Threading.Tasks;
using NUnit.Framework;

namespace BusyAssertNUnit
{
    [TestFixture]
    public class TestWithBusyAssertNUnit
    {
        [Test]
        public void DifficultCalculationTest()
        {
            var cut = new ClassWithAsyncOperation();

            cut.RunAsync(2, 3);

            Assert.That(() => cut.Result, Is.EqualTo(5).After(10000, 10));
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
}
