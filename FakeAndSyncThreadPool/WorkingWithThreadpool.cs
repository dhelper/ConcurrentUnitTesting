using System.Threading;
using NUnit.Framework;
using TypeMock.ArrangeActAssert;

namespace FakeAndSyncThreadPool
{
    [TestFixture]
    public class WorkingWithThreadpool
    {
        [Test, Isolated]
        public void UsingWrapperTest()
        {
            Isolate.WhenCalled(() => ThreadPoolWrapper.QueueUserWorkItem(null))
                .DoInstead(context => ((WaitCallback)context.Parameters[0]).Invoke(null));

            var cut = new ClassWithWrappedThreadpool();
            cut.RunInThread();

            Assert.IsTrue(cut.SomethingImportantHappened);
        }
    }

    public class ClassWithWrappedThreadpool
    {
        public void RunInThread()
        {
            ThreadPoolWrapper.QueueUserWorkItem(_ =>
            {
                SomethingImportantHappened = true;
            });
        }

        public bool SomethingImportantHappened { get; private set; }
    }


    public class ThreadPoolWrapper
    {
        public static void QueueUserWorkItem(WaitCallback callback)
        {
            ThreadPool.QueueUserWorkItem(callback);
        }
    }
}
