using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AwaitInTest
{
    [TestClass]
    public class AwaitingTests
    {
        [TestMethod]
        public async void ThisTestWouldNotFound()
        {
            var calculator = new MyAsyncCalculator();

            var result = await calculator.AddAsync(1, 2);

            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public async Task GoodAsyncTest()
        {
            var calculator = new MyAsyncCalculator();

            var result = await calculator.AddAsync(1, 2);

            Assert.AreEqual(3, result);
        }
    }

    class MyAsyncCalculator
    {
        public async Task<int> AddAsync(int a, int b)
        {
            await Task.Delay(1000);

            return a + b;
        }
    }
}
