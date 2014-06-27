using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS
{
    public class EventBus : IEventBus
    {
        private IEventHandlerFactory _eventHandlerFactory;

        public EventBus(IEventHandlerFactory eventHandlerFactory)
        {
            _eventHandlerFactory = eventHandlerFactory;
        }

        public void Publish<T>(T @event) where T : Event
        {
            log4net.LogManager.GetLogger("event.logger").Info(
                string.Format("{0}{1}{2}",
                @event, Environment.NewLine,
                JsonConvert.SerializeObject(@event)));

            var handlers = _eventHandlerFactory.GetHandlers<T>();
            if (handlers != null)
            {
                foreach (var handler in handlers)
                {
                    handler.Handle(@event);
                }
            }
        }
    }
}
