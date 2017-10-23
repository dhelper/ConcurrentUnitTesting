using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestForAsync
{
    [TestClass]
    public class AsyncVerifyTests
    {
        [TestMethod, Timeout(10000)]
        public void CallDoneInOtherThreadWithOneCall()
        {
            var waitHandle = new ManualResetEvent(false);
            var counting = new CountdownEvent(1);
            var fakeClient = A.Fake<IClient>();
            A.CallTo(() => fakeClient.Send(A<Message>._)).Invokes(() =>
            {
                waitHandle.WaitOne();
                counting.Signal();
            });

            var mailer = new Mailer(fakeClient);

            mailer.SendEmail("address1", "");
            waitHandle.Set();

            var eventsReached = counting.Wait(5000);

            Assert.IsTrue(eventsReached, "One or more of the operation didnt happen");
        }

        [TestMethod, Timeout(10000)]
        public void CallDoneInOtherThreadWithTwoCalls()
        {
            var waitHandle = new ManualResetEvent(false);
            var counting = new CountdownEvent(2);
            var fakeClient = A.Fake<IClient>();
            A.CallTo(() => fakeClient.Send(A<Message>.That.Matches(message => message.To == "address1")))
                .Invokes(() =>
            {
                waitHandle.WaitOne();
                counting.Signal();
            });

            A.CallTo(() => fakeClient.Send(A<Message>.That.Matches(message => message.To == "address2")))
                .Invokes(() => counting.Signal());

            var mailer = new Mailer(fakeClient);

            mailer.SendEmail("address1", "");
            mailer.SendEmail("address2", "");
            waitHandle.Set();

            var eventsReached = counting.Wait(5000);

            Assert.IsTrue(eventsReached, "One or more of the operation didnt happen");
        }
    }

    public class Mailer
    {
        private readonly IClient _client;

        public Mailer(IClient client)
        {
            _client = client;
        }

        public void SendEmail(string address, string text)
        {
            var message = new Message
            {
                To = address,
                Body = text
            };

            Task.Factory.StartNew(() => _client.Send(message));
        }
    }

    public interface IClient
    {
        void Send(Message message);
    }

    public class Message
    {
        public string To { get; set; }
        public string Body { get; set; }
    }
}
