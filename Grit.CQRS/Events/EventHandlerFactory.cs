using Ninject;
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
        private static IEnumerable<string> _assmblies;
        private static IDictionary<Type, List<Type>> _handlers;
        private static bool _isInitialized;
        private static readonly object _lockThis = new object();

        public static void Init(IKernel kernel, IEnumerable<string> assmblies)
        {
            if (!_isInitialized)
            {
                lock (_lockThis)
                {
                    _assmblies = assmblies;
                    _kernel = kernel;
                    HookHandlers();
                    _isInitialized = true;
                }
            }
        }

        private static void HookHandlers()
        {
            _handlers = new Dictionary<Type, List<Type>>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(n => _assmblies.Any(m => m == n.GetName().Name)))
            {
                var types = assembly.GetExportedTypes();
                var events = types.Where(x => x.IsSubclassOf(typeof(Event))).ToList();
                var allHandlers = types.Where(x => x.GetInterfaces()
                        .Any(a => a.IsGenericType && a.GetGenericTypeDefinition() == typeof(IEventHandler<>)));
                foreach (var @event in events)
                {
                    var handlers = allHandlers
                        .Where(h => h.GetInterfaces()
                            .Any(ii => ii.GetGenericArguments()
                                .Any(aa => aa == @event))).ToList();
                    List<Type> value;
                    if(_handlers.TryGetValue(@event, out value))
                    {
                        _handlers[@event].AddRange(value);
                    }
                    else
                    {
                        _handlers[@event] = handlers;
                    }
                }
            }
        }

        public IEnumerable<IEventHandler<T>> GetHandlers<T>() where T : Event
        {
            var handlers = _handlers[typeof(T)];
            var lstHandlers = handlers.Select(handler => (IEventHandler<T>)_kernel.GetService(handler)).ToList();
            return lstHandlers;
        }
    }
}
