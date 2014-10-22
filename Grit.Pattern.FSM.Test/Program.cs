using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.Pattern.FSM.Test
{
    class Program
    {
        enum Trigger
        {
            CallDialed,
            HungUp,
            CallConnected,
            LeftMessage,
        }

        enum State
        {
            OffHook,
            Ringing,
            Connected,
            PhoneDestroyed
        }

        static void Main(string[] args)
        {
            int size = 100000;
            var list = new List<StateMachine<State, Trigger>.StateInstance>(size);

            var phoneCall = new StateMachine<State, Trigger>();

            phoneCall.Configure(State.OffHook)
                .Permit(Trigger.CallDialed, State.Ringing);

            phoneCall.Configure(State.Ringing)
                .Permit(Trigger.HungUp, State.OffHook)
                .Permit(Trigger.CallConnected, State.Connected);

            phoneCall.Configure(State.Connected)
                .Permit(Trigger.LeftMessage, State.OffHook)
                .Permit(Trigger.HungUp, State.OffHook);

            foreach (int i in Enumerable.Range(0, size))
            {
                list.Add(phoneCall.Instance(State.OffHook));
            }

            var instance = phoneCall.Instance(State.OffHook);
            Console.WriteLine(instance.State);
            instance.Fire(Trigger.CallDialed);
            Console.WriteLine(instance.State);
            instance.Fire(Trigger.CallConnected);
            Console.WriteLine(instance.State);
            instance.Fire(Trigger.HungUp);
            Console.WriteLine(instance.State);
            instance.Fire(Trigger.LeftMessage);
            Console.WriteLine(instance.State);
        }
    }
}
