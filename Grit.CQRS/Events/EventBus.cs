using Newtonsoft.Json;
using RabbitMQ.Client.Framing.v0_9_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Grit.CQRS
{
    public class EventBus : IEventBus
    {
        private IEventHandlerFactory _eventHandlerFactory;
        private IList<Event> _events = new List<Event>();
        private string _exchange;

        public EventBus(string exchange, 
            IEventHandlerFactory eventHandlerFactory)
        {
            _exchange = exchange;
            _eventHandlerFactory = eventHandlerFactory;
        }

        public void Publish<T>(T @event) where T : Event
        {
            _events.Add(@event);
        }

        public void Flush()
        {
            foreach (Event @event in _events)
            {
                FlushAnEvent(@event);
            }
            _events.Clear();
        }

        private void FlushAnEvent<T>(T @event) where T : Event
        {
            string json = JsonConvert.SerializeObject(@event);
            log4net.LogManager.GetLogger("event.logger").Info(
                string.Format("{0}{1}{2}",
                @event, Environment.NewLine,
                json));

            var channel = ServiceLocator.Channel;

            channel.BasicPublish(_exchange,
                @event.RoutingKey, 
                new BasicProperties { 
                    DeliveryMode = 2,
                    Type = @event.Type
                },
                Encoding.UTF8.GetBytes(json));

            var handlers = _eventHandlerFactory.GetHandlers<T>();
            if (handlers != null)
            {
                foreach (var handler in handlers)
                {
                    // handle event in thread pool
                    ThreadPool.QueueUserWorkItem(x =>
                    {
                        try
                        {
                            handler.Handle(@event);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    });
                }
            }
        }

        public void Handle<T>(T @event) where T : Event
        {
            var handlers = _eventHandlerFactory.GetHandlers<T>();
            if (handlers != null)
            {
                foreach (var handler in handlers)
                {
                    // handle event in current thread
                    handler.Handle(@event);
                }
            }
        }
        public Type GetType(string eventName)
        {
            return _eventHandlerFactory.GetType(eventName);
        }

        public void Clear()
        {
            _events.Clear();
        }
    }
}
