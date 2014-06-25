using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS.Demo
{
    public class EventHandlerFactory : IEventHandlerFactory
    {
        const string name = "Grit.CQRS.Demo.Model";
        private static IDictionary<Type, List<Type>> eventHandlers;
        static EventHandlerFactory()
        {
            eventHandlers = new Dictionary<Type, List<Type>>();
            var assembly = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(a => a.GetName().Name == name);
            var types = assembly.GetExportedTypes();
            var events = types.Where(x => x.IsSubclassOf(typeof(Event))).ToList();
            var allHandlers = types.Where(x => x.GetInterfaces()
                    .Any(a => a.IsGenericType && a.GetGenericTypeDefinition() == typeof(IEventHandler<>)));
            foreach(var @event in events)
            {
                var handlers = allHandlers
                    .Where(h => h.GetInterfaces()
                        .Any(ii => ii.GetGenericArguments()
                            .Any(aa => aa == @event))).ToList();
                eventHandlers[@event] = handlers;
            }
        }

        public IEnumerable<IEventHandler<T>> GetHandlers<T>() where T : Event
        {
            var handlers = eventHandlers[typeof(T)];
            var lstHandlers = handlers.Select(handler => (IEventHandler<T>)BootStrapper.Kernel.GetService(handler)).ToList();
            return lstHandlers;
        }
    }
}
