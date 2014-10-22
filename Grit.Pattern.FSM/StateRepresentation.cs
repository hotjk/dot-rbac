using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.Pattern.FSM
{
    public partial class StateMachine<TState, TTrigger>
        where TState : struct, IConvertible
        where TTrigger : struct, IConvertible
    {
        internal class StateRepresentation
        {
            public TState State { get; private set; }
            private readonly IDictionary<TTrigger, TState> _triggerStates = new Dictionary<TTrigger, TState>();

            internal StateRepresentation(TState state)
            {
                State = state;
            }

            public void AddTriggerState(TTrigger trigger, TState state)
            {
                if(_triggerStates.ContainsKey(trigger))
                {
                    throw new ArgumentException("The trigger already exist in the representation.");
                }
                _triggerStates[trigger] = state;
            }

            public bool TryFind(TTrigger trigger, out TState state)
            {
                return _triggerStates.TryGetValue(trigger, out state);
            }
        }
    }
}
