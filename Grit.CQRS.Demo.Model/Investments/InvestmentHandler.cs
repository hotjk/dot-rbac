using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grit.CQRS.Demo.Contracts.Commands;
using Grit.CQRS.Demo.Contracts.Events;

namespace Grit.CQRS.Demo.Model.Investments.Handlers
{
    public class InvestmentHandler : 
        ICommandHandler<InvestmentCreateCommand>
    {
        static InvestmentHandler()
        {
            AutoMapper.Mapper.CreateMap<InvestmentCreateCommand, Investment>();
            AutoMapper.Mapper.CreateMap<InvestmentCreateCommand, InvestmentCreatedEvent>();
        }

        private IInvestmentWriteRepository _repository;
        public InvestmentHandler(IInvestmentWriteRepository repository)
        {
            _repository = repository;
        }

        public void Execute(InvestmentCreateCommand command)
        {
            _repository.Add(AutoMapper.Mapper.Map<Investment>(command));

            ServiceLocator.EventBus.Publish(AutoMapper.Mapper.Map<InvestmentCreatedEvent>(command));
        }
    }
}
