using Grit.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Demo.Contracts.Events
{
    public class InvestmentOrderPaied : Event
    {
        public int InvestmentId { get; set; }
    }
}
