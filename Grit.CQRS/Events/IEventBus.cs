using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS
{
    public interface IEventBus
    {
        void Publish<T>(T @event) where T : Event;
        void FlushAll();
        void Flush<T>(T @event) where T : Event;
        void DirectHandle<T>(T @event) where T : Event;
        Type GetType(string name);

        void Clear();
    }
}
