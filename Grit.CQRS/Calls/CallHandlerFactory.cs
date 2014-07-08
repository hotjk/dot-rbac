using Ninject;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS
{
    public class CallHandlerFactory : ICallHandlerFactory
    {
        private static IKernel _kernel;
        private static IEnumerable<string> _callAssmblies;
        private static IEnumerable<string> _handlerAssmblies;
        private static IDictionary<Type, Type> _handlers;
        private static IModel _channel;
        private static string _queue;
        private static bool _isInitialized;
        private static readonly object _lockThis = new object();

        private static IDictionary<string, Type> _callTypes;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel">Ninject kernel</param>
        /// <param name="callAssmblies">The assmbly name list that keep the domain command/call.</param>
        /// <param name="handlerAssmblies">The assmbly name list that keep the domain command/call handlers</param>
        /// <param name="channel">RabbitMQ queue channel</param>
        /// <param name="queue">RabbitMQ exchange name</param>
        public static void Init(IKernel kernel,
            IEnumerable<string> callAssmblies,
            IEnumerable<string> handlerAssmblies,
            IModel channel,
            string queue)
        {
            if (!_isInitialized)
            {
                lock (_lockThis)
                {
                    _callAssmblies = callAssmblies;
                    _handlerAssmblies = handlerAssmblies;
                    _kernel = kernel;
                    HookHandlers();
                    _channel = channel;
                    _queue = queue;
                    _isInitialized = true;
                }
            }
        }

        public IModel GetChannel()
        {
            return _channel;
        }

        public string GetQueue()
        {
            return _queue;
        }

        public Type GetType(string callName)
        {
            return _callTypes[callName];
        }

        private static void HookHandlers()
        {
            _handlers = new Dictionary<Type, Type>();
            List<Type> calls = new List<Type>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies.Where(n => _callAssmblies.Any(m => m == n.GetName().Name)))
            {
                calls.AddRange(assembly.GetExportedTypes().Where(x => x.IsSubclassOf(typeof(Call))));
            }

            foreach (var assembly in assemblies.Where(n => _handlerAssmblies.Any(m => m == n.GetName().Name)))
            {
                var allHandlers = assembly.GetExportedTypes().Where(x => x.GetInterfaces()
                        .Any(a => a.IsGenericType && a.GetGenericTypeDefinition() == typeof(ICallHandler<>)));
                foreach (var call in calls)
                {
                    var handlers = allHandlers
                        .Where(h => h.GetInterfaces()
                            .Any(i => i.GetGenericArguments()
                                .Any(e => e == call))).ToList();

                    if (handlers.Count > 1 ||
                        (handlers.Count == 1 && _handlers.ContainsKey(call)))
                    {
                        throw new MoreThanOneDomainCommandHandlerException("more than one handler for call: " + call.Name);
                    }
                    if (handlers.Count == 1)
                    {
                        _handlers[call] = handlers.First();
                    }
                }
            }
            //foreach (var call in calls)
            //{
            //    if (!_handlers.ContainsKey(call))
            //    {
            //        throw new UnregisteredDomainCommandException("no handler registered for call: " + call.Name);
            //    }
            //}
            _callTypes = new Dictionary<string, Type>();
            foreach (Type type in calls)
            {
                _callTypes[type.Name] = type;
            }
            Log(calls);
        }

        private static void Log(List<Type> calls)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("CallBus:{0}", Environment.NewLine);
            foreach (var call in calls)
            {
                sb.AppendFormat("{0}{1}", call, Environment.NewLine);
                Type value;
                if (_handlers.TryGetValue(call, out value))
                {
                    sb.AppendFormat("\t{0}{1}", value, Environment.NewLine);
                }
            }
            sb.AppendLine();
            log4net.LogManager.GetLogger("call.logger").Debug(sb);
        }

        public ICallHandler<T> GetHandler<T>() where T : Call
        {
            Type handler;
            if (!_handlers.TryGetValue(typeof(T), out handler))
            {
                throw new UnregisteredDomainCommandException("no handler registered for call: " + typeof(T));
            }

            return (ICallHandler<T>)_kernel.GetService(handler);
        }
    }
}
