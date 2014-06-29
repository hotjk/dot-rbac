using Grit.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Demo.Contracts.Commands
{
    public class DecreaseAccountAmountCommand : Command
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
