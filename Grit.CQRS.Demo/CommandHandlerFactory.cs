using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS.Demo
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        const string name = "Grit.CQRS.Demo.Model";
        private static IDictionary<Type, List<Type>> commandHandlers;
        static CommandHandlerFactory()
        {
            commandHandlers = new Dictionary<Type, List<Type>>();
            var assembly = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(a => a.GetName().Name == name);
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
                commandHandlers[command] = handlers;
            }
        }

        public ICommandHandler<T> GetHandler<T>() where T : Command
        {
            var handler = commandHandlers[typeof(T)].FirstOrDefault(); ;
            return (ICommandHandler<T>)BootStrapper.Kernel.GetService(handler);
        }
    }
}
