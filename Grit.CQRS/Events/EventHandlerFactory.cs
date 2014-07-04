using Ninject;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS
{
    public class EventHandlerFactory : IEventHandlerFactory
    {
        private static IKernel _kernel;
        private static IEnumerable<string> _eventAssmblies;
        private static IEnumerable<string> _handlerAssmblies;
        private static IDictionary<Type, List<Type>> _handlers;
        private static IModel _channel;
        private static string _exchange;
        private static bool _isInitialized;
        private static readonly object _lockThis = new object();

        private static IDictionary<string, Type> _eventTypes;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel">Ninject kernel</param>
        /// <param name="eventAssmblies">The assmbly name list that keep the domain command/event.</param>
        /// <param name="handlerAssmblies">The assmbly name list that keep the domain command/event handlers</param>
        /// <param name="channel">RabbitMQ queue channel</param>
        /// <param name="exchange">RabbitMQ exchange name</param>
        public static void Init(IKernel kernel,
            IEnumerable<string> eventAssmblies,
            IEnumerable<string> handlerAssmblies,
            IModel channel,
            string exchange)
        {
            if (!_isInitialized)
            {
                lock (_lockThis)
                {
                    _eventAssmblies = eventAssmblies;
                    _handlerAssmblies = handlerAssmblies;
                    _kernel = kernel;
                    HookHandlers();
                    _channel = channel;
                    _exchange = exchange;
                    _isInitialized = true;
                }
            }
        }

        public IModel GetChannel()
        {
            return _channel;
        }

        public string GetExchange()
        {
            return _exchange;
        }

        public Type GetEventType(string eventName)
        {
            return _eventTypes[eventName];
        }

        private static void HookHandlers()
        {
            _handlers = new Dictionary<Type, List<Type>>();
            
            List<Type> events = new List<Type>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies.Where(n => _eventAssmblies.Any(m => m == n.GetName().Name)))
            {
                events.AddRange(assembly.GetExportedTypes().Where(x => x.IsSubclassOf(typeof(Event))));
            }

            foreach (var assembly in assemblies.Where(n => _handlerAssmblies.Any(m => m == n.GetName().Name)))
            {
                var allHandlers = assembly.GetExportedTypes().Where(x => x.GetInterfaces()
                        .Any(a => a.IsGenericType && a.GetGenericTypeDefinition() == typeof(IEventHandler<>)));
                foreach (var @event in events)
                {
                    var handlers = allHandlers
                        .Where(h => h.GetInterfaces()
                            .Any(i => i.GetGenericArguments()
                                .Any(e => e == @event))).ToList();
                    if (handlers.Count() == 0)
                    {
                        continue;
                    }
                    List<Type> value;
                    if (_handlers.TryGetValue(@event, out value))
                    {
                        _handlers[@event].AddRange(handlers);
                    }
                    else
                    {
                        _handlers[@event] = handlers;
                    }
                }
            }
            _eventTypes = new Dictionary<string, Type>();
            foreach(Type type in events)
            {
                _eventTypes[type.Name] = type;
            }
            Log(events);
        }

        private static void Log(List<Type> events)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("EventBus:{0}", Environment.NewLine);
            foreach (var @event in events)
            {
                sb.AppendFormat("{0}{1}", @event, Environment.NewLine);
                List<Type> handlers;
                if (_handlers.TryGetValue(@event, out handlers))
                {
                    foreach (var handler in handlers)
                    {
                        sb.AppendFormat("\t{0}{1}", handler, Environment.NewLine);
                    }
                }
            }
            sb.AppendLine();
            log4net.LogManager.GetLogger("event.logger").Info(sb);
        }

        public IEnumerable<IEventHandler<T>> GetHandlers<T>() where T : Event
        {
            List<Type> handlers;
            if(_handlers.TryGetValue(typeof(T), out handlers))
            {
                return handlers.Select(handler => (IEventHandler<T>)_kernel.GetService(handler)).ToList();
            }
            return null;
        }
    }
}
