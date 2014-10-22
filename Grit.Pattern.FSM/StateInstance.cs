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
        public class StateInstance
        {
            public StateMachine<TState, TTrigger> StateMachine { get; private set; }
            public TState State { get; private set; }

            public StateInstance(StateMachine<TState, TTrigger> stateMachine, TState initialState)
            {
                this.StateMachine = stateMachine;
                this.State = initialState;
            }

            public bool Fire(TTrigger trigger)
            {
                TState destinationState;
                if(!StateMachine.Try(this.State, trigger, out destinationState))
                {
                    return false;
                }

                this.State = destinationState;
                return true;
            }
        }
    }
}
