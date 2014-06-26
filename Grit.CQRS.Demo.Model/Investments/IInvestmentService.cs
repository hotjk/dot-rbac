using System;

namespace Grit.CQRS.Demo.Model.Investments
{
    public interface IInvestmentService
    {
        Investment Get(int id);
    }
}
