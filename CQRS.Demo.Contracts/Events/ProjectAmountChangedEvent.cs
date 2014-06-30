using Grit.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Demo.Contracts.Events
{
    public class ProjectAmountChangedEvent : Event
    {
        public int ProjectId { get; set; }
        public decimal Amount { get; set; }
    }
}
