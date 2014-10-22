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
        private readonly IDictionary<TState, StateRepresentation> _stateConfiguration = new Dictionary<TState, StateRepresentation>();

        public StateConfiguration Configure(TState state)
        {
            StateRepresentation found;
            if (!_stateConfiguration.TryGetValue(state, out found))
            {
                found = new StateRepresentation(state);
                _stateConfiguration.Add(state, found);
            }

            return new StateConfiguration(found);
        }

        public StateInstance Instance(TState initialState)
        {
            return new StateInstance(this, initialState);
        }

        public bool Try(TState state, TTrigger trigger, out TState destinationState)
        {
            StateRepresentation representation;
            if (!_stateConfiguration.TryGetValue(state, out representation))
            {
                destinationState = default(TState);
                return false;
            }

            return representation.TryFind(trigger, out destinationState);
        }
    }
}
