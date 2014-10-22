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
        public class StateConfiguration
        {
            private readonly StateRepresentation _representation;
            internal StateConfiguration(StateRepresentation representation)
            {
                _representation = representation;
            }

            public StateConfiguration Permit(TTrigger trigger, TState destinationState)
            {
                if(_representation.State.Equals(destinationState))
                {
                    throw new ArgumentException("The destination state MUST not equal to the source state.");
                }
                _representation.AddTriggerState(trigger, destinationState);
                return this;
            }
        }
    }
}
