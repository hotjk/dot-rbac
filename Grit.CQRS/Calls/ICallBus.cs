using Grit.CQRS.Calls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS
{
    public interface ICallBus
    {
        void Invoke<T>(T call) where T : Call;
        CallResponse Send<T>(T call) where T : Call;
        Type GetType(string name);
        string GetQueue();
    }
}
