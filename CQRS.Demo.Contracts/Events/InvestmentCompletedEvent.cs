using Grit.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Demo.Contracts.Events
{
    public class InvestmentCompletedEvent : Event
    {
        public int AccountId { get; set; }
        public int ProjectId { get; set; }
        public int InvestmentId { get; set; }
        public decimal Amount { get; set; }
    }
}
