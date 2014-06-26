using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS.Demo.Model.Investments
{
    public interface IInvestmentWriteService
    {
        bool Add(Investment investment);
    }
}
