using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.ServiceBus.Demo
{
    public class TestService1 : ITestService1
    {
        public void Handle(TestCommand message)
        {
            Console.WriteLine(message.Content);
            BootStrapper.Bus.Publish(new TestEvent1 { Content = "Hello Event1!" });
        }

        public void Handle(TestEvent2 @event)
        {
            Console.WriteLine(@event.Content);
        }
    }
}
