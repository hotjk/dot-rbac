using Grit.CQRS.Demo.Model.Investments.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grit.CQRS.Demo.Model.Investments.Handlers
{
    public class InvestmentHandler : ICommandHandler<InvestmentCreateCommand>
    {
        static InvestmentHandler()
        {
            AutoMapper.Mapper.CreateMap<InvestmentCreateCommand, Investment>();
        }

        private IInvestmentWriteRepository _repository;
        public InvestmentHandler(IInvestmentWriteRepository repository)
        {
            _repository = repository;
        }

        public void Execute(InvestmentCreateCommand command)
        {
            _repository.Add(AutoMapper.Mapper.Map<Investment>(command));
        }
    }
}
