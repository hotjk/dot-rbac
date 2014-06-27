using System;

namespace CQRS.Demo.Model.Investments
{
    public interface IInvestmentRepository
    {
        Investment Get(int id);
    }
}
