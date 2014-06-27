using CQRS.Demo.Contracts.Events;
using CQRS.Demo.Contracts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grit.CQRS;
using CQRS.Demo.Model.Investments;

namespace CQRS.Demo.Model.Investments
{
    public class InvestmentHandler : 
        ICommandHandler<CreateInvestmentCommand>,
        ICommandHandler<CompleteInvestmentCommand>
    {
        static InvestmentHandler()
        {
            AutoMapper.Mapper.CreateMap<CreateInvestmentCommand, Investment>();
            AutoMapper.Mapper.CreateMap<CreateInvestmentCommand, InvestmentCreatedEvent>();
            AutoMapper.Mapper.CreateMap<Investment, InvestmentCompletedEvent>();
        }

        private IInvestmentWriteRepository _repository;
        public InvestmentHandler(IInvestmentWriteRepository repository)
        {
            _repository = repository;
        }

        public void Execute(CreateInvestmentCommand command)
        {
            _repository.Add(AutoMapper.Mapper.Map<Investment>(command));

            ServiceLocator.EventBus.Publish(AutoMapper.Mapper.Map<InvestmentCreatedEvent>(command));
        }

        public void Execute(CompleteInvestmentCommand command)
        {
            Investment investment = _repository.GetForUpdate(command.InvestmentId);
            _repository.Complete(command.InvestmentId);

            ServiceLocator.EventBus.Publish(AutoMapper.Mapper.Map<InvestmentCompletedEvent>(investment));
        }
    }
}
