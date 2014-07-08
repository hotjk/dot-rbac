using Grit.CQRS.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS
{
    public interface IActionBus
    {
        void Invoke<T>(T action) where T : Action;
        ActionResponse Send<T>(T action) where T : Action;
        Type GetType(string name);
        string GetQueue();
    }
}
