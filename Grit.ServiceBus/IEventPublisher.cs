using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.ServiceBus
{
    public interface IEventPublisher
    {
        void Publish<T>(T @event, bool singleThread) where T : Event;
    }
}
