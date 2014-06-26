using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS.Demo.Model.Investments.Events
{
    public class InvestmentCreateCommand : Command
    {
        public int InvestmentId { get; set; }
        public int AccountId { get; set; }
        public int ProjectId { get; set; }
        public int Amount { get; set; }
    }
}
