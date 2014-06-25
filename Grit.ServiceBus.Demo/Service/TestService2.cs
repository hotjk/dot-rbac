using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.ServiceBus.Demo
{
    public class TestService2 : ITestService2
    {
        public void Handle(TestEvent1 @event)
        {
            Console.WriteLine(@event.Content);
            BootStrapper.Bus.Publish(new TestEvent2 { Content = "Hello Event2!" });
        }
    }
}
