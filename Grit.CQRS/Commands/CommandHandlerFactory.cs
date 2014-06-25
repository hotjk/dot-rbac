using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS
{
    public class CommandHandlerFactory : ICommandHandlerFactory
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

            foreach(var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(n=>_assmblies.Any(m=>m == n.GetName().Name)))
            {
                var types = assembly.GetExportedTypes();
                var commands = types.Where(x => x.IsSubclassOf(typeof(Command))).ToList();
                var allHandlers = types.Where(x => x.GetInterfaces()
                        .Any(a => a.IsGenericType && a.GetGenericTypeDefinition() == typeof(ICommandHandler<>)));
                foreach(var command in commands)
                {
                    var handlers = allHandlers
                        .Where(h => h.GetInterfaces()
                            .Any(ii => ii.GetGenericArguments()
                                .Any(aa => aa == command))).ToList();
                    if(_handlers.ContainsKey(command))
                    {
                        throw new MoreThanOneDomainCommandHandlerException("more than one handler for command: " + command.Name);
                    }
                    _handlers[command] = handlers;
                }
            }
        }

        public ICommandHandler<T> GetHandler<T>() where T : Command
        {
            var handler = _handlers[typeof(T)].FirstOrDefault(); ;
            return (ICommandHandler<T>)_kernel.GetService(handler);
        }
    }
}
