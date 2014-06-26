using System;

namespace Grit.CQRS.Demo.Model.Investments
{
    public interface IInvestmentRepository
    {
        Investment Get(int id);
    }
}
