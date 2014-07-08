using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS
{
    public interface ICallHandlerFactory
    {
        ICallHandler<T> GetHandler<T>() where T : Call;
        IModel GetChannel();
        string GetQueue();
        Type GetType(string name);
    }
}
