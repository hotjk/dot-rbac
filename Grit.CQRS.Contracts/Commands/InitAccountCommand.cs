using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS.Demo.Model.Accounts.Events
{
    public class InitAccountCommand : Command
    {
        public int AccountId { get; set; }
    }
}
