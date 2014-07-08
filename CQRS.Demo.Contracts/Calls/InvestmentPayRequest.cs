using Grit.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Demo.Contracts.Calls
{
    public class InvestmentPayRequest : Call
    {
        public int InvestmentId { get; set; }
    }
}
